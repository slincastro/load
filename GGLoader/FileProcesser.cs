using GGLoader.BLL.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGLoader
{
    class FileProcesser
    {
        public static Log ProcessFile(string currentTestId, string pathFile, string pathDestination, string fileName, int processQuantities, int shootQuantity)
        {

            var lines = new FileReader().Read(pathFile);
            var log = new Log(lines, currentTestId);

            var currentSendedMessages = processQuantities * shootQuantity;

            var curentDiagnostic = new Diagnostic(log, currentSendedMessages);

            var diagnosticPath = string.Format("{0}\\{1}\\", pathDestination, currentTestId);
            var createFolderDirectory = Directory.CreateDirectory(diagnosticPath);

            GenerateReportByProcess(processQuantities, log, shootQuantity, diagnosticPath, curentDiagnostic);

            GenerateGeneralReport(currentTestId, log, currentSendedMessages, processQuantities, diagnosticPath, curentDiagnostic);

            return log;
        }

        private static void GenerateGeneralReport(string currentTestId, Log log, int totalSendedMessages, int totalProcesses, string diagnosticPath, Diagnostic diagnostic)
        {
            var reportInformation = new Dictionary<string, string>();

            var tableProcesses = new StringBuilder();
            
            diagnostic.Processes.OrderBy(p=> p.Id).ToList().ForEach(p => 
            {
                tableProcesses.Append(GenerateHeader(p.Id));
                tableProcesses.Append(GenerateRow("Sended Messages :", (totalSendedMessages/totalProcesses).ToString()));
                tableProcesses.Append(GenerateRow("Recived Messages :",p.UnprocessedMessages.ToString()));
                tableProcesses.Append(GenerateRow("Processed Messages:", p.ProcessedMessages.ToString()));
                tableProcesses.Append(GenerateRow("Details:", String.Format("<a href=\"Report{0}.html\">{1}</a>",p.Id,"Click Here")));
            });

            reportInformation.Add("%SendedMessages%", totalSendedMessages.ToString());
            reportInformation.Add("%TotalProcesses%", totalProcesses.ToString());
            reportInformation.Add("%RecievedMessages%", log.RecievedMessages.ToString());
            reportInformation.Add("%ProcessedMessages%", log.ProcessedMessages.ToString());
            reportInformation.Add("%ProcessTable%", tableProcesses.ToString());

            var processChart = new List<string>();
            diagnostic.Processes.ForEach(p =>
            {
                var logLines = log.Lines.Where(l => l.ProcessId.Equals(p.Id)).ToList();
                var processResponse = new ProcessResponse(logLines, p.Id);
                
                 processChart.Add(GeneratechartData(processResponse));

            });
            reportInformation.Add("%ProcessChart%", string.Join(",", processChart));
            

            new ReportGenerator("GeneralReport.txt").GenerateReport(reportInformation, diagnosticPath, currentTestId);
        }

        private static string GeneratechartData(ProcessResponse process)
        {
            return "{"+string.Format(" y: {0}, label: \"{1}\" ",process.AverageProcess,process.Id)+"}"; 
        }

        private static void GenerateReportByProcess(int processQuantities, Log log, int currentSendedMessages, string diagnosticPath, Diagnostic currentDiagnostic)
        {

            currentDiagnostic.Processes.ForEach(p =>
            {
                var logLines = log.Lines.Where(l => l.ProcessId.Equals(p.Id)).ToList();
                var processResponse = new ProcessResponse(logLines, p.Id);
                var yaxisGraph = new List<string>();
                processResponse.Messages.ForEach(pA => { yaxisGraph.Add(GetSecondsProcces(pA.ProcessingTime)); });
                GenerateProcessReport(p, currentSendedMessages, yaxisGraph, diagnosticPath, processResponse);
            });

        }

        private static void GenerateProcessReport(LogProcess process, int currentSendedMessages, List<string> yaxisGraph, string diagnosticPath, ProcessResponse processResponse)
        {
            var reportInformation = new Dictionary<string, string>();

            reportInformation.Add("%RenderPoints%", string.Join(",", yaxisGraph));
            reportInformation.Add("%GraphTitle%", string.Format("Performance Graphic {0} with , {1} messages", process.Id, currentSendedMessages));
            reportInformation.Add("%SendedMessages%", currentSendedMessages.ToString());
            reportInformation.Add("%RecievedMessages%", process.ProcessedMessages.ToString());
            reportInformation.Add("%ProcessedMessages%", process.UnprocessedMessages.ToString());
            reportInformation.Add("%AverageTimeResponse%", processResponse.AverageProcess.ToString() + " ms");

            new ReportGenerator("ReportProcessFormat.txt").GenerateReport(reportInformation, diagnosticPath, process.Id);
        }

        public static string GetSecondsProcces(TimeSpan processTime)
        {
            int miliseconds = Convert.ToInt32(processTime.TotalMilliseconds);

            return "{" + string.Format(" y:{0} ", miliseconds + "") + "}";
        }

        public static string GenerateHeader(string name)
        {
            var header = new StringBuilder();
            header.Append("<tr>");
            header.Append(string.Format("<th>{0}</th>",name));
            header.Append("<th></th>");
            header.Append("</tr>");

            return header.ToString();
        }

        public static string GenerateRow(string label, string value)
        {
           
            var row = new StringBuilder();
            row.Append("<tr>");
            row.Append(String.Format("<td>{0}</td>",label));
            row.Append(String.Format("<td>{0}</td>", value));
            row.Append("</tr> ");

            return row.ToString();
        }
        
    }



}
