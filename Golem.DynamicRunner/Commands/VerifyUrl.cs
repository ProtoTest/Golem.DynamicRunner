using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gallio.Framework;
using MbUnit.Framework;
using OpenQA.Selenium;
using ProtoTest.Golem.Core;
using ProtoTest.Golem.WebDriver;

namespace Golem.DynamicRunner
{
    class VerifyUrl : Command
    {
        public VerifyUrl(string commandString)
            : base(commandString)
        {
        }

        protected override void ParseCommand()
        {
            this.param = toks[1];
        }

        protected override void VerifyCommandSyntax()
        {
            Assert.AreEqual(BrowserCommands.VerifyUrl, BrowserCommandName,commandString);
            Assert.IsNull(by, "VerifyUrl should not have a locator : " + commandString);
            Assert.IsNotEmpty(param, "VerifyUrl command should have a param : VerifyUrl(http://www.google.com) " + commandString);
        }

        public override void ExecuteCommand()
        {
            if (!WebDriverTestBase.driver.Url.Contains(param))
            {
                TestBase.AddVerificationError("URL Not correct.  Expected : " + param + " got : " + WebDriverTestBase.driver.Url);
            }
            else
            {
                TestContext.CurrentContext.IncrementAssertCount();
            }
        }
    }
}
