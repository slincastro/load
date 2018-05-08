using GGLoader.BLL.Domain;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;

namespace GGLoader.Tests
{
    [TestFixture]
    class LogTest
    {
        [Test]
        public void ShouldCount3ProcesedMessagesBy5Sent()
        {
            var log = new List<string> {
                "2018-05-01 12:47:05,691[3] DEBUG Guggenheim.Service.Sentry - Received Msg: Shoot # 1, Process: Process16, Port: 518, ID: 9ce2175a-8539-4ab6-b756-31351ac6dbb5",
                "2018 - 05 - 01 12:47:05,692[15] DEBUG Guggenheim.Service.Sentry - Received Msg: Shoot # 1, Process: Process11, Port: 513, ID: ac3196b4-b639-4616-b170-5019d7ca1f07",
                "2018 - 05 - 01 12:47:05,692[3] DEBUG Guggenheim.Service.Sentry - Received Msg: Shoot # 1, Process: Process10, Port: 512, ID: be76be40-cac8-41b1-b0f9-a948b23c2d31",
                "2018 - 05 - 01 12:47:05,692[21] DEBUG Guggenheim.Service.Sentry - Received Msg: Shoot # 1, Process: Process17, Port: 519, ID: 7c43711d-2391-43b9-9719-85f377c16813",
                "2018 - 05 - 01 12:47:05,692[3] DEBUG Guggenheim.Service.Sentry - Received Msg: Shoot # 1, Process: Process1, Port: 503, ID: c641b9d1-7bb0-43a7-8d63-80faefe64ffb",
                "2018-05-01 12:47:05,697 [19] DEBUG Guggenheim.Service.Sentry - Processing message:Shoot # 1, Process: Process11, Port: 518, ID: ac3196b4-b639-4616-b170-5019d7ca1f07",
                "2018-05-01 12:47:05,697 [19] DEBUG Guggenheim.Service.Sentry - Processing message:Shoot # 1, Process: Process10, Port: 518, ID: be76be40-cac8-41b1-b0f9-a948b23c2d31",
                "2018-05-01 12:47:05,697 [19] DEBUG Guggenheim.Service.Sentry - Processing message:Shoot # 1, Process: Process16, Port: 518, ID: 9ce2175a-8539-4ab6-b756-31351ac6dbb5"
            };

            var expectedProcessed = 3;
            var expectedRecieved = 5;
            var expectedLines = 8;
            var currentLog = new Log(log);

            Assert.AreEqual(expectedProcessed, currentLog.ProcessedMessages);
            Assert.AreEqual(expectedRecieved, currentLog.RecievedMessages);
            Assert.AreEqual(expectedLines, currentLog.TotalReadedLines);
        }

