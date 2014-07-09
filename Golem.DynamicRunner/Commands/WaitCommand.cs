using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MbUnit.Framework;
using OpenQA.Selenium;
using ProtoTest.Golem.Core;
using ProtoTest.Golem.WebDriver;

namespace Golem.DynamicRunner
{
    class WaitCommand : Command
    {
        public WaitCommand(string commandString)
            : base(commandString)
        {
        }

        protected override void ParseCommand()
        {
            this.by = getByFromString(toks[1]);
            this.param = toks[2];
        }

        protected override void VerifyCommandSyntax()
        {
            Assert.AreEqual(BrowserCommands.Wait, BrowserCommandName,commandString);
            Assert.IsNotNull(by, "Wait command needs a locator : Wait(locator,time) : " + commandString);
            Assert.IsNotEmpty(param, "Wait command should have a param : Wait(locator,time) " + commandString);
        }

        public override void ExecuteCommand()
        {
            new Element(by).WaitUntil(int.Parse(param)).Present().Verify().Visible();
        }
    }
}
