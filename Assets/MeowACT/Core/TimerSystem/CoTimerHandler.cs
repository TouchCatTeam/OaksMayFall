// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 26/03/2022 1:50
// 最后一次修改于: 28/03/2022 15:28
// 版权所有: CheapMiaoStudio
// 描述:
// ----------------------------------------------

using System;
using System.Collections;
using UnityEngine;

namespace MeowACT
{
    /// <summary>
    /// 协程计时器
    /// </summary>
    public class CoTimerHandler
    {
        /// <summary>
        /// 时长
        /// </summary>
        private float duration;
        
        /// <summary>
        /// 委托回调
        /// </summary>
        private Action<object[]> callback;
        
        /// <summary>
        /// 回调参数
        /// </summary>
        private object[] args;
        
        /// <summary>
        /// 协程计时器的构造函数
        /// </summary>
        /// <param name="duration">时长</param>
        /// <param name="callback">委托回调</param>
        /// <param name="args">回调参数</param>
        public CoTimerHandler(float duration, Action<object[]> callback, params object[] args)
        {
            this.duration = duration;
            this.callback = callback;
            this.args = args;
        }

        /// <summary>
        /// 基于委托回调函数构造的迭代器函数
        /// </summary>
        /// <returns></returns>
        private IEnumerator CallBackCoroutine()
        {
            // 约定 0 为等待一帧
            if (duration == 0f)
                yield return new WaitForEndOfFrame();
            else
                yield return new WaitForSeconds(duration);
            callback?.Invoke(args);
        }

        /// <summary>
        /// 协程启动基于委托回调函数构造的迭代器函数
        /// </summary>
        public void Start()
        {
            TimerSystemSingleton.SingleInstance.StartCoroutine(CallBackCoroutine());
        }
    }
}