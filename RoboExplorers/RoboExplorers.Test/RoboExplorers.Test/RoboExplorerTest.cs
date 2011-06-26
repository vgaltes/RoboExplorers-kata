using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace RoboExplorers.Test
{
    [TestClass]
    public class RoboExplorerTest
    {
        string robotName = "vgaltes";
        int position = 10;
        Mock<ServerCommunications> serverCommunications = new Mock<ServerCommunications>(MockBehavior.Loose);
        Mock<MoveCreator> moveCreator = new Mock<MoveCreator>(MockBehavior.Loose);
        Mock<TimeSleeper> timeSleeper = new Mock<TimeSleeper>(MockBehavior.Loose);
        RoboExplorer robot = null;

        [TestInitialize]
        public void SetUp()
        {
            moveCreator.Setup(mc => mc.CreateMove()).Returns(position);
            timeSleeper.Setup(ts => ts.Sleep(It.IsAny<int>()));
            robot = new RoboExplorer(robotName, serverCommunications.Object, moveCreator.Object, timeSleeper.Object, 200);
        }

        [TestMethod]
        public void SiInitializeNoMeDevueleOKNoLlamoAMove()
        {            
            serverCommunications.Setup(sc => sc.Initialize(robotName)).Returns(ServerStatus.GAME_OVER);
            serverCommunications.Setup(sc => sc.Move(position));

            robot.Run();

            serverCommunications.Verify(sc => sc.Initialize(robotName));
            serverCommunications.Verify(sc => sc.Move(position), Times.Never());
        }

        [TestMethod]
        public void SiInitializeMeDevueleOKLlamoAMove()
        {
            SetupServerSendGameOverInNMoves(1);

            robot.Run();

            serverCommunications.Verify(sc => sc.Initialize(robotName));
            serverCommunications.Verify(sc => sc.Move(position), Times.AtLeastOnce());
        }

        [TestMethod]
        public void SiElServidorMeDevuelveGameOverDespuesDeUnMovimientoDejoDeMover()
        {
            SetupServerSendGameOverInNMoves(2);

            robot.Run();

            serverCommunications.Verify(sc => sc.Move(position), Times.Exactly(2));
        }
                
        [TestMethod]
        public void TengoQueLlamarAlCreadorDeTiradasAntesDeTirar()
        {
            SetupServerSendGameOverInNMoves(1);

            robot.Run();

            moveCreator.VerifyAll();
            serverCommunications.Verify(sc => sc.Move(position));
        }

        [TestMethod]
        public void TengoQueLlamarAlSleeperAntesDeTirar()
        {
            SetupServerSendGameOverInNMoves(1);
            
            robot.Run();

            moveCreator.VerifyAll();
            timeSleeper.VerifyAll();
            serverCommunications.Verify(sc => sc.Move(position));
        }

        private void SetupServerSendGameOverInNMoves(int numberOfMovesToFail)
        {
            Queue<ServerStatus> serverResult = new Queue<ServerStatus>();
            for (int i = 0; i < numberOfMovesToFail-1; i++)
            {
                serverResult.Enqueue(ServerStatus.OK);
            }

            serverResult.Enqueue(ServerStatus.GAME_OVER);

            serverCommunications.Setup(sc => sc.Initialize(robotName)).Returns(ServerStatus.OK);
            serverCommunications.Setup(sc => sc.Move(position)).Returns(serverResult.Dequeue);
        }

    }
}
