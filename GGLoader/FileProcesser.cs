using GGLoader.BLL.Domain;
using GGLoader.Reports;
using System.Collections.Generic;
using System.IO;

namespace GGLoader
{
    class FileProcesser
    {
        public static Log ProcessFile(LoadTest loadTest)
        {
            var lines = new FileReader().Read(loadTest.AnalyzedLogPath);

            var diagnosticPath = loadTest.GetDiagnosticPath();
            var createFolderDirectory = Directory.CreateDirectory(diagnosticPath);

            loadTest.Log = new Log(lines, loadTest.Id);
            loadTest.Diagnostic = new Diagnostic(loadTest.Log, loadTest.TotalSendedMessages);

            new List<IReportFactory>
            {
                new ProcessReportFactory(),
                new GeneralReportFactory()

            }.ForEach(r => 
            {
                r.Create(loadTest).Excecute();
            });

            return loadTest.Log;
        }
    }



}
