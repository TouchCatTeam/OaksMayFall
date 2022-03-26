// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 25/03/2022 16:50
// 最后一次修改于: 26/03/2022 7:13
// 版权所有: ThinkDifferentStudio
// 描述:
// ----------------------------------------------

namespace OaksMayFall
{
    public class TimerHandler
    {
        private bool isPasue;
        public bool IsPasue => isPasue;
        
        private bool isStop;
        public bool IsStop => isStop;
        
        private float elapsedTime;
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

        private float duration;
        public float Duration => duration;

        private bool isLoop;
        public bool IsLoop => isLoop;
        
        private TimerComponent.CallBackDelegate callback;
        
        private object[] args;

        public static RList<TimerHandler> TimerHandlerList = new RList<TimerHandler>();

        public TimerHandler(float duration, bool isLoop, TimerComponent.CallBackDelegate callback,
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

        public void Reset()
        {
            elapsedTime = 0;
        }

        public void Start()
        {
            isPasue = false;
        }
        
        public void Pause()
        {
            isPasue = true;
        }
        
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