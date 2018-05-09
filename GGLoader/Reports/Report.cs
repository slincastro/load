using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GGLoader.BLL.Domain;

namespace GGLoader.Reports
{
    public class Report
    {
        private Report(string reportId)
        {
            Id = reportId;
        }

        public Dictionary<string,string> Attributes { get; set; }
        public int Clients { get; set; }
        public string FileNameEvidence { get; set; }
        public string Id { get; set; }
        public int MessagesByClient { get; set; }
        public string Path { get; set; }
        public int TotalSendedMessages { get; set; }

        public Builder NewBuilder(string reportId) { return new Builder(reportId); }

        public class Builder
        {
            private Report instance;

            public Builder(string reportId)
            {
                instance = new Report(reportId);
            }

            public Builder WithAttributes(Dictionary<string, string> attributes)
            {
                instance.Attributes = attributes;
                return this;
            }

            public Builder ByLoadTest(LoadTest loadTest)
            {
                instance.Path = loadTest.GetDiagnosticPath();
                instance.MessagesByClient = loadTest.MessagesByClient;
                instance.Clients = loadTest.Clients;
                instance.FileNameEvidence = loadTest.FileNameEvidence;
                instance.TotalSendedMessages = loadTest.TotalSendedMessages;
                return this;
            }

            public Report Build() { return instance; }

        }

        
    }
}
