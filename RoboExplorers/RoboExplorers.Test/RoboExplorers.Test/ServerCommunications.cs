using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoboExplorers.Test
{
    public interface ServerCommunications
    {
        ServerStatus Initialize(string robotName);

        ServerStatus Move(int position);
    }
}
