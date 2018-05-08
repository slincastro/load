using GGLoader.BLL;
using GGLoader.BLL.Domain;
using GGLoader.Drawing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace GGLoader
{
    class Program
    {
        static void Main(string[] args)
        {
            var finish = "Y";
            do
            {

                var testNumber = Guid.NewGuid().ToString();
                const string logPath = @"C:\logs\Guggenheim.Sentry.log";
                var destinationPath = @"C:\LoadLogs\";
                string fileName = string.Format(@"LogEvidence{0}.txt", testNumber);
                Console.Write("Thread quantities: ");
                var processQuantities = short.Parse(Console.ReadLine());
                Console.Write("Shoot quantities: ");
                var shootQuantity = int.Parse(Console.ReadLine());

                GenerateCalls(testNumber, processQuantities, shootQuantity);
                ProcessLog(testNumber, logPath, destinationPath, fileName, processQuantities, shootQuantity);

                Console.WriteLine("FINISH (y)?:");
                finish = Console.ReadLine();

            } while (!finish.ToUpper().Equals("Y"));
        }

        private static void Wait(int processQuantities, int shootQuantity)
        {
            Console.WriteLine(" waiting for the client !!");

            var cursorLeft = Console.CursorLeft;
            var cursorTop = Console.CursorTop;

            var calculatedSleepTime = (4 * processQuantities * shootQuantity) / 10;
            const int defaultTime = 50000;
            var sleepTime = calculatedSleepTime < 50000 ? defaultTime : calculatedSleepTime;

            Clock(cursorLeft, cursorTop, sleepTime);

        }

        private static void ProcessLog(string testNumber, string logPath, string destinationPath, string fileName, int processQuantities, int shootQuantity)
        {
            Console.WriteLine("Processing Log !");
            var cursorLeft = Console.CursorLeft;
            var cursorTop = Console.CursorTop;
            var spinner1 = new ConsoleWaiter(cursorLeft, cursorTop, 400);

            spinner1.Start();
            var log = FileProcesser.ProcessFile(testNumber, logPath, destinationPath,fileName, processQuantities, shootQuantity);
            spinner1.Stop();

            Console.WriteLine("CHECKING FILE:");
            Console.WriteLine("Received Messages:  " + log.RecievedMessages);
            Console.WriteLine("Processed Messages: " + log.ProcessedMessages);
            Console.WriteLine("In QUEUE: " + log.TotalReadedLines);
        }

        private static void GenerateCalls(string testNumber, int processQuantities, int shootQuantity)
        {
            var animation = new List<string>
            { "<(O_o )>",
                "^(O.o )>",
                "(O.o )>;= ",
                "(O.o )>;= -",
                "(O.o )>;=   -",
                "(O.o )>;= -   -",
                "(O.o )>;= -   -   -",
                "(O.o )>;= -   -   -   -",
                "(O.o )>;= -   -   -   -   -",
                "(O.o )>;= -   -   -   -   -   -",
                "(O.o )>;=     -   -   -   -   -   *",
                "(O.o )>;=         -   -   -   -   *",
                "(O.o )>;=             -   -   -   *",
                "(O.o )>;=                     -   *",
                "(O.o )>;=~                         ",
            };

            var cursorLeft = Console.CursorLeft;
            var cursorTop = Console.CursorTop;


            var factory = new ProcessFactory(processQuantities, shootQuantity);
            var processes = factory.Processes;
            cursorLeft = Console.CursorLeft;
            cursorTop = Console.CursorTop;
            var waiter = new ConsoleWaiter(cursorLeft, cursorTop, 400, animation);

            waiter.Start();

            factory.Excecute(testNumber);

            waiter.Stop();

            Console.WriteLine("All threads are complete !!");

            Wait(processQuantities, shootQuantity);
        }

        public static void Clock(int cursorLeft, int cursorTop, int topTime)
        {

            var cont = 0;
            topTime = topTime / 1000;
            for (int a = topTime; a >= 0; a--)
            {
                TimeSpan time = TimeSpan.FromSeconds(topTime - cont);
                Console.SetCursorPosition(cursorLeft, cursorTop + 1);
                Console.Write("{0} ", time.ToString(@"hh\:mm\:ss"));    // Add space to make sure to override previous contents
                System.Threading.Thread.Sleep(1000);
                cont++;
            }

            Console.WriteLine("------------------------------------------------------------");
        }




    }
}
