using GGLoader.BLL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGLoader.Reports
{
    class GeneralReport : IGenerator
    {
        public Report Report { get; set; }
        public LoadTest LoadTest { get; set; }
        public Diagnostic Diagnostic { get; set; }
        public Log Log { get; set; }

        public void Excecute()
        {
            GenerateGeneralReport();
            new ReportGenerator("GeneralReport.txt").GenerateReport(Report);
        }

        private void GenerateGeneralReport()
        {
            var loadTest = LoadTest;
            var log = Log;
            var diagnostic = Diagnostic;

            var reportInformation = new Dictionary<string, string>();
            var reportGenerator = new ReportGenerator("GeneralReport.txt");
            var chartLines = new ChartLines();
            var tableProcessesBuilder = new StringBuilder();
            var orderesProcesses = diagnostic.Processes.OrderBy(p => p.Id).ToList();
            var processChart = new List<string>();
            var multilineChart = new List<string>();

            orderesProcesses.ForEach(p =>
            {
                var logLines = log.Lines.Where(l => l.ProcessId.Equals(p.Id)).ToList();
                var processResponse = new ProcessResponse(logLines, p.Id);
                var yaxisGraph = new List<string>();
                var processMessageCont = 0;

                tableProcessesBuilder.Append(reportGenerator.GenerateHeader(p.Id));
                tableProcessesBuilder.Append(reportGenerator.GenerateRow("Sended Messages :", (loadTest.MessagesByClient).ToString()));
                tableProcessesBuilder.Append(reportGenerator.GenerateRow("Recived Messages :", p.UnprocessedMessages.ToString()));
                tableProcessesBuilder.Append(reportGenerator.GenerateRow("Processed Messages:", p.ProcessedMessages.ToString()));
                tableProcessesBuilder.Append(reportGenerator.GenerateRow("Details:", String.Format("<a href=\"Report{0}.html\">{1}</a>", p.Id, "Click Here")));

                processResponse.Messages.ForEach(pA => { yaxisGraph.Add(reportGenerator.GetLinesProcessData(pA, processMessageCont++)); });
                multilineChart.Add(chartLines.CreateMultilineChart(string.Join(",", yaxisGraph), p.Id));
                processChart.Add(reportGenerator.GeneratechartData(processResponse));
            });


            reportInformation.Add("%SendedMessages%", loadTest.TotalSendedMessages.ToString());
            reportInformation.Add("%TotalProcesses%", loadTest.Clients.ToString());
            reportInformation.Add("%RecievedMessages%", log.RecievedMessages.ToString());
            reportInformation.Add("%ProcessedMessages%", log.ProcessedMessages.ToString());
            reportInformation.Add("%ProcessTable%", tableProcessesBuilder.ToString());
            reportInformation.Add("%ProcessChart%", string.Join(",", processChart));
            reportInformation.Add("%LineMessagesCalls%", string.Join(",", multilineChart));

            var report = new Report.Builder(loadTest.Id)
                .WithAttributes(reportInformation)
                .ByLoadTest(loadTest)
                .Build();
            Report = report;
        }
    }
}
