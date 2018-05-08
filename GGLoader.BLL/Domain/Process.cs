using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGLoader.BLL.Domain
{
    public class Process
    {
        public string Name { get; set; }
        public int Port { get; set; }
        public int Shoots { get; internal set; }
    }
}
