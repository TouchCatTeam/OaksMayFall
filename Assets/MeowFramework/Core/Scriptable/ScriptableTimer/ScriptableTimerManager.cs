// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 02/04/2022 23:14
// 最后一次修改于: 11/04/2022 10:31
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MeowFramework.Core.Scriptable
{
    /// <summary>
    /// 可资产化计时器管理器
    /// </summary>
    [InlineEditor]
    [CreateAssetMenu(menuName = "MeowFramework/Scriptable Timer/Create Scriptable Timer Manager")]
    public class ScriptableTimerManager : SerializedScriptableObject
    {
        /// <summary>
        /// 可资产化计时器管理器的时间缩放比例
        /// </summary>
        private float timeScale = 1f;
        
        /// <summary>
        /// 可资产化计时器管理器的时间缩放比例
        /// </summary>
        public float TimeScale => timeScale;

        /// <summary>
        /// 计时器列表 可以在 ForEach 中删除元素
        /// </summary>
        public static List<ScriptableTimer> ScriptableTimerList = new List<ScriptableTimer>();
        
        /// <summary>
        /// 创建计时器
        /// </summary>
        /// <param name="duration">时长</param>
        /// <param name="isLoop">是否循环</param>
        /// <param name="callback">回调函数</param>
        /// <returns></returns>
        public ScriptableTimer CreateScriptableTimer(float duration, bool isLoop, Action<object[]> callback)
        {
            return CreateScriptableTimer(duration, isLoop, callback, null);
        }
        
        /// <summary>
        /// 创建计时器
        /// </summary>
        /// <param name="duration">时长</param>
        /// <param name="isLoop">是否循环</param>
        /// <param name="callback">回调函数</param>
        /// <param name="args">回调参数</param>
        /// <returns></returns>
        public ScriptableTimer CreateScriptableTimer(float duration, bool isLoop, Action<object[]> callback,
            params object[] args)
        {
            if (duration < 0f)
                return null;
            var timer =  new ScriptableTimer(duration, isLoop, callback, args);
            ScriptableTimerList.Add(timer);
            return timer;
        }

        /// <summary>
        /// 更新计时器
        /// </summary>
        private void UpdateAllScriptableTimers()
        {
            // 从尾到头遍历，可以方便在遍历中删除元素
            for (int i = ScriptableTimerList.Count; i >= 0; --i)
            {
                ScriptableTimerList[i].ElapsedTime += Time.deltaTime * timeScale;
                if (ScriptableTimerList[i].ElapsedTime >= ScriptableTimerList[i].Duration)
                    ScriptableTimerList[i].Stop();
            }
        }
    }
}