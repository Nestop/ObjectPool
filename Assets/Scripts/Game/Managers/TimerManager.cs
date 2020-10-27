using System;
using System.Collections.Generic;
using Utils;

namespace Game.Managers
{
    public class TimerManager : MBSingleton<TimerManager>
    {
        private readonly List<Timer> _timers = new List<Timer>();
        
        protected override void OnSingletonAwake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void Update()
        {
            foreach (var timer in _timers)
            {
                timer.Tick();
            }
        }

        public Timer AddTimer(float loopTime, int loopCount, Action loopAction, bool autoStart = false, bool autoDelete = true, Action actionEnd = null)
        {
            var timer = new Timer(loopTime, loopCount, loopAction, autoStart, autoDelete, actionEnd);
            _timers.Add(timer);
            return timer;
        }

        public void StartTimer(Timer timer)
        {
            if(!_timers.Contains(timer)) _timers.Add(timer);
            timer.OnTimerDelete += DeleteTimer;
            timer.Start();
        }

        public void StopTimer(Timer timer, bool delete = false)
        {
            if (!_timers.Contains(timer)) return;
            timer.Stop();
            if (delete) _timers.Remove(timer);
        }

        private void DeleteTimer(Timer timer)
        {
            _timers.Remove(timer);
        }
    }
}