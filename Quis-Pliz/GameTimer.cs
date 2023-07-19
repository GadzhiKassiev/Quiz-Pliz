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
            _timerThread = new Thread(Timing);
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

        private void Timing()
        {
            _timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            Screen.ShowTime(_count++);
        }
    }
}
