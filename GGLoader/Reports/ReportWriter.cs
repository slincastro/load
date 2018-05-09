using GGLoader.BLL.Domain;
using GGLoader.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGLoader
{
    public class ReportWriter
    {
        private const string _layoutPathFormat = "{0}\\Layouts\\{1}";
        private const string _reportFormat = "Report{0}.html";
        private string _formatFileName;

        public ReportWriter(string formatFileName)
        {
            this._formatFileName = formatFileName;
        }

        public void Write(ReportInformation report)
        {
            var formatPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var logPath = string.Format(_layoutPathFormat, formatPath, _formatFileName);

            var lines = new FileReader().Read(logPath);
            var informationReport = new List<string>();

            lines.ForEach(l =>
            {
                var line = l;
                foreach (var remplaceItem in report.Attributes)
                {
                    line = line.Replace(remplaceItem.Key, remplaceItem.Value);
                }
                informationReport.Add(line);
            });

            new FileWriter().Write(report.Path + string.Format(_reportFormat, report.Id), informationReport);
        }

       
    }
}
