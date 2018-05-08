using GGLoader.BLL;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGLoader.Tests
{
    [TestFixture]
    class ProcessFactoryTests
    {
        [Test]
        public void ShouldReturn3ProcessWhenIsend3()
        {
            const int processQuantity = 3;
            const int shoots = 20;
            const int expectedProcesses = 3;
            const int expectedShoots = 20;

            var processes = new ProcessFactory(processQuantity, shoots).Processes;

            Assert.AreEqual(expectedProcesses, processes.Count);
            Assert.AreEqual(expectedProcesses, processes.Count(p => p.Shoots.Equals(expectedShoots)));
        }
    }
}
