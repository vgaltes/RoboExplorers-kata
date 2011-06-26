using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RoboExplorers.Test
{
    [TestClass]
    public class DontRepeatMoveCreatorTest
    {
        DontRepeatMoveCreator moveCreator = new DontRepeatMoveCreator();

        [TestMethod]
        public void NoRepitoDosTiradasConsecutivas()
        {            
            int firstMove = moveCreator.CreateMove();
            int secondMove = moveCreator.CreateMove();

            Assert.AreNotEqual(firstMove, secondMove);
        }

        [TestMethod]
        public void DespuesDeMilTiradasTodosLosNumerosEstanEntre0Y100()
        {
            List<bool> expectedResults = new List<bool>();
            List<bool> values = new List<bool>();
            for (int i = 0; i < 1000; i++)
            {
                int move = moveCreator.CreateMove();
                values.Add(move <= 100 && move >= 0);
                expectedResults.Add(true);
            }

            CollectionAssert.AreEqual(expectedResults, values);
        }
    }
}
