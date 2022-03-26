// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 12/03/2022 16:08
// 最后一次修改于: 26/03/2022 12:36
// 版权所有: ThinkDifferentStudio
// 描述:
// ----------------------------------------------

using GameFramework.ObjectPool;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityGameFramework.Runtime;

namespace OaksMayFall
{
    public class HPBarComponent : GameFrameworkComponent
    {
        [SerializeField]
        private GameObject HPBarItemTemplate = null;

        [FormerlySerializedAs("InstancePoolCapacity")] [SerializeField]
        private int instancePoolCapacity = 16;

        private IObjectPool<HPBarItemObject> HPBarItemObjectPool = null;
        private List<HPBarItem> activeHpBarItems = null;

        private void Start()
        {
            // 检查配置
            if (HPBarItemTemplate == null)
            {
                GameFramework.GameFrameworkLog.Debug("没有配置血量条的原型 HPBarItemTemplate");
                return;
            }
            if(GameEntry.ObjectPool == null)
            {
                GameFramework.GameFrameworkLog.Debug("没有初始化对象池 GameEntry.ObjectPool\n你可能需要检查是否放置了 GameEntry 脚本\n以及项目设置 Project Setting 中 GameEntry 脚本的执行顺序是否在 HPBarComponent 脚本之前");
                return;
            }

            HPBarItemObjectPool = GameEntry.ObjectPool.CreateSingleSpawnObjectPool<HPBarItemObject>("HPBarItem", instancePoolCapacity);
            activeHpBarItems = new List<HPBarItem>();
        }

        /// <summary>
        /// 体力条物体的值变化，同时显示
        /// 如果这个 GameObject 没有血条，那么从对象池中创建一个
        /// </summary>
        /// <param name="owner">血条的主人</param>
        /// <param name="fromHPRatio">血条初始值</param>
        /// <param name="toHPRatio">血条终点值</param>
        public void SmoothValueAndShow(GameObject owner, float fromHPRatio, float toHPRatio)
        {
            // 如果输入的实体为空，则不需要血条
            if (owner == null)
            {
                Log.Warning("HPBar owner is invalid.");
                return;
            }

            // 根据实体得到这个实体对应的可用血条
            HPBarItem hpBarItem = GetActiveHPBarItem(owner);
            // 如果得不到，就创建一个血条对象
            if (hpBarItem == null)
            {
                // 创建一个可用血条对象，以输入的 owner 为，但是血条对象的 owner 属性
                hpBarItem = CreateHPBarItem(owner);
                // 将这个创建出来的血条对象加入到可用血条列表中
                activeHpBarItems.Add(hpBarItem);
            }
            
            hpBarItem.SmoothValueAndShow(owner, fromHPRatio, toHPRatio);
        }

        public void HideHPBar(HPBarItem hpBarItem)
        {
            hpBarItem.Reset();
            activeHpBarItems.Remove(hpBarItem);
            HPBarItemObjectPool.Unspawn(hpBarItem);
        }

        /// <summary>
        /// 得到实体对应的可用血条
        /// </summary>
        /// <param name="owner">血条的实体主人</param>
        /// <returns></returns>
        public HPBarItem GetActiveHPBarItem(GameObject owner)
        {
            if (owner == null)
            {
                return null;
            }

            for (int i = 0; i < activeHpBarItems.Count; i++)
            {
                if (activeHpBarItems[i].Owner == owner)
                {
                    return activeHpBarItems[i];
                }
            }

            return null;
        }

        /// <summary>
        /// 创建血条实例
        /// </summary>
        /// <param name="owner">血条的实体主人</param>
        /// <returns></returns>
        private HPBarItem CreateHPBarItem(GameObject owner)
        {
            HPBarItem hpBarItem = null;
            // 从对象池中拿一个血条对象出来
            HPBarItemObject hpBarItemObject = HPBarItemObjectPool.Spawn();
            // 从对象池中拿到了血条对象，那么
            if (hpBarItemObject != null)
            {
                hpBarItem = (HPBarItem)hpBarItemObject.Target;
            }
            // 要是从对象池中拿不到，说明需要新建血条对象
            else
            {
                // 实例化血条 Perfab
                hpBarItem = Instantiate(HPBarItemTemplate).GetComponent<HPBarItem>();
                if(hpBarItem == null)
                    Debug.LogError("要显示血条的 Perfab 身上没有 HPBarItem 组件");
                hpBarItem.GetComponent<Canvas>().worldCamera = Camera.main;
                hpBarItem.Owner = owner;

                hpBarItem.Refresh();
                
                // 创建的血条对象加入对象池
                HPBarItemObjectPool.Register(HPBarItemObject.Create(hpBarItem), true);
            }

            return hpBarItem;
        }
    }
}
