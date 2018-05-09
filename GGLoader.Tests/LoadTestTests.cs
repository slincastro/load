using GGLoader.BLL.Domain;
using NUnit.Framework;

namespace GGLoader.Tests
{
    [TestFixture, Category("Load Tests")]
    class LoadTestTests
    {
        [Test]
        public void ShouldReturnLoadTestInformation()
        {
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

            const string expectedTestId = "LoadTest1";
            const int expectedMessagesByClient = 5;
            const int expectedClients = 10;
            const int expectedTotalSendedMessages = 50;
            const string expectedPathDestinationLogs = @"C:\LoadLogs\";
            const string expectedAnalyzedLogPath = @"C:\analyzedPath\";
            const string expectedFileGenerated = "expectedLogFile";

            Assert.AreEqual(expectedTestId, loadTest.Id);
            Assert.AreEqual(expectedClients, loadTest.Clients);
            Assert.AreEqual(expectedMessagesByClient, loadTest.MessagesByClient);
            Assert.AreEqual(expectedTotalSendedMessages, loadTest.TotalSendedMessages);
            Assert.IsTrue(expectedPathDestinationLogs.Equals(loadTest.DestinationPath));
            Assert.IsTrue(expectedAnalyzedLogPath.Equals(loadTest.AnalyzedLogPath));
            Assert.IsTrue(expectedFileGenerated.Equals(loadTest.FileNameEvidence));
        }
    }
}
