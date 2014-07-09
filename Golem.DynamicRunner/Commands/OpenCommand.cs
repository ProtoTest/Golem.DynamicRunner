using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MbUnit.Framework;
using OpenQA.Selenium;
using ProtoTest.Golem.WebDriver;

namespace Golem.DynamicRunner
{
    class OpenCommand : Command
    {
        public OpenCommand(string commandString)
            : base(commandString)
        {
        }

        protected override void ParseCommand()
        {
            this.by = null;
            this.param = toks[1];
        }

        protected override void VerifyCommandSyntax()
        {
            Assert.AreEqual(BrowserCommands.Open, BrowserCommandName,commandString);
            Assert.IsNull(by, "Open command does not use a locator : Open(url): " + commandString);
            Assert.IsNotEmpty(param, "Open command should have a URL as a param : Open(http://www.google.com): " + commandString);
        }

        public override void ExecuteCommand()
        {
            WebDriverTestBase.driver.Navigate().GoToUrl(param);
        }
    }
}
