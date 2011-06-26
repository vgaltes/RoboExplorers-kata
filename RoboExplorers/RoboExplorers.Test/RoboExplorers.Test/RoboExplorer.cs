using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoboExplorers.Test
{
    public class RoboExplorer
    {
        ServerCommunications serverCommunications = null;
        string robotName = string.Empty;
        private MoveCreator moveCreator;
        private TimeSleeper timeSleeper;
        private int timeToWaitForNextMove;

        public RoboExplorer(string robotName, ServerCommunications serverCommunications, MoveCreator moveCreator, TimeSleeper timeSleeper, int timeToWaitForNextMove)
        {
            this.robotName = robotName;
            this.serverCommunications = serverCommunications;
            this.moveCreator = moveCreator;
            this.timeSleeper = timeSleeper;
            this.timeToWaitForNextMove = timeToWaitForNextMove;
        }

        public void Run()
        {
            ServerStatus serverResponse = this.serverCommunications.Initialize(this.robotName);

            while (serverResponse == ServerStatus.OK)
            {
                int position = moveCreator.CreateMove();
                timeSleeper.Sleep(this.timeToWaitForNextMove);
                serverResponse = this.serverCommunications.Move(position);
            }
        }
    }
}
