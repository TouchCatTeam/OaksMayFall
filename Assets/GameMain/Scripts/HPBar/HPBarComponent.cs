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
        private Transform m_HPBarInstanceRoot = null;

        [SerializeField]
        private int m_InstancePoolCapacity = 16;

        private IObjectPool<HPBarItemObject> m_HPBarItemObjectPool = null;
        private List<HPBarItem> m_ActiveHPBarItems = null;
        private Canvas m_CachedCanvas = null;

        private void Start()
        {
            // 检查配置
            if (m_HPBarItemTemplate == null)
            {
                GameFramework.GameFrameworkLog.Debug("没有配置血量条的原型 m_HPBarItemTemplate");
                return;
            }
            if (m_HPBarInstanceRoot == null)
            {
                GameFramework.GameFrameworkLog.Debug("没有配置血量条的根节点 m_HPBarInstanceRoot");
                return;
            }
            if(GameEntry.ObjectPool == null)
            {
                GameFramework.GameFrameworkLog.Debug("没有初始化对象池 GameEntry.ObjectPool\n你可能需要检查是否放置了 GameEntry 脚本\n以及项目设置 Project Setting 中 GameEntry 脚本的执行顺序是否在 HPBarComponent 脚本之前");
                return;
            }

            m_CachedCanvas = m_HPBarInstanceRoot.GetComponent<Canvas>();
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

        public void ShowHPBar(UEntity entity, float fromHPRatio, float toHPRatio)
        {
            if (entity == null)
            {
                Log.Warning("Entity is invalid.");
                return;
            }

            HPBarItem hpBarItem = GetActiveHPBarItem(entity);
            if (hpBarItem == null)
            {
                hpBarItem = CreateHPBarItem(entity);
                m_ActiveHPBarItems.Add(hpBarItem);
            }

            hpBarItem.Init(entity, m_CachedCanvas, fromHPRatio, toHPRatio);
        }

        private void HideHPBar(HPBarItem hpBarItem)
        {
            hpBarItem.Reset();
            m_ActiveHPBarItems.Remove(hpBarItem);
            m_HPBarItemObjectPool.Unspawn(hpBarItem);
        }

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

        private HPBarItem CreateHPBarItem(UEntity entity)
        {
            HPBarItem hpBarItem = null;
            HPBarItemObject hpBarItemObject = m_HPBarItemObjectPool.Spawn();
            if (hpBarItemObject != null)
            {
                hpBarItem = (HPBarItem)hpBarItemObject.Target;
            }
            else
            {
                hpBarItem = Instantiate(m_HPBarItemTemplate);
                Transform transform = hpBarItem.GetComponent<Transform>();
                transform.SetParent(m_HPBarInstanceRoot);
                transform.localScale = Vector3.one;
                m_HPBarItemObjectPool.Register(HPBarItemObject.Create(hpBarItem), true);
            }

            return hpBarItem;
        }
    }
}
