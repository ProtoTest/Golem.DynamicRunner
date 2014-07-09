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
    class ClickCommand : Command
    {
        public ClickCommand(string commandString) : base(commandString)
        {
        }

        protected override void ParseCommand()
        {
            this.by = getByFromString(toks[1]);
        }

        protected override void VerifyCommandSyntax()
        {
            Assert.AreEqual(BrowserCommands.Click, BrowserCommandName, commandString);
            Assert.IsNotNull(by, "Click command needs a locator : Click(locator): " + commandString);
            Assert.IsEmpty(toks[2], "Click command should not have a param : Click(locator): " + commandString);
        }

        public override void ExecuteCommand()
        {
            new Element(by).WaitUntil().Present().GetVisibleElement().Click();
        }
    }
}
