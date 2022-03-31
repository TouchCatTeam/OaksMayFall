// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 29/03/2022 8:24
// 最后一次修改于: 30/03/2022 15:15
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MeowACT
{
    /// <summary>
    /// 可资产化事件的订阅者
    /// </summary>
    public class ScriptableEventListener : MonoBehaviour
    {
        /// <summary>
        /// 为了 Unity 的序列化而制作的可资产化事件-响应映射对结构体
        /// </summary>
        [Serializable]
        public struct EventMapPair
        {
            public ScriptableEvent Key;
            public UnityEvent<List<UnityEngine.Object>> Value;
        }

        /// <summary>
        /// 可资产化事件-响应映射对数组
        /// </summary>
        public EventMapPair[] EventMap;
        
        /// <summary>
        /// 可资产化事件-响应字典
        /// </summary>
        public Dictionary<ScriptableEvent, UnityEvent<List<UnityEngine.Object>>> EventMapDictionary = new Dictionary<ScriptableEvent, UnityEvent<List<UnityEngine.Object>>>();
        
        /// <summary>
        /// 惰性加载字典
        /// </summary>
        private void LazyLoadDictionary()
        {
            for (int i = 0; i < EventMap.Length; i++)
            {
                EventMapDictionary.Add(EventMap[i].Key, EventMap[i].Value);
            }
        }

        /// <summary>
        /// 脚本激活时订阅字典中所有可资产化事件
        /// </summary>
        private void OnEnable()
        {
            // 只在字典中没有键值对时才加载字典，防止因为脚本多次 Enable 而多次加载字典
            if(EventMapDictionary.Count == 0)
                LazyLoadDictionary();
            
            foreach (KeyValuePair<ScriptableEvent, UnityEvent<List<UnityEngine.Object>>> pair in EventMapDictionary)
            {
                pair.Key.RegisterListener(this);
            }
        }

        /// <summary>
        /// 脚本禁用时退订字典中所有可资产化事件
        /// </summary>
        private void OnDisable()
        {
            foreach (KeyValuePair<ScriptableEvent, UnityEvent<List<UnityEngine.Object>>> pair in EventMapDictionary)
            {
                pair.Key.UnregisterListener(this);
            }
        }

        
        /// <summary>
        /// 可资产化事件触发时调用字典中相应的事件回调
        /// </summary>
        /// <param name="sender">触发的事件</param>
        public void OnEventRaised(ScriptableEvent sender, List<UnityEngine.Object> args)
        {
            EventMapDictionary[sender].Invoke(args);
        }
    }
}