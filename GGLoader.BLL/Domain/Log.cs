using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGLoader.BLL.Domain
{
    public class Log
    {
        private List<Line> _lines;

        public int RecievedMessages { get; }
        public int ProcessedMessages { get; }
        public int TotalReadedLines { get; set; }
        public int CurrentProcessLines { get; set; }
        public List<Line> Lines { get; }

        public Log(List<string> log, string currentTestNumber = "")
        {
            TotalReadedLines = log.Count();

            if (!string.IsNullOrEmpty(currentTestNumber))
                log = log.Where(l => l.Contains(currentTestNumber)).ToList();

            _lines = new List<Line>();

            var cont = 0;
            log.ForEach(l => {
                var line = new Line(l);
                line.index = ""+cont++;
                _lines.Add(line);
            });


            if (!string.IsNullOrEmpty(currentTestNumber))
                _lines = _lines.Where(l => l.TestNumber.Equals(currentTestNumber)).ToList();

            Lines = _lines;
            CurrentProcessLines = _lines.Count();
            RecievedMessages = _lines.Count(l => !l.IsProcessed);
            ProcessedMessages = _lines.Count(l => l.IsProcessed);
        }




    }
}
