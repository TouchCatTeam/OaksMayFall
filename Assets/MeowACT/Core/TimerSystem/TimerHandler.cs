// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 25/03/2022 16:50
// 最后一次修改于: 29/03/2022 11:02
// 版权所有: CheapMiaoStudio
// 描述:
// ----------------------------------------------

using System;
using System.Collections.Generic;

namespace MeowACT
{
    /// <summary>
    /// 计时器
    /// </summary>
    public class TimerHandler
    {
        /// <summary>
        /// 是否暂停
        /// </summary>
        private bool isPasue;
        
        /// <summary>
        /// 是否暂停
        /// </summary>
        public bool IsPasue => isPasue;
        
        /// <summary>
        /// 是否停止
        /// </summary>
        private bool isStop;
        
        /// <summary>
        /// 是否停止
        /// </summary>
        public bool IsStop => isStop;
        
        /// <summary>
        /// 流逝时间
        /// </summary>
        private float elapsedTime;
        
        /// <summary>
        /// 流逝时间
        /// </summary>
        public float ElapsedTime
        {
            get => elapsedTime;
            set
            {
                if (!isStop && !isPasue) 
                    elapsedTime = value;
                if (elapsedTime >= duration)
                    Stop();
            }
        }

        /// <summary>
        /// 时长
        /// </summary>
        private float duration;
        
        /// <summary>
        /// 时长
        /// </summary>
        public float Duration => duration;

        /// <summary>
        /// 是否循环
        /// </summary>
        private bool isLoop;
        
        /// <summary>
        /// 是否循环
        /// </summary>
        public bool IsLoop => isLoop;
        
        /// <summary>
        /// 委托回调
        /// </summary>
        private Action<object[]> callback;
        
        /// <summary>
        /// 回调参数
        /// </summary>
        private object[] args;

        /// <summary>
        /// 计时器列表 可以在 ForEach 中删除元素
        /// </summary>
        public static List<TimerHandler> TimerHandlerList = new List<TimerHandler>();

        /// <summary>
        /// 计时器的构造函数
        /// </summary>
        /// <param name="duration">时长</param>
        /// <param name="isLoop">是否循环</param>
        /// <param name="callback">委托回调</param>
        /// <param name="args">回调参数</param>
        public TimerHandler(float duration, bool isLoop, Action<object[]> callback,
            params object[] args)
        {
            this.isPasue = true;
            this.isStop = false;
            this.duration = duration;
            this.isLoop = isLoop;
            this.callback = callback;
            this.args = args;

            TimerHandlerList.Add(this);
        }

        /// <summary>
        /// 重置计时器
        /// </summary>
        public void Reset()
        {
            elapsedTime = 0;
        }

        /// <summary>
        /// 启动计时器
        /// </summary>
        public void Start()
        {
            isPasue = false;
        }
        
        /// <summary>
        /// 暂停计时器
        /// </summary>
        public void Pause()
        {
            isPasue = true;
        }
        
        /// <summary>
        /// 停止计时器
        /// </summary>
        public void Stop()
        {
            callback?.Invoke(args);
            if (isLoop)
            {
                elapsedTime -= duration;
            }
            else
            {
                isPasue = true;
                isStop = true;
                TimerHandlerList.Remove(this);
            }
        }
    }
}