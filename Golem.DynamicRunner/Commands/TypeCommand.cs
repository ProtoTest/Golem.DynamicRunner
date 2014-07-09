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
    class TypeCommand : Command
    {
        public TypeCommand(string commandString)
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
            Assert.AreEqual(BrowserCommands.Type, BrowserCommandName,commandString);
            Assert.IsNotNull(by, "Type command needs a locator : Type(locator,text): " + commandString);
            Assert.IsNotEmpty(param, "Type command should have a param in format Type(locator,text): " + commandString);
        }

        public override void ExecuteCommand()
        {
            new Element(by).WaitUntil().Present().GetVisibleElement().SetText(param);
        }
    }
}
