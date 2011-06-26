using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoboExplorers.Test
{
    public interface TimeSleeper
    {
        void Sleep(int milisecondsBetweenCalls);
    }
}
