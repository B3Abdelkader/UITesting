using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebUITesting
{
    class Scenarios : Fixtures
    {
        [Test]
        [TestCase(TestName = "WebTest",// Ignore = "pour CI",
         Author = "@B3Abdelkader")]
        public void Mon1erTest()
        {
            _driver.Navigate().GoToUrl("https://free.fr");
        }
    }
}
