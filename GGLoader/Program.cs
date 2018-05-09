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
                

                const string analizedLogPath = @"C:\logs\Guggenheim.Sentry.log";
                var destinationPath = @"C:\LoadLogs\";
                string evidenceFileName = string.Format(@"LogEvidence{0}.txt", testNumber);

                Console.Write("Thread quantities: ");
                var clientsNumber = Int32.Parse(Console.ReadLine());
                Console.Write("Shoot quantities: ");
                var messagesNumber = int.Parse(Console.ReadLine());

                var currentTest = new LoadTest(testNumber)
                    .NewBuilder()
                    .WithClients(clientsNumber)
                    .WithMessagesByClient(messagesNumber)
                    .WithDestinationPath(destinationPath)
                    .WithAnalizedLogPath(analizedLogPath)
                    .WithGeneratedFileName(evidenceFileName)
                    .Build();

                GenerateCalls(currentTest);
                ProcessLog(currentTest);

                Console.WriteLine("FINISH (y)?:");
                finish = Console.ReadLine();

            } while (!finish.ToUpper().Equals("Y"));
        }

        private static void Wait(int totalSendedMessages)
        {
            Console.WriteLine(" waiting for the client !!");

            var cursorLeft = Console.CursorLeft;
            var cursorTop = Console.CursorTop;

            var calculatedSleepTime = (4 * totalSendedMessages) / 10;
            const int defaultTime = 50000;
            var sleepTime = calculatedSleepTime < 50000 ? defaultTime : calculatedSleepTime;

            Clock(cursorLeft, cursorTop, sleepTime);

        }

        private static void ProcessLog(LoadTest currentTest)
        {
            Console.WriteLine("Processing Log !");
            var cursorLeft = Console.CursorLeft;
            var cursorTop = Console.CursorTop;
            var spinner1 = new ConsoleWaiter(cursorLeft, cursorTop, 400);

            spinner1.Start();
            var log = FileProcesser.ProcessFile(currentTest);
            spinner1.Stop();

            Console.WriteLine("CHECKING FILE:");
            Console.WriteLine("Received Messages:  " + log.RecievedMessages);
            Console.WriteLine("Processed Messages: " + log.ProcessedMessages);
            Console.WriteLine("In QUEUE: " + log.TotalReadedLines);
        }

        private static void GenerateCalls(LoadTest currentLoadTest)
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


            var factory = new ProcessFactory(currentLoadTest.Clients,currentLoadTest.MessagesByClient);
            var processes = factory.Processes;
            cursorLeft = Console.CursorLeft;
            cursorTop = Console.CursorTop;
            var waiter = new ConsoleWaiter(cursorLeft, cursorTop, 400, animation);

            waiter.Start();

            factory.Excecute(currentLoadTest.Id);

            waiter.Stop();

            Console.WriteLine("All threads are complete !!");

            Wait(currentLoadTest.TotalSendedMessages);
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
