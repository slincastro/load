using GGLoader.BLL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GGLoader.Reports
{
    class ProcessReport : IGenerator
    {
        public Report Report { get; set; }

        public LoadTest LoadTest { get; set; }

        public Diagnostic Diagnostic { get; set; }

        public Log Log { get; set; }

        public void Excecute()
        {
            var reports = GenerateReportByProcess();

            reports.ForEach(report => { new ReportWriter("ReportProcessFormat.txt").Write(report); });
            
        }

        private List<Report> GenerateReportByProcess()
        {
            var loadTest = LoadTest;
            var log = Log;
            var currentDiagnostic = Diagnostic;

            var reports = new List<Report>();

            currentDiagnostic.Processes.ForEach(p =>
            {
                var logLines = log.Lines.Where(l => l.ProcessId.Equals(p.Id)).ToList();
                var processResponse = new ProcessResponse(logLines, p.Id);
                List<string> yaxisGraph = GetFormatedData(processResponse);
                var reportInformation = new Dictionary<string, string>();

                reportInformation.Add("%RenderPoints%", string.Join(",", yaxisGraph));
                reportInformation.Add("%GraphTitle%", string.Format("Performance Graphic {0} with , {1} messages", p.Id, p.Messages));
                reportInformation.Add("%SendedMessages%", p.UnprocessedMessages.ToString());
                reportInformation.Add("%RecievedMessages%", p.ProcessedMessages.ToString());
                reportInformation.Add("%ProcessedMessages%", p.UnprocessedMessages.ToString());
                reportInformation.Add("%AverageTimeResponse%", processResponse.AverageProcess.ToString() + " ms");

                var report = new Report.Builder(p.Id)
                .WithAttributes(reportInformation)
                .ByLoadTest(loadTest)
                .Build();

                reports.Add( report );
            });

            return reports;
        }

        private static List<string> GetFormatedData(ProcessResponse processResponse)
        {
            var yaxisGraph = new List<string>();
            processResponse.Messages.ForEach(pA => { yaxisGraph.Add(GetSecondsProcces(pA.ProcessingTime)); });
            return yaxisGraph;
        }


        public static string GetSecondsProcces(TimeSpan processTime)
        {
            int miliseconds = Convert.ToInt32(processTime.TotalMilliseconds);

            return "{" + string.Format(" y:{0} ", miliseconds + "") + "}";
        }


    }
}
