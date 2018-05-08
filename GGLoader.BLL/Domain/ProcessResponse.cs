using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGLoader.BLL.Domain
{
    public class ProcessResponse
    {


        public ProcessResponse(List<Line> lines, string process)
        {
            Lines = lines;
            Id = process;

            Messages = lines.GroupBy(item => item.Id)
             .Select(group => new ProcessMessage
             {
                 Id = group.Key,
                 SendedMessage = group.FirstOrDefault(sm => !sm.IsProcessed),
                 ProcessedMessage = group.FirstOrDefault(sm => sm.IsProcessed),
                 ProcessingTime = GetTimeProcess(group)
             }).ToList();

            AverageProcess = Messages.Average(p => Convert.ToInt32(p.ProcessingTime.Milliseconds));
        }

        private TimeSpan GetTimeProcess(IGrouping<string, Line> group)
        {
            var sendedMessage = group.FirstOrDefault(sm => !sm.IsProcessed).TimeProcess;
            var processedMessage = group.FirstOrDefault(sm => sm.IsProcessed).TimeProcess;

            return processedMessage.Subtract(sendedMessage);
        }

        public string Id { get; set; }
        public List<Line> Lines { get; set; }
        public List<ProcessMessage> Messages { get; set; }
        public double AverageProcess { get; internal set; }
    }

    public class ProcessMessage
    {
        public string Id { get; set; }
        public TimeSpan ProcessingTime { get; set; }
        public Line SendedMessage { get; set; }
        public Line ProcessedMessage { get; set; }
        
    }


}
