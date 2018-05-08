using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGLoader
{
    class ReportGenerator
    {
        private string _formatFileName;

        public ReportGenerator(string formatFileName)
        {
            this._formatFileName = formatFileName;
        }

        public void GenerateReport(Dictionary<string, string> data, string path,string process)
        {
            var formatPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var logPath = string.Format("{0}\\Layouts\\{1}", formatPath, _formatFileName);

            var lines = new FileReader().Read(logPath);
            var report = new List<string>();

            foreach (var item in lines)
            {
                var line = item;
                foreach (var remplaceItem in data)
                {
                    line = line.Replace(remplaceItem.Key, remplaceItem.Value);
                }
                report.Add(line);
            }

            new FileWriter().Write(path + string.Format("Report{0}.html",process), report);
        }
    }
}
