// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 26/03/2022 1:50
// 最后一次修改于: 26/03/2022 7:13
// 版权所有: ThinkDifferentStudio
// 描述:
// ----------------------------------------------

using System.Collections;
using UnityEngine;

namespace OaksMayFall
{
    public class CoTimerHandler
    {
        private float duration;
        
        private TimerComponent.CallBackDelegate callback;
        
        private object[] args;
        public CoTimerHandler(float duration, TimerComponent.CallBackDelegate callback, params object[] args)
        {
            this.duration = duration;
            this.callback = callback;
            this.args = args;
        }

        private IEnumerator CallBackCoroutine()
        {
            // 约定 0 为等待一帧
            if (duration == 0f)
                yield return new WaitForEndOfFrame();
            else
                yield return new WaitForSeconds(duration);
            callback?.Invoke(args);
        }

        public void Start()
        {
            GameEntry.Timer.StartCoroutine(CallBackCoroutine());
        }
    }
}