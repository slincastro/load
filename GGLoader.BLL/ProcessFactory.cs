using GGLoader.BLL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GGLoader.BLL
{
    public class ProcessFactory
    {
        public List<Process> Processes { get; }

        public ProcessFactory(int quantity, int shoots)
        {
            var processes = new List<Process>();

            for (var i = 0; i < quantity; i++)
            {
                processes.Add(new Process { Name = "Process" + i, Port = 502 + i, Shoots = shoots });
            }

            Processes = processes;
        }

        public void Excecute(string testNumber)
        {
            var threads = new List<Thread>();

            var processes = Processes;

            processes.ForEach(p =>
            {
                var threadFire = new Thread(() => UDPClient.Fire(p, testNumber));
                threads.Add(threadFire);
                threadFire.Start();
            });

            threads.ForEach(t =>
            {
                t.Join();
            });
        }
    }
}
