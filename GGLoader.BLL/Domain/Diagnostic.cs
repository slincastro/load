using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGLoader.BLL.Domain
{
    public class Diagnostic
    {
        public Diagnostic(Log log,int messageSended)
        {
            MessageSended = messageSended;
            MessagesRecieved = log.RecievedMessages;
            MessagesProcessed = log.ProcessedMessages;
            TotalReadedLines = log.TotalReadedLines;
            CurrentProcessLines = log.CurrentProcessLines;
            
            var partialProcesses = log.Lines.GroupBy(l => new { l.ProcessId, l.IsProcessed })
                .Select(g => new PartialLogProcess {
                    Id = g.Key.ProcessId,
                    isProcessed = g.Key.IsProcessed,
                    MessageNumber = g.Count()
                });

            var partialProcessesStatus = log.Lines.GroupBy(l => l.ProcessId)
                .Select(g => 
                    new LogProcess
                    {
                        Id = g.Key,
                        Messages = g.Count()
                    }
                    ).ToList();

            TotalProcess = partialProcessesStatus.Count();

            partialProcessesStatus.ForEach(p =>
            {
                p.ProcessedMessages = partialProcesses.FirstOrDefault(pp => pp.Id.Equals(p.Id)).MessageNumber;
                p.UnprocessedMessages = p.Messages - p.ProcessedMessages;
            });

            Processes = partialProcessesStatus;

        }

        public int MessageSended { get; set; }
        public int MessagesRecieved { get; }
        public int MessagesProcessed { get; }
        public int TotalReadedLines { get; set; }
        public int CurrentProcessLines { get; set; }
        public int TotalProcess { get; set; }
        public List<LogProcess> Processes { get; set; }
    }

    public class PartialLogProcess
    {
        public string Id { get; set; }
        public int MessageNumber { get; set; }
        public bool isProcessed { get; set; }
    }

    public class LogProcess
    {
        public string Id { get; set; }
        public int Messages { get; set; }
        public int ProcessedMessages { get; set; }
        public int UnprocessedMessages { get; set; }
    }
}
