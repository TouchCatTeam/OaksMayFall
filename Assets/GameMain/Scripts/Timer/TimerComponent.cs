using System;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace OaksMayFall
{
    public class TimerComponent : GameFrameworkComponent
    {
        private float _timeScale = 1f;
        public float TimeScale => _timeScale;
        
        public TimerHandler CreateTimer(float duration, bool isLoop, TimerHandler.CallBackDelegate callback, params object[] args)
        {
            return new TimerHandler(duration, isLoop, callback, args);
        }

        public TimerHandler CreateTimer(float duration, bool isLoop, TimerHandler.CallBackDelegate callback)
        {
            return new TimerHandler(duration, isLoop, callback, null);
        }
        
        private void Update()
        {
            foreach (TimerHandler timerHandler in TimerHandler.TimerHandlerList)
            {
                timerHandler.ElapsedTime += Time.deltaTime * _timeScale;
            }
        }
    }
}
