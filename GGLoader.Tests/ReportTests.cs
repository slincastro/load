using GGLoader.BLL.Domain;
using GGLoader.Reports;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGLoader.Tests
{
    [TestFixture,Category("Report")]
    class ReportTests
    {
        [Test]
        public void ShouldReturnReportParameters() {

            const string currentTestId = "LoadTest1";
            const int currentMessagesByClient = 5;
            const int clients = 10;
            const string currentPathDestinationLogs = @"C:\LoadLogs\";
            const string currentAnalyzedLogPath = @"C:\analyzedPath\";
            const string currentFileGenerated = "expectedLogFile";

            var loadTest = new LoadTest(currentTestId)
                    .NewBuilder()
                    .WithClients(clients)
                    .WithMessagesByClient(currentMessagesByClient)
                    .WithDestinationPath(currentPathDestinationLogs)
                    .WithAnalizedLogPath(currentAnalyzedLogPath)
                    .WithGeneratedFileName(currentFileGenerated)
                    .Build();

            var expectedAttributesNumber = 3;
            var currentReportId = "report1";

            var currentReportInformation = new Dictionary<string, string>();
            currentReportInformation.Add("%SendedMessages%", "SendedMessages");
            currentReportInformation.Add("%TotalProcesses%", "TotalProcesses");
            currentReportInformation.Add("%RecievedMessages%", "RecievedMessages");

            var report = new Report.Builder(currentReportId)
                .WithAttributes(currentReportInformation)
                .ByLoadTest(loadTest)
                .Build();

            var expectedReportId = "report1";
            const int expectedMessagesByClient = 5;
            const int expectedClients = 10;
            const int expectedTotalSendedMessages = 50;
            const string expectedPathDestinationLogs = @"C:\LoadLogs\";
            const string expectedFileGenerated = "expectedLogFile";

            Assert.AreEqual(expectedAttributesNumber, report.Attributes.Count);
            Assert.IsTrue(report.Path.Equals(expectedPathDestinationLogs));
            Assert.IsTrue(report.Id.Equals(expectedReportId));
            Assert.AreEqual(expectedClients, report.Clients);
            Assert.AreEqual(expectedMessagesByClient, report.MessagesByClient);
            Assert.AreEqual(expectedTotalSendedMessages, report.TotalSendedMessages);
            Assert.IsTrue(expectedPathDestinationLogs.Equals(report.Path));
            Assert.IsTrue(expectedFileGenerated.Equals(report.FileNameEvidence));


        }
    }
}
