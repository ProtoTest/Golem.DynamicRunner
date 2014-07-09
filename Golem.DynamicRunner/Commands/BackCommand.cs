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
    class BackCommand : Command
    {
        public BackCommand(string commandString)
            : base(commandString)
        {
        }

        protected override void ParseCommand()
        {
            
        }

        protected override void VerifyCommandSyntax()
        {
            Assert.AreEqual(BrowserCommands.Back,BrowserCommandName,commandString);
            Assert.Count(1, toks, "Back command does not use any params : Back(): " + commandString);
        }

        public override void ExecuteCommand()
        {
            WebDriverTestBase.driver.Navigate().Back();
        }
    }
}
