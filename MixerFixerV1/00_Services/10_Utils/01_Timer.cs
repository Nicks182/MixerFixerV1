using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services
{
    public class Srv_TimerManager
    {
        private Timer? G_Timer;
        private AutoResetEvent? G_AutoResetEvent;
        private Action? G_Action;
        public bool IsTimerStarted { get; set; }
        public void PrepareTimer(Action action)
        {
            if (IsTimerStarted == false)
            {
                G_Action = action;
                G_AutoResetEvent = new AutoResetEvent(false);
                G_Timer = new Timer(Execute, G_AutoResetEvent, 1000, 25);
                IsTimerStarted = true;
            }
        }
        public void Execute(object? stateInfo)
        {
            G_Action();
        }

        
    }
}
