//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework.ObjectPool;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace OaksMayFall
{
    public class HPBarComponent : GameFrameworkComponent
    {
        [SerializeField]
        private HPBarItem m_HPBarItemTemplate = null;

        [SerializeField]
        private int m_InstancePoolCapacity = 16;

        private IObjectPool<HPBarItemObject> m_HPBarItemObjectPool = null;
        private List<HPBarItem> m_ActiveHPBarItems = null;

        private void Start()
        {
            // 检查配置
            if (m_HPBarItemTemplate == null)
            {
                GameFramework.GameFrameworkLog.Debug("没有配置血量条的原型 m_HPBarItemTemplate");
                return;
            }
            if(GameEntry.ObjectPool == null)
            {
                GameFramework.GameFrameworkLog.Debug("没有初始化对象池 GameEntry.ObjectPool\n你可能需要检查是否放置了 GameEntry 脚本\n以及项目设置 Project Setting 中 GameEntry 脚本的执行顺序是否在 HPBarComponent 脚本之前");
                return;
            }

            m_HPBarItemObjectPool = GameEntry.ObjectPool.CreateSingleSpawnObjectPool<HPBarItemObject>("HPBarItem", m_InstancePoolCapacity);
            m_ActiveHPBarItems = new List<HPBarItem>();
        }

        private void OnDestroy()
        {
        }

        private void Update()
        {
            // 如果配置正常，那么 m_ActiveHPBarItems 将会指向一个列表，否则指向 null
            if (m_ActiveHPBarItems != null)
            {
                for (int i = m_ActiveHPBarItems.Count - 1; i >= 0; i--)
                {
                    HPBarItem hpBarItem = m_ActiveHPBarItems[i];
                    if (hpBarItem.Refresh())
                    {
                        continue;
                    }

                    HideHPBar(hpBarItem);
                }
            }
        }

        /// <summary>
        /// 显示某个实体的血条从某一值变化到另一值，如果这个实体没有血条，就自动创建一个血条对象
        /// </summary>
        /// <param name="entity">血条的主人</param>
        /// <param name="fromHPRatio">血条初始值</param>
        /// <param name="toHPRatio">血条终点值</param>
        public void ShowHPBar(UEntity entity, float fromHPRatio, float toHPRatio)
        {
            // 如果输入的实体为空，则不需要血条
            if (entity == null)
            {
                Log.Warning("Entity is invalid.");
                return;
            }

            // 根据实体得到这个实体对应的可用血条
            HPBarItem hpBarItem = GetActiveHPBarItem(entity);
            // 如果得不到，就创建一个血条对象
            if (hpBarItem == null)
            {
                // 创建一个可用血条对象，血条的主人是输入的实体
                hpBarItem = CreateHPBarItem(entity);
                // 将这个创建出来的血条对象加入到可用血条列表中
                m_ActiveHPBarItems.Add(hpBarItem);
            }
            
            hpBarItem.Process(entity, fromHPRatio, toHPRatio);
        }

        private void HideHPBar(HPBarItem hpBarItem)
        {
            hpBarItem.Reset();
            m_ActiveHPBarItems.Remove(hpBarItem);
            m_HPBarItemObjectPool.Unspawn(hpBarItem);
        }

        /// <summary>
        /// 得到实体对应的可用血条
        /// </summary>
        /// <param name="entity">血条的实体主人</param>
        /// <returns></returns>
        private HPBarItem GetActiveHPBarItem(UEntity entity)
        {
            if (entity == null)
            {
                return null;
            }

            for (int i = 0; i < m_ActiveHPBarItems.Count; i++)
            {
                if (m_ActiveHPBarItems[i].Owner == entity)
                {
                    return m_ActiveHPBarItems[i];
                }
            }

            return null;
        }

        /// <summary>
        /// 创建血条实例
        /// </summary>
        /// <param name="entity">血条的实体主人</param>
        /// <returns></returns>
        private HPBarItem CreateHPBarItem(UEntity entity)
        {
            HPBarItem hpBarItem = null;
            // 从对象池中拿一个血条对象出来
            HPBarItemObject hpBarItemObject = m_HPBarItemObjectPool.Spawn();
            // 从对象池中拿到了血条对象，那么
            if (hpBarItemObject != null)
            {
                hpBarItem = (HPBarItem)hpBarItemObject.Target;
            }
            // 要是从对象池中拿不到，说明需要新建血条对象
            else
            {
                // 实例化血条 Perfab
                hpBarItem = Instantiate(m_HPBarItemTemplate);
                // 血条挂在 entity 的 HPBarRoot 下
                var hpBarRoot = entity.transform.Find("HPBarRoot");
                if(hpBarRoot == null)
                    Debug.LogError("要显示血条的实体的身上没有 HPBarRoot 用于挂载血条实例");
                hpBarItem.transform.SetParent(hpBarRoot);
                hpBarItem.GetComponent<Canvas>().worldCamera = Camera.main;
                hpBarItem.transform.localPosition = Vector3.zero;
                hpBarItem.transform.localEulerAngles = Vector3.zero;
                hpBarItem.transform.localScale = Vector3.one;
                // 创建的血条对象加入对象池
                m_HPBarItemObjectPool.Register(HPBarItemObject.Create(hpBarItem), true);
            }

            return hpBarItem;
        }
    }
}
