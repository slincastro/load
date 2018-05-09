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
        private string _formatFileName;

        public ReportWriter(string formatFileName)
        {
            this._formatFileName = formatFileName;
        }

        public void Write(Report report)
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

       
    }
}
