using GGLoader.BLL.Domain;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGLoader.Tests
{
    [TestFixture,Category("Tests")]
    public class ProcessResponseTests
    {
        

        [Test]
        public void ShouldResponseInternalProcessByNumber() {
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


            const string currentTest = "123";
            var currentLog = new Log(log, currentTest);
            string process = "Process11";

            var lines = currentLog.Lines.Where(l => l.ProcessId.Equals(process)).ToList();

            var processResponse = new ProcessResponse(lines,process);

            Assert.IsTrue(processResponse.Lines.Count().Equals(2));
            Assert.IsTrue(processResponse.Id.Equals(process));
        }

        [Test]
        public void ShouldResponsetimeDiferenceInMessage()
        {
            var log = new List<string> {
                "2018-05-01 12:47:05,691[3] DEBUG Guggenheim.Service.Sentry - Received Msg: TestNumber:123, Shoot # 1, Process: Process11, Port: 518, ID: 9ce2175a-8539-4ab6-b756-31351ac6dbb5",
                "2018-05-01 12:47:05,692[15] DEBUG Guggenheim.Service.Sentry - Processing message: TestNumber:123, Shoot # 1, Process: Process11, Port: 513, ID: 9ce2175a-8539-4ab6-b756-31351ac6dbb5",
              };


            const string currentTest = "123";
            var currentLog = new Log(log, currentTest);
            string process = "Process11";

            var lines = currentLog.Lines.Where(l => l.ProcessId.Equals(process)).ToList();

            var processResponse = new ProcessResponse(lines, process);

            Assert.IsTrue(processResponse.Lines.Count().Equals(2));
            Assert.IsTrue(processResponse.Id.Equals(process));
        }

    }
}
