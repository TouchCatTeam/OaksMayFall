// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 29/03/2022 8:20
// 最后一次修改于: 02/04/2022 23:05
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using System.Collections.Generic;
using UnityEngine;

namespace MeowFramework.Core
{
    /// <summary>
    /// 可资产化事件
    /// </summary>
    [CreateAssetMenu(menuName = "MeowFramework.Core/Sriptable Reflection/Create Scriptable Event")]
    public class ScriptableEvent : ScriptableObject
    {
        /// <summary>
        /// 可资产化事件的订阅者列表
        /// </summary>
        private readonly List<ScriptableEventListener> eventListeners = 
            new List<ScriptableEventListener>();

        /// <summary>
        /// 触发可资产化事件
        /// </summary>
        public void Raise(List<object> args)
        {
            for(int i = eventListeners.Count -1; i >= 0; i--)
                eventListeners[i].OnEventRaised(this, args);
        }

        /// <summary>
        /// 订阅可资产化事件
        /// </summary>
        /// <param name="listener"></param>
        public void RegisterListener(ScriptableEventListener listener)
        {
            if (!eventListeners.Contains(listener))
                eventListeners.Add(listener);
        }

        /// <summary>
        /// 退订可资产化事件
        /// </summary>
        /// <param name="listener"></param>
        public void UnregisterListener(ScriptableEventListener listener)
        {
            if (eventListeners.Contains(listener))
                eventListeners.Remove(listener);
        }
    }
}