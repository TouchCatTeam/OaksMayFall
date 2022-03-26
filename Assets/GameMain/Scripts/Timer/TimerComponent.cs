// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 24/03/2022 10:54
// 最后一次修改于: 26/03/2022 7:23
// 版权所有: ThinkDifferentStudio
// 描述:
// ----------------------------------------------

using UnityEngine;
using UnityGameFramework.Runtime;

namespace OaksMayFall
{
    public class TimerComponent : GameFrameworkComponent
    {
        private float timeScale = 1f;
        public float TimeScale => timeScale;

        public delegate void CallBackDelegate(params object[] args);
        
        public TimerHandler CreateTimer(float duration, bool isLoop, CallBackDelegate callback,
            params object[] args)
        {
            if (duration < 0f)
                return null;
            return new TimerHandler(duration, isLoop, callback, args);
        }

        public TimerHandler CreateTimer(float duration, bool isLoop, CallBackDelegate callback)
        {
            if (duration < 0f)
                return null;
            return new TimerHandler(duration, isLoop, callback, null);
        }

        public CoTimerHandler CreateCoTimer(float duration, CallBackDelegate callback, params object[] args)
        {
            if (duration < 0f)
                return null;
            return new CoTimerHandler(duration, callback, args);
        }
        
        public CoTimerHandler CreateCoTimer(float duration, CallBackDelegate callback)
        {
            if (duration < 0f)
                return null;
            return new CoTimerHandler(duration, callback, null);
        }
        
        private void Update()
        {
            foreach (TimerHandler timerHandler in TimerHandler.TimerHandlerList)
            {
                timerHandler.ElapsedTime += Time.deltaTime * timeScale;
            }
        }
    }
}
