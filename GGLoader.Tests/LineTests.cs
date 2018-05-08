using GGLoader.BLL.Domain;
using NUnit.Framework;
using System;
using System.Globalization;

namespace GGLoader.Tests
{
    [TestFixture]
    class LineTest
    {
        [Test]
        public void ShouldReturnProcessIdAndMessaggeThreadWhenReadALogLine()
        {
            var logLine = "2018-05-01 10:28:58,250 [32] DEBUG Guggenheim.Service.Sentry - Received Msg:TestNumber: 1234, Shoot # 19549, Process: Process13, Port: 515, ID: e1beadfb-c8b7-41cb-9a6e-f26f9b26c9ca";
            var expectedProcess = "Process13";
            var expectedId = "e1beadfb-c8b7-41cb-9a6e-f26f9b26c9ca";
            var expectedTestNumber = "1234";
            
            DateTime expectedTimeProccess = DateTime.ParseExact("10:28:58.250", "HH:mm:ss.fff",
                                        CultureInfo.InvariantCulture);
            var lineProcessed = new Line(logLine);

            Assert.IsTrue(expectedProcess.Equals(lineProcessed.ProcessId));
            Assert.IsTrue(expectedId.Equals(lineProcessed.Id));
            Assert.IsFalse(lineProcessed.IsProcessed);
            Assert.IsTrue(expectedTestNumber.Equals(lineProcessed.TestNumber));
            Assert.IsTrue(expectedTimeProccess.Equals(lineProcessed.TimeProcess));
        }
    }
}
