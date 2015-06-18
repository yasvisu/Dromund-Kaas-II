using System;
using System.Threading;
using System.Threading.Tasks;

namespace DromundKaasII.Tools
{
    public class AsyncTimer
    {
        Action action;
        int ticks;
        TimeSpan time;

        public AsyncTimer(Action Action, int Ticks, ulong Time)
        {
            this.action = Action;
            this.ticks = Ticks;
            this.time = new TimeSpan(0, 0, 0, 0, (int)Time);
        }

        public Action Action
        {
            get
            {
                return this.action;
            }
        }
        public int Ticks
        {
            get
            {
                return this.ticks;
            }
        }
        public TimeSpan Time
        {
            get
            {
                return this.time;
            }
        }

        public Task StartAsync()
        {
            Task thisTask = Task.Factory.StartNew(() =>
            {
                while (ticks > 0)
                {
                    this.Action();
                    this.ticks--;
                    Thread.Sleep(this.Time);
                }
            });
            
            return thisTask;
        }
    }
}
