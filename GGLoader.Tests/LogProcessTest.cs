using GGLoader.BLL.Domain;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace GGLoader.Tests
{
    [TestFixture]
    class LogProcessTest
    {
        [Test]
        public void ShouldCount3DiferentGroupsBy10SentOfTheSameTest()
        {
            var logLines = new List<string> {
                "2018-05-02 15:55:15,635[3] DEBUG Guggenheim.Service.Sentry - Received Msg: TestNumber: cc8cf5bc - e801 - 4f68 - 928a - e1176e80d403, Shoot # 1, Process: Process2, Port: 504, ID: 615ff83c-0e14-4674-9c4a-7ad4b37a3ec1",
                "2018 - 05 - 02 15:55:15,636[27] DEBUG Guggenheim.Service.Sentry - Received Msg: TestNumber: cc8cf5bc - e801 - 4f68 - 928a - e1176e80d403, Shoot # 1, Process: Process0, Port: 502, ID: 53edeb7f-6b60-4c39-86d2-8b232866dd01",
                "2018 - 05 - 02 15:55:15,636[28] DEBUG Guggenheim.Service.Sentry - Received Msg: TestNumber: cc8cf5bc - e801 - 4f68 - 928a - e1176e80d403, Shoot # 1, Process: Process1, Port: 503, ID: 4e360588-1761-4e19-b362-06ca72aa2b80",
                "2018 - 05 - 02 15:55:15,636[27] DEBUG Guggenheim.Service.Sentry - Received Msg: TestNumber: cc8cf5bc - e801 - 4f68 - 928a - e1176e80d403, Shoot # 2, Process: Process2, Port: 504, ID: 73f5fc09-797c-4c17-9692-1d914bcd1ca4",
                "2018 - 05 - 02 15:55:15,636[28] DEBUG Guggenheim.Service.Sentry - Received Msg: TestNumber: cc8cf5bc - e801 - 4f68 - 928a - e1176e80d403, Shoot # 2, Process: Process1, Port: 503, ID: 3c02ab18-f30f-4ea7-8a96-06d1a1bd9bdb",
                "2018 - 05 - 02 15:55:15,636[27] DEBUG Guggenheim.Service.Sentry - Received Msg: TestNumber: cc8cf5bc - e801 - 4f68 - 928a - e1176e80d403, Shoot # 2, Process: Process0, Port: 502, ID: e38169de-4fa1-47ae-bd48-5bff74aee0d6",
                "2018 - 05 - 02 15:55:15,636[28] DEBUG Guggenheim.Service.Sentry - Received Msg: TestNumber: cc8cf5bc - e801 - 4f68 - 928a - e1176e80d403, Shoot # 3, Process: Process2, Port: 504, ID: 09c9017d-d93d-48d1-8b96-4a59ac381d64",
                "2018 - 05 - 02 15:55:15,636[27] DEBUG Guggenheim.Service.Sentry - Received Msg: TestNumber: cc8cf5bc - e801 - 4f68 - 928a - e1176e80d403, Shoot # 3, Process: Process1, Port: 503, ID: c476c1a9-d2b0-4a0a-b85c-889349cdb56e",
                "2018 - 05 - 02 15:55:15,636[28] DEBUG Guggenheim.Service.Sentry - Received Msg: TestNumber: cc8cf5bc - e801 - 4f68 - 928a - e1176e80d403, Shoot # 3, Process: Process0, Port: 502, ID: 277c93b2-d1de-453a-b50c-9e7ea22acf5c",
                "2018 - 05 - 02 15:55:15,636[19] DEBUG Guggenheim.Service.Sentry - Processing message: TestNumber: cc8cf5bc - e801 - 4f68 - 928a - e1176e80d403, Shoot # 1, Process: Process2, Port: 504, ID: 615ff83c-0e14-4674-9c4a-7ad4b37a3ec1",
                "2018 - 05 - 02 15:55:15,636[27] DEBUG Guggenheim.Service.Sentry - Received Msg: TestNumber: cc8cf5bc - e801 - 4f68 - 928a - e1176e80d403, Shoot # 4, Process: Process2, Port: 504, ID: 480223b7-8cf1-4e74-b6dc-3514d3949e85",
                "2018 - 05 - 02 15:55:15,636[28] DEBUG Guggenheim.Service.Sentry - Received Msg: TestNumber: cc8cf5bc - e801 - 4f68 - 928a - e1176e80d403, Shoot # 4, Process: Process1, Port: 503, ID: 078316fa-cd05-44fc-a9f3-f853e57816fe",
                "2018 - 05 - 02 15:55:15,636[27] DEBUG Guggenheim.Service.Sentry - Received Msg: TestNumber: cc8cf5bc - e801 - 4f68 - 928a - e1176e80d403, Shoot # 4, Process: Process0, Port: 502, ID: c9a83a89-9ffa-4dac-9d0e-aec3a2c44692",
                "2018 - 05 - 02 15:55:15,637[19] DEBUG Guggenheim.Service.Sentry - Processing message: TestNumber: cc8cf5bc - e801 - 4f68 - 928a - e1176e80d403, Shoot # 1, Process: Process0, Port: 502, ID: 53edeb7f-6b60-4c39-86d2-8b232866dd01",
                "2018 - 05 - 02 15:55:15,637[19] DEBUG Guggenheim.Service.Sentry - Processing message: TestNumber: cc8cf5bc - e801 - 4f68 - 928a - e1176e80d403, Shoot # 1, Process: Process1, Port: 503, ID: 4e360588-1761-4e19-b362-06ca72aa2b80",
                "2018 - 05 - 02 15:55:15,639[19] DEBUG Guggenheim.Service.Sentry - Processing message: TestNumber: cc8cf5bc - e801 - 4f68 - 928a - e1176e80d403, Shoot # 2, Process: Process2, Port: 504, ID: 73f5fc09-797c-4c17-9692-1d914bcd1ca4",
                "2018 - 05 - 02 15:55:15,640[19] DEBUG Guggenheim.Service.Sentry - Processing message: TestNumber: cc8cf5bc - e801 - 4f68 - 928a - e1176e80d403, Shoot # 2, Process: Process1, Port: 503, ID: 3c02ab18-f30f-4ea7-8a96-06d1a1bd9bdb",
                "2018 - 05 - 02 15:55:15,640[19] DEBUG Guggenheim.Service.Sentry - Processing message: TestNumber: cc8cf5bc - e801 - 4f68 - 928a - e1176e80d403, Shoot # 2, Process: Process0, Port: 502, ID: e38169de-4fa1-47ae-bd48-5bff74aee0d6",
                "2018 - 05 - 02 15:55:15,641[19] DEBUG Guggenheim.Service.Sentry - Processing message: TestNumber: cc8cf5bc - e801 - 4f68 - 928a - e1176e80d403, Shoot # 3, Process: Process2, Port: 504, ID: 09c9017d-d93d-48d1-8b96-4a59ac381d64",
                "2018 - 05 - 02 15:55:15,641[19] DEBUG Guggenheim.Service.Sentry - Processing message: TestNumber: cc8cf5bc - e801 - 4f68 - 928a - e1176e80d403, Shoot # 3, Process: Process1, Port: 503, ID: c476c1a9-d2b0-4a0a-b85c-889349cdb56e",
                "2018 - 05 - 02 15:55:15,641[19] DEBUG Guggenheim.Service.Sentry - Processing message: TestNumber: cc8cf5bc - e801 - 4f68 - 928a - e1176e80d403, Shoot # 3, Process: Process0, Port: 502, ID: 277c93b2-d1de-453a-b50c-9e7ea22acf5c",
                "2018 - 05 - 02 15:55:15,642[19] DEBUG Guggenheim.Service.Sentry - Processing message: TestNumber: cc8cf5bc - e801 - 4f68 - 928a - e1176e80d403, Shoot # 4, Process: Process2, Port: 504, ID: 480223b7-8cf1-4e74-b6dc-3514d3949e85",
                "2018 - 05 - 02 15:55:15,642[19] DEBUG Guggenheim.Service.Sentry - Processing message: TestNumber: cc8cf5bc - e801 - 4f68 - 928a - e1176e80d403, Shoot # 4, Process: Process1, Port: 503, ID: 078316fa-cd05-44fc-a9f3-f853e57816fe",
                "2018 - 05 - 02 15:55:15,643[19] DEBUG Guggenheim.Service.Sentry - Processing message: TestNumber: cc8cf5bc - e801 - 4f68 - 928a - e1176e80d403, Shoot # 4, Process: Process0, Port: 502, ID: c9a83a89-9ffa-4dac-9d0e-aec3a2c44692"
            };

            var currentTestNumber = "cc8cf5bc - e801 - 4f68 - 928a - e1176e80d403";
            var currentLog = new Log(logLines, currentTestNumber);
            var expectedGroups = 3;
            const int messagesSended = 0;

            var curentDiagnostic = new Diagnostic(currentLog, messagesSended);

            Assert.AreEqual(expectedGroups, curentDiagnostic.Processes.Count());
        }
    }
}
