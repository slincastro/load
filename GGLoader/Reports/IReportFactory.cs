using GGLoader.BLL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGLoader.Reports
{
    public interface IReportFactory
    {
        IReport Create(LoadTest loadTest);
    }

    internal class GeneralReportFactory : IReportFactory
    {
        public IReport Create(LoadTest loadTest)
        {
            return new GeneralReport { LoadTest = loadTest };
        }
    }

    internal class ProcessReportFactory : IReportFactory
    {
        public IReport Create(LoadTest loadTest)
        {
            return new ProcessReport { LoadTest = loadTest };
        }
    }
}
