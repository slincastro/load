using System;

namespace GGLoader.BLL.Domain
{
    public class LoadTest
    {
        private const string _pathFormat = "{0}\\{1}_clients{2}_messages_TestId-{3}\\";

        public LoadTest(string testId)
        {
            Id = testId;
        }

        public string AnalyzedLogPath { get; set; }
        public int Clients { get; set; }
        public string DestinationPath { get; set; }
        public string FileNameEvidence { get; set; }
        public string Id { get;}
        public int MessagesByClient { get; set; }
        public int TotalSendedMessages { get; set; }

        public Builder NewBuilder() { return new Builder(Id); }

        public string GetDiagnosticPath() {
            return string.Format(_pathFormat, DestinationPath, Clients, MessagesByClient, Id);
        }

        public class Builder {
            private LoadTest instance;

            public Builder(string testId) {
                instance = new LoadTest(testId);
            }

            public Builder WithMessagesByClient(int messagesByClient) {
                instance.MessagesByClient = messagesByClient;
                instance.TotalSendedMessages = instance.Clients > 0 ? instance.MessagesByClient * instance.Clients : 0;
                return this;
            }

            public Builder WithGeneratedFileName(string currentFileName)
            {
                instance.FileNameEvidence = currentFileName;
                return this;
            }

            public Builder WithAnalizedLogPath(string currentAnalyzedLogPath)
            {
                instance.AnalyzedLogPath = currentAnalyzedLogPath;
                return this;
            }

            public Builder WithDestinationPath(string currentPathDestinationLogs)
            {
                instance.DestinationPath = currentPathDestinationLogs;
                return this;
            }

            public Builder WithClients(int clients) {
                instance.Clients = clients;
                instance.TotalSendedMessages = instance.MessagesByClient > 0 ? instance.MessagesByClient * instance.Clients : 0;
                return this;
            }

            public LoadTest Build() { return instance; }
        }
    }
}
