// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 29/03/2022 8:24
// 最后一次修改于: 02/04/2022 23:05
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine.Events;

namespace MeowFramework.Core
{
    /// <summary>
    /// 可资产化事件的订阅者
    /// </summary>
    public class ScriptableEventListener : SerializedMonoBehaviour
    {
        /// <summary>
        /// 可资产化事件-响应字典
        /// </summary>
        public Dictionary<ScriptableEvent, UnityEvent<List<object>>> EventMapDictionary = new Dictionary<ScriptableEvent, UnityEvent<List<object>>>();
        
        /// <summary>
        /// 脚本激活时订阅字典中所有可资产化事件
        /// </summary>
        private void OnEnable()
        {
            foreach (var pair in EventMapDictionary)
            {
                pair.Key.RegisterListener(this);
            }
        }

        /// <summary>
        /// 脚本禁用时退订字典中所有可资产化事件
        /// </summary>
        private void OnDisable()
        {
            foreach (var pair in EventMapDictionary)
            {
                pair.Key.UnregisterListener(this);
            }
        }

        
        /// <summary>
        /// 可资产化事件触发时调用字典中相应的事件回调
        /// </summary>
        /// <param name="sender">触发的事件</param>
        public void OnEventRaised(ScriptableEvent sender, List<object> args)
        {
            EventMapDictionary[sender].Invoke(args);
        }
    }
}