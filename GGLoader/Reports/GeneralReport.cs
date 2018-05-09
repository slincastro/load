using GGLoader.BLL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            new ReportWriter("GeneralReport.txt").Write(Report);
        }

        private void GenerateGeneralReport()
        {
            var loadTest = LoadTest;
            var log = Log;
            var diagnostic = Diagnostic;

            var reportInformation = new Dictionary<string, string>();
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

                tableProcessesBuilder.Append(GenerateHeader(p.Id));
                tableProcessesBuilder.Append(GenerateRow("Sended Messages :", (loadTest.MessagesByClient).ToString()));
                tableProcessesBuilder.Append(GenerateRow("Recived Messages :", p.UnprocessedMessages.ToString()));
                tableProcessesBuilder.Append(GenerateRow("Processed Messages:", p.ProcessedMessages.ToString()));
                tableProcessesBuilder.Append(GenerateRow("Details:", String.Format("<a href=\"Report{0}.html\">{1}</a>", p.Id, "Click Here")));

                processResponse.Messages.ForEach(pA => { yaxisGraph.Add(GetLinesProcessData(pA, processMessageCont++)); });
                multilineChart.Add(CreateMultilineChart(string.Join(",", yaxisGraph), p.Id));
                processChart.Add(GeneratechartData(processResponse));
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

        public string GetLinesProcessData(ProcessMessage process, int shootNumber)
        {
            return "{" + string.Format("x: {0}, y: {1} ", shootNumber.ToString(), Convert.ToInt32(process.ProcessingTime.TotalMilliseconds).ToString()) + "}";
        }

        public string GeneratechartData(ProcessResponse process)
        {
            return "{" + string.Format(" y: {0}, label: \"{1}\" ", process.AverageProcess, process.Id) + "}";
        }

        public string GenerateHeader(string name)
        {
            var header = new StringBuilder();
            header.Append("<tr>");
            header.Append(string.Format("<th>{0}</th>", name));
            header.Append("<th></th>");
            header.Append("</tr>");

            return header.ToString();
        }

        public string GenerateRow(string label, string value)
        {

            var row = new StringBuilder();
            row.Append("<tr>");
            row.Append(string.Format("<td>{0}</td>", label));
            row.Append(string.Format("<td>{0}</td>", value));
            row.Append("</tr> ");

            return row.ToString();
        }

        public string CreateMultilineChart(string data, string processName)
        {
            var lineBuilder = new StringBuilder();

            lineBuilder.Append("{");
            lineBuilder.Append("type: \"line\",");
            lineBuilder.Append("axisYType: \"secondary\",");
            lineBuilder.Append(string.Format("name: \"{0}\",", processName));
            lineBuilder.Append("showInLegend: true,");
            lineBuilder.Append("markerSize: 0,");
            lineBuilder.Append("yValueFormatString: \"$#,###k\",");
            lineBuilder.Append("dataPoints: [");
            lineBuilder.Append(data);
            lineBuilder.Append("]}");

            return lineBuilder.ToString();
        }
    }
}
