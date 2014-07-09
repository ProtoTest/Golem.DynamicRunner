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
    class VerifyCommand : Command
    {
        public VerifyCommand(string commandString)
            : base(commandString)
        {
        }

        protected override void ParseCommand()
        {
            this.by = getByFromString(toks[1]);
        }

        protected override void VerifyCommandSyntax()
        {
            Assert.AreEqual(BrowserCommands.Verify, BrowserCommandName,commandString);
            Assert.IsNotNull(by, "Verify command needs a locator : Verify(locator) : " + commandString);
            Assert.IsNull(param, "Verify command should not have a param : Verify(locator) : " + commandString);
        }

        public override void ExecuteCommand()
        {
            new Element(by).WaitUntil().Present().Verify().Visible();
        }
    }
}
