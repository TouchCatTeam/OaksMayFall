//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework;
using GameFramework.ObjectPool;
using UnityEngine;

namespace OaksMayFall
{
    /// <summary>
    /// 对象池中的血条对象
    /// </summary>
    public class HPBarItemObject : ObjectBase
    {
        /// <summary>
        /// 对象池中的血条对象创建血条 Item 实例
        /// </summary>
        /// <param name="target">血条的 Perfab 的实例</param>
        /// <returns></returns>
        public static HPBarItemObject Create(object target)
        {
            // 新建对象引用
            HPBarItemObject hpBarItemObject = ReferencePool.Acquire<HPBarItemObject>();
            // 使用血条的 Perfab 的实例初始化血条对象
            // 现在这个对象的 target 就是血条的 Perfab 的实例
            hpBarItemObject.Initialize(target);
            // 返回血条对象
            return hpBarItemObject;
        }

        /// <summary>
        /// 对象池中的血条对象释放血条 Item 实例
        /// </summary>
        /// <param name="isShutdown"></param>
        protected override void Release(bool isShutdown)
        {
            // 返回一个血条的 Perfab 的实例，类型转换为血条物体
            HPBarItem hpBarItem = (HPBarItem)Target;
            // 如果血条的 Perfab 的实例为空，那么直接返回
            if (hpBarItem == null)
            {
                return;
            }
            
            // 销毁血条的 Perfab 的实例
            Object.Destroy(hpBarItem.gameObject);
        }
    }
}
