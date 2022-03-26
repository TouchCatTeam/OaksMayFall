// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 24/03/2022 10:54
// 最后一次修改于: 26/03/2022 19:49
// 版权所有: CheapMiaoStudio
// 描述:
// ----------------------------------------------

using UnityEngine;
using UnityGameFramework.Runtime;

namespace MeowACT
{
    /// <summary>
    /// 计时器管理器单例
    /// </summary>
    public sealed class TimerManagerSingleton : MonoBehaviour
    {
        /// <summary>
        /// 计时器管理器单例
        /// </summary>
        private static readonly TimerManagerSingleton singleInstance;

        /// <summary>
        /// 计时器管理器单例
        /// </summary>
        public static TimerManagerSingleton SingleInstance => singleInstance;
        
        /// <summary>
        /// 计时器管理器单例的时间缩放比例
        /// </summary>
        private float timeScale = 1f;
        
        /// <summary>
        /// 计时器管理器单例的时间缩放比例
        /// </summary>
        public float TimeScale => timeScale;

        /// <summary>
        /// 计时器管理器单例的回调委托类型
        /// </summary>
        public delegate void CallBackDelegate(params object[] args);

        /// <summary>
        /// 计时器管理器单例的静态构造函数
        /// 首次实例化该类或任何的静态成员被引用时调用该静态构造函数
        /// </summary>
        static TimerManagerSingleton()
        {
            var gameObject = new GameObject();
            DontDestroyOnLoad(gameObject);
            singleInstance = gameObject.AddComponent<TimerManagerSingleton>();
        }
        
        /// <summary>
        /// 创建计时器
        /// </summary>
        /// <param name="duration">时长</param>
        /// <param name="isLoop">是否循环</param>
        /// <param name="callback">回调函数</param>
        /// <returns></returns>
        public TimerHandler CreateTimer(float duration, bool isLoop, CallBackDelegate callback)
        {
            return CreateTimer(duration, isLoop, callback, null);
        }
        
        /// <summary>
        /// 创建计时器
        /// </summary>
        /// <param name="duration">时长</param>
        /// <param name="isLoop">是否循环</param>
        /// <param name="callback">回调函数</param>
        /// <param name="args">回调参数</param>
        /// <returns></returns>
        public TimerHandler CreateTimer(float duration, bool isLoop, CallBackDelegate callback,
            params object[] args)
        {
            if (duration < 0f)
                return null;
            return new TimerHandler(duration, isLoop, callback, args);
        }

        /// <summary>
        /// 创建协程计时器
        /// </summary>
        /// <param name="duration">时长</param>
        /// <param name="callback">回调函数</param>
        /// <returns></returns>
        public CoTimerHandler CreateCoTimer(float duration, CallBackDelegate callback)
        {
            return CreateCoTimer(duration, callback, null);
        }
        
        /// <summary>
        /// 创建协程计时器
        /// </summary>
        /// <param name="duration">时长</param>
        /// <param name="callback">回调函数</param>
        /// <param name="args">回调参数</param>
        /// <returns></returns>
        public CoTimerHandler CreateCoTimer(float duration, CallBackDelegate callback, params object[] args)
        {
            if (duration < 0f)
                return null;
            return new CoTimerHandler(duration, callback, args);
        }

        /// <summary>
        /// 更新计时器
        /// </summary>
        private void Update()
        {
            foreach (TimerHandler timerHandler in TimerHandler.TimerHandlerList)
            {
                timerHandler.ElapsedTime += Time.deltaTime * timeScale;
            }
        }
    }
}
