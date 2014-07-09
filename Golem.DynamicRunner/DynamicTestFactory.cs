using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Gallio.Common.Markup;
using Gallio.Common.Media;
using Gallio.Framework;
using Gallio.Framework.Pattern;
using Gallio.Model;
using MbUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using ProtoTest.Golem.Core;
using ProtoTest.Golem.Proxy;
using ProtoTest.Golem.WebDriver;

namespace Golem.DynamicRunner
{
    public class DynamicTestFactory : WebDriverTestBase
    {
     
        [DynamicTestFactory]
        public static IEnumerable<Test> RunTestsFromCSVFile()
        {
            Config.Settings.runTimeSettings.ElementTimeoutSec = 30;
            var reader = new StreamReader(File.OpenRead(@"TestData.csv"));
            while (!reader.EndOfStream)
            {
                
                var line = reader.ReadLine();
                yield return new TestCase(line, () =>
                {
                         WebDriverTestBase.driver = WebDriverTestBase.testDataCollection.FirstOrDefault().Value.driver;
                        TestRunner runner = new TestRunner(line);
                        runner.runCommands();
                });
            }
        }
    }
}
