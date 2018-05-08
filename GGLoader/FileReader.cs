using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGLoader
{
    public class FileReader
    {
        public List<string> Read(string filePath)
        {
            var lines = new List<string>();
            using (FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string line;
                    while ((line = reader.ReadLineAsync().Result) != null)
                    {
                        lines.Add(line);
                    }
                }
            }

            return lines;
        }

    }
}
