using GGLoader.BLL.Domain;
using GGLoader.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGLoader
{
    public class ReportGenerator
    {
        private string _formatFileName;

        public ReportGenerator(string formatFileName)
        {
            this._formatFileName = formatFileName;
        }

        public void GenerateReport(Report report)
        {
            var formatPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var logPath = string.Format("{0}\\Layouts\\{1}", formatPath, _formatFileName);

            var lines = new FileReader().Read(logPath);
            var informationReport = new List<string>();

            foreach (var item in lines)
            {
                var line = item;
                foreach (var remplaceItem in report.Attributes)
                {
                    line = line.Replace(remplaceItem.Key, remplaceItem.Value);
                }
                informationReport.Add(line);
            }

            new FileWriter().Write(report.Path + string.Format("Report{0}.html",report.Id), informationReport);
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
    }
}
