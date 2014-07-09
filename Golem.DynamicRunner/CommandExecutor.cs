using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using ProtoTest.Golem.WebDriver;

namespace Golem.DynamicRunner
{
    class CommandExecutor
    {
        public CommandExecutor()
        {
        }

        public void executeCommand(Command command)
        {
            Element element = new Element(command.by);
            switch (command.BrowserCommandName)
            {
                case BrowserCommands.Click:
                    element.WaitUntil().Present().GetVisibleElement().Click();
                    break;
                case BrowserCommands.Type:
                    element.WaitUntil().Present().GetVisibleElement().SetText(command.param);
                    break;
                case BrowserCommands.Verify:
                    element.WaitUntil().Present().Verify().Visible();
                    break;
                case BrowserCommands.Wait:
                    element.WaitUntil().Present();
                    break;
                case BrowserCommands.Open:
                    WebDriverTestBase.driver.Navigate().GoToUrl(command.param);
                    break;
                case BrowserCommands.Back:
                    WebDriverTestBase.driver.Navigate().Back();
                    break;
                case BrowserCommands.Forward:
                    WebDriverTestBase.driver.Navigate().Forward();
                    break;
                default :
                    throw new Exception("The browserCommand was has not been implemented yet : " + command.BrowserCommandName);
            }
        }
    }
}
