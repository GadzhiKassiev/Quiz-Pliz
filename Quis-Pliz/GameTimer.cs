using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quis_Pliz
{
    internal class GameTimer
    {
        Thread _timerThread;
        System.Timers.Timer _timer;
        int _count;

        public GameTimer()
        {
            _count = 0;
            _timer = new System.Timers.Timer(1000);
            _timerThread = new Thread(ElapseHookup);
            _timerThread.IsBackground = true;
            _timerThread?.Start();
        }

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
            _count = 0;
        }

        private void ElapseHookup()
        {
            _timer.Elapsed += HandleTimerElapsed;
        }

        private void HandleTimerElapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            Screen.ShowTime(_count++);
        }
    }
}
