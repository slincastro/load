using GGLoader.BLL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGLoader.Reports
{
    public interface IGenerator
    {
        LoadTest LoadTest { get; set; }
        Diagnostic Diagnostic { get; set; }
        Log Log { get; set; }

        void Excecute();
    }
}