        [Test]
        public void ShouldCount3ProcesedMessagesBy10SentOfTheSameTest()
        {
            var log = new List<string> {
                "2018-05-01 12:47:05,691[3] DEBUG Guggenheim.Service.Sentry - Received Msg: TestNumber:123, Shoot # 1, Process: Process16, Port: 518, ID: 9ce2175a-8539-4ab6-b756-31351ac6dbb5",
                "2018-05-01 12:47:05,692[15] DEBUG Guggenheim.Service.Sentry - Received Msg: TestNumber:123, Shoot # 1, Process: Process11, Port: 513, ID: ac3196b4-b639-4616-b170-5019d7ca1f07",
                "2018-05-01 12:47:05,692[3] DEBUG Guggenheim.Service.Sentry - Received Msg: TestNumber:123, Shoot # 1, Process: Process10, Port: 512, ID: be76be40-cac8-41b1-b0f9-a948b23c2d31",
                "2018-05-01 12:47:05,692[21] DEBUG Guggenheim.Service.Sentry - Received Msg: TestNumber:123, Shoot # 1, Process: Process17, Port: 519, ID: 7c43711d-2391-43b9-9719-85f377c16813",
                "2018-05-01 12:47:05,692[3] DEBUG Guggenheim.Service.Sentry - Received Msg: TestNumber:123, Shoot # 1, Process: Process1, Port: 503, ID: c641b9d1-7bb0-43a7-8d63-80faefe64ffb",
                "2018-05-01 12:47:05,697 [19] DEBUG Guggenheim.Service.Sentry - Processing message: TestNumber:123, Shoot # 1, Process: Process11, Port: 518, ID: ac3196b4-b639-4616-b170-5019d7ca1f07",
                "2018-05-01 12:47:05,697 [19] DEBUG Guggenheim.Service.Sentry - Processing message:TestNumber:123, Shoot # 1, Process: Process10, Port: 518, ID: be76be40-cac8-41b1-b0f9-a948b23c2d31",
                "2018-05-01 12:47:05,697 [19] DEBUG Guggenheim.Service.Sentry - Processing message: TestNumber:123, Shoot # 1, Process: Process16, Port: 518, ID: 9ce2175a-8539-4ab6-b756-31351ac6dbb5",
                "2018-05-01 12:47:05,691[3] DEBUG Guggenheim.Service.Sentry - Received Msg: TestNumber:44123, Shoot # 1, Process: Process16, Port: 518, ID: 9ce2175a-8539-4ab6-b756-31351ac6dbb5",
                "2018-05-01 12:47:05,692[15] DEBUG Guggenheim.Service.Sentry - Received Msg: TestNumber:44123, Shoot # 1, Process: Process11, Port: 513, ID: ac3196b4-b639-4616-b170-5019d7ca1f07",
                "2018-05-01 12:47:05,692[3] DEBUG Guggenheim.Service.Sentry - Received Msg: TestNumber:44123, Shoot # 1, Process: Process10, Port: 512, ID: be76be40-cac8-41b1-b0f9-a948b23c2d31",
                "2018-05-01 12:47:05,692[21] DEBUG Guggenheim.Service.Sentry - Received Msg: TestNumber:44123, Shoot # 1, Process: Process17, Port: 519, ID: 7c43711d-2391-43b9-9719-85f377c16813",
                "2018-05-01 12:47:05,692[3] DEBUG Guggenheim.Service.Sentry - Received Msg: TestNumber:44123, Shoot # 1, Process: Process1, Port: 503, ID: c641b9d1-7bb0-43a7-8d63-80faefe64ffb",
                "2018-05-01 12:47:05,697 [19] DEBUG Guggenheim.Service.Sentry - Processing message: TestNumber:44123, Shoot # 1, Process: Process11, Port: 518, ID: ac3196b4-b639-4616-b170-5019d7ca1f07",
                "2018-05-01 12:47:05,697 [19] DEBUG Guggenheim.Service.Sentry - Processing message: TestNumber:44123, Shoot # 1, Process: Process10, Port: 518, ID: be76be40-cac8-41b1-b0f9-a948b23c2d31",
                "2018-05-01 12:47:05,697 [19] DEBUG Guggenheim.Service.Sentry - Processing message:TestNumber:44123, Shoot # 1, Process: Process16, Port: 518, ID: 9ce2175a-8539-4ab6-b756-31351ac6dbb5"
            };

            var expectedProcessed = 3;
            var expectedRecieved = 5;
            var expectedLines = 16;
            var expectedCurrenProcessLines = 8;
            var currentTestNumber = "123";
            var currentLog = new Log(log, currentTestNumber);

            Assert.AreEqual(expectedProcessed, currentLog.ProcessedMessages);
            Assert.AreEqual(expectedRecieved, currentLog.RecievedMessages);
            Assert.AreEqual(expectedCurrenProcessLines, currentLog.CurrentProcessLines);
            Assert.AreEqual(expectedLines, currentLog.TotalReadedLines);
        }


        [Explicit]
        [Test]
        public void ProccessLogs()
        {

            var path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var logPath = string.Format("{0}\\testLog.txt", path);
            const string currentTest = "d3779f03-85f0-4610-a7bd-a2931c6d1eb5";
            var lines = new FileReader().Read(logPath);

            var log = new Log(lines, currentTest);

            string json = JsonConvert.SerializeObject(log);

            //write string to file
            System.IO.File.WriteAllText(@"C:\LoadLogs\path.txt", json);


        }
    }
}
