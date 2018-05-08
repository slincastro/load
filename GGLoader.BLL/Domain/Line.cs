using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGLoader.BLL.Domain
{
    public class Line
    {

        public string index { get; set; } // agregar indice de procesamiento
        public string Id { get; set; }
        public string ProcessId { get; set; }
        public bool IsProcessed { get; set; }
        public string TestNumber { get; set; }

        public string Content { get; }
        public DateTime TimeProcess { get; set; } //comparar el tiempo de procesamiento

        public Line(string logLine)
        {
            TestNumber = string.Empty;
            Content = logLine;

            if (!logLine.Contains("Received Msg:") && !logLine.Contains("Processing message:"))
                return;

            ExtractAtributtes(logLine);
        }

        private void ExtractAtributtes(string line)
        {
            const string idLabel = "ID:";
            const string testNumberLabel = "TestNumber:";
            const string processLabel = "Process:";

            IsProcessed = line.Contains("Processing message:");
            TimeProcess = GetTimeProcess(line);
            var lineAttributes = line.Split(',');

            lineAttributes.ToList().ForEach(atr =>
            {
                if (atr.Contains(processLabel))
                    ProcessId = atr.Replace(processLabel, string.Empty).Trim();
                if (atr.Contains(idLabel))
                    Id = atr.Replace(idLabel, string.Empty).Trim();
                if (atr.Contains(testNumberLabel))
                {
                    var atribute = atr.Split(new string[] { "TestNumber:" }, StringSplitOptions.None);
                    if (atribute.Length == 1)
                        return;
                    TestNumber = atribute[1].Replace(testNumberLabel, string.Empty).Trim();
                }



            });
        }

        private DateTime GetTimeProcess(string line)
        {
            var stringTimeProcess = line.Substring(11, 12).Replace(",", ".");
            return DateTime.ParseExact(stringTimeProcess, "HH:mm:ss.fff",
                                        CultureInfo.InvariantCulture);
        }
    }
}
