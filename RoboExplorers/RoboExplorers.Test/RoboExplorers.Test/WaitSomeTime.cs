using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoboExplorers.Test
{
    public class WaitSomeTime : TimeSleeper
    {
        DateTime lastCall = DateTime.Now;

        public void Sleep(int milisecondsBetweenCalls)
        {
            DateTime actualCall = DateTime.Now;
            TimeSpan timeBetweenCalls = actualCall - lastCall;

            if (timeBetweenCalls.TotalMilliseconds < milisecondsBetweenCalls)
            {
                System.Threading.Thread.Sleep(milisecondsBetweenCalls - (int)timeBetweenCalls.TotalMilliseconds);
            }
            
            lastCall = DateTime.Now;
        }
    }
}
