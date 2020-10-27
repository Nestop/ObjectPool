using System;
using UnityEngine;

namespace Utils
{
    public class Timer
    {
        public event Action<Timer> OnTimerDelete; 
        
        private readonly float _loopTime;
        private readonly int _loopCount;
        private readonly Action _loopAction;
        private readonly Action _endAction;
        private readonly bool _delete;
        
        private float _timer;
        private bool _isActive;
        private int _currentLoop;

        public Timer(float loopTime, int loopCount, Action loopAction, bool autoStart = false, bool autoDelete = true, Action endAction = null)
        {
            _loopAction = loopAction;
            _endAction = endAction;
            _loopTime = loopTime;
            _loopCount = loopCount;
            _timer = 0;
            _delete = autoDelete;
            
            if(autoStart) Start();
        }

        public void Start()
        {
            _currentLoop = _loopCount;
            _isActive = true;
        }

        public void Stop()
        {
            _isActive = false;
        }

        public void Tick()
        {
            if(!_isActive) return;
            
            _timer += Time.deltaTime;
            
            if (_timer < _loopTime) return;
            
            _loopAction?.Invoke();
            _currentLoop--;
            _timer = 0;
            
            if(_currentLoop > 0) return;
            
            _endAction?.Invoke();
            _isActive = false;
            
            if (_delete) OnTimerDelete?.Invoke(this);
        }
    }
}