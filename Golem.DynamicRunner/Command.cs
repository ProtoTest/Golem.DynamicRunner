using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MbUnit.Framework;
using OpenQA.Selenium;

namespace Golem.DynamicRunner
{
    public class Command
    {
 
        public BrowserCommands BrowserCommandName { get; set; }
        public By by { get; set; }
        public string param { get; set; }

        public Command(BrowserCommands BrowserCommandName, By by, string param = "")
        {
            this.BrowserCommandName = BrowserCommandName;
            this.by = by;
            this.param = param;
            VerifyCommandSyntax();
        }

        private void VerifyCommandSyntax()
        {
            switch (BrowserCommandName)
            {
                case BrowserCommands.Click :
                    Assert.IsNotNull(by,"Click command needs a locator : Click(locator)" );
                    Assert.IsEmpty(param,"Click command should not have a param : Click(locator)");
                    break;
                case BrowserCommands.Type:
                    Assert.IsNotNull(by, "Type command needs a locator : Type(locator,text)");
                    Assert.IsNotEmpty(param, "Type command should have a param in format Type(locator,text)");
                    break;
                case BrowserCommands.Open:
                    Assert.IsNull(by, "Open command does not use a locator : Open(url)");
                    Assert.IsNotEmpty(param, "Open command should have a URL as a param : Open(http://www.google.com)");
                    break;
                case BrowserCommands.Back:
                    Assert.IsNull(by, "Back command does not use a locator : Back()");
                    Assert.IsEmpty(param, "Back command does not accept a param : Back()");
                    break;
                case BrowserCommands.Forward:
                    Assert.IsNull(by, "Forward command does not use a locator : Forward()");
                    Assert.IsEmpty(param, "Forward command does not accept a param : Forward()");
                    break;
                case BrowserCommands.Verify:
                    Assert.IsNotNull(by, "Verify command needs a locator : Verify(locator)");
                    Assert.IsEmpty(param, "Verify command should not have a param : Verify(locator)");
                    break;
                case BrowserCommands.Wait:
                    Assert.IsNotNull(by, "Wait command needs a locator : Wait(locator,time)");
                    Assert.IsNotEmpty(param, "Wait command should have a param : Wait(locator,time)");
                    break;



            }
        }
    }
}
