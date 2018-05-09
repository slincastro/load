﻿using GGLoader.BLL.Domain;
using GGLoader.Reports;
using System.IO;

namespace GGLoader
{
    class FileProcesser
    {
        public static Log ProcessFile(LoadTest loadTest)
        {

            var lines = new FileReader().Read(loadTest.AnalyzedLogPath);
            var log = new Log(lines, loadTest.Id);

            var currentDiagnostic = new Diagnostic(log, loadTest.TotalSendedMessages);

            var diagnosticPath = loadTest.GetDiagnosticPath();
            var createFolderDirectory = Directory.CreateDirectory(diagnosticPath);

            new ProcessReport { LoadTest = loadTest, Log = log, Diagnostic = currentDiagnostic }.Excecute();
            new GeneralReport { LoadTest = loadTest, Log = log, Diagnostic = currentDiagnostic }.Excecute();

            return log;
        }
    }



}
