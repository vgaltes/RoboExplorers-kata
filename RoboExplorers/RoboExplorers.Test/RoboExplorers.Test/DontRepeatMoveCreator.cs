using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoboExplorers.Test
{
    public class DontRepeatMoveCreator : MoveCreator
    {
        private int lastMove = 0;

        public int CreateMove()
        {
            int directionIdentifier = 1;
            int nextMove = lastMove;

            if (lastMove == 100)
            {
                directionIdentifier = -1;
            }
            else if (lastMove == 0)
            {
                directionIdentifier = 1;
            }

            nextMove = lastMove + (1 * directionIdentifier);
            lastMove = nextMove;

            return nextMove;
        }
    }
}
