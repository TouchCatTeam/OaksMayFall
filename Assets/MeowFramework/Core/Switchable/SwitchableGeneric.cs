// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 22/04/2022 18:55
// 最后一次修改于: 25/04/2022 10:57
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MeowFramework.Core.Switchable
{
    public abstract class SwitchableGeneric<T> : ISwitchable
    {
        /// <summary>
        /// 当前值
        /// </summary>
        [ShowInInspector]
        [Tooltip("当前值")]
        protected T value;

        /// <summary>
        /// 当前值
        /// </summary>
        public T Value
        {
            get => value;
            set
            {
                AfterValueChangeAction?.Invoke(this.value, value);
                this.value = value;
            }
        }
        
        /// <summary>
        /// 值改变后触发的委托
        /// </summary>
        [HideInInspector]
        public Action<T, T> AfterValueChangeAction;
        
        /// <summary>
        /// 预设值字典
        /// </summary>
        [Tooltip("预设值字典")]
        public Dictionary<Enum, T> TargetValueDict = new Dictionary<Enum, T>();

        // 缓存

        /// <summary>
        /// 平滑速度
        /// </summary>
        protected T smoothVelocity;

        /// <summary>
        /// 平滑时间
        /// </summary>
        [Tooltip("平滑时间")]
        public float SmoothTime = 0.2f;

        // 析构函数
        ~SwitchableGeneric()
        {
            AfterValueChangeAction = null;
        }
        
        // 接口
        
        // 延迟实现接口
        
        /// <summary>
        /// 使变量在不同预设值之间切换
        /// </summary>
        /// <param name="mode">预设模式</param>
        public abstract void SwitchValue(Enum mode);
    }
}