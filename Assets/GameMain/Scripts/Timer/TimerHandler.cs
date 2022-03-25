using UnityEngine;

namespace OaksMayFall
{
    public class TimerHandler
    {
        private bool _isPasue;
        public bool IsPasue => _isPasue;
        
        private bool _isStop;
        public bool IsStop => _isStop;
        
        private float _elapsedTime;
        public float ElapsedTime
        {
            get => _elapsedTime;
            set
            {
                if (!_isStop && !_isPasue) 
                    _elapsedTime = value;
                if (_elapsedTime >= _duration)
                    Stop();
            }
        }

        private float _duration;
        public float Duration => _duration;

        private bool _isLoop;
        public bool IsLoop => _isLoop;
        
        public delegate void CallBackDelegate(params object[] args);
        private CallBackDelegate _callback;
        
        private object[] _args;

        public static RList<TimerHandler> TimerHandlerList = new RList<TimerHandler>();
        
        public TimerHandler(float duration, bool isLoop, CallBackDelegate callback, params object[] args)
        {
            _isPasue = true;
            _isStop = false;
            _duration = duration;
            _isLoop = isLoop;
            _callback = callback;
            _args = args;

            TimerHandlerList.Add(this);
            
            Debug.Log("add");
        }

        public void Reset()
        {
            _elapsedTime = 0;
        }

        public void Start()
        {
            _isPasue = false;
            Debug.Log("Start");
        }
        
        public void Pause()
        {
            _isPasue = true;
        }
        
        public void Stop()
        {
            Debug.Log("Stop");
            _callback?.Invoke(_args);
            if (_isLoop)
            {
                _elapsedTime -= _duration;
            }
            else
            {
                _isPasue = true;
                _isStop = true;
                TimerHandlerList.Remove(this);
            }
        }
    }
}