using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGLoader
{
    public class FileWriter
    {
        public void Write(string pathDestination, string json)
        {
            File.WriteAllText(pathDestination, json);
        }

        public void Write(string pathDestination, List<string> lines)
        {
            using (TextWriter tw = new StreamWriter(pathDestination))
            {
                foreach (string s in lines)
                    tw.WriteLine(s);
            }
        }
    }
}
