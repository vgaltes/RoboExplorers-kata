using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RoboExplorers.Test
{
    [TestClass]
    public class WaitSomeTimeTest
    {
        WaitSomeTime sleeper = new WaitSomeTime();

        [TestMethod]
        public void Espero200MilisegundosEntreLlamadas()
        {   
            sleeper.Sleep(200);
            Stopwatch timer = Stopwatch.StartNew();
            sleeper.Sleep(200);
            long elapsed = timer.ElapsedMilliseconds;

            Assert.IsTrue(elapsed > 200, string.Format("El tiempo de espera ha sido de {0} milisegundos", elapsed));
        }

        [TestMethod]
        public void SiHagoOperacionesQueDuranMasDe200MilisegundosEntreDosLlamadasElTiempoDeEsperaDebeSerInferiorAlTiempoDeLasOperacionesIntermediasMas20Milisegundos()
        {
            sleeper.Sleep(200);
            Stopwatch timer = Stopwatch.StartNew();
            System.Threading.Thread.Sleep(400);
            sleeper.Sleep(200);
            long elapsed = timer.ElapsedMilliseconds;

            Assert.IsTrue(elapsed <= 420, string.Format("El tiempo de espera ha sido de {0} milisegundos", elapsed));
        }
    }
}
