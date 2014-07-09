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
using ProtoTest.Golem.Tests;
using ProtoTest.Golem.Tests.PageObjects.Google;
using ProtoTest.Golem.WebDriver;

namespace Golem.DynamicRunner
{
    [Parallelizable]
    public class CSVTestFactory : WebDriverTestBase
    {

        public List<TestRunner> GetTests()
        {
            List<TestRunner> testsToRun = new List<TestRunner>();
            var reader = new StreamReader(File.OpenRead(@"TestData.csv"));
            while (!reader.EndOfStream)
            {
                var testData = WebDriverTestBase.testData;
                var line = reader.ReadLine();
                if (line != "")
                {
                    var runner = new TestRunner(line);
                    if (runner.testCommand == TestCommands.Run)
                    {
                        testsToRun.Add(new TestRunner(line));
                    }
                    if (runner.testCommand == TestCommands.Only)
                    {
                        testsToRun = new List<TestRunner>();
                        testsToRun.Add(new TestRunner(line));
                        return testsToRun;
                    }
                }
                
            }
            return testsToRun;
        }


        [Test, Parallelizable,Factory("GetTests")]
        public void RunTestsFromCSV(TestRunner runner)
        {
            runner.runCommands();
        }
    }
}
