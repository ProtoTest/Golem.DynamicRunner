using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Gallio.Runtime.Extensibility;
using OpenQA.Selenium;
using ProtoTest.Golem.Proxy.HAR;
using ProtoTest.Golem.WebDriver;

namespace Golem.DynamicRunner
{
    public class TestRunner
    {
        private IWebDriver driver;
        public TestCommands testCommand;
        public List<string> commandStrings;
        public List<Command> commands;
        public Char delim = ';';
        public TestRunner(string line)
        {
            this.driver = WebDriverTestBase.driver;
            this.commandStrings = line.Split(delim).ToList();
            this.testCommand = (TestCommands)Enum.Parse(typeof(TestCommands), commandStrings[0]);
            this.commandStrings.RemoveAt(0);
            this.commands = getCommands();

        }

        private Command getCommand(string column)
        {
            //click(.id)
            //type(//div[@id='id'],some weird text)
            //open(http://www.aol.com)
            //back
            char[] parens = new[] {'(', ')',','};
            string[] toks = column.Split(parens);
            string commandName = toks[0];
            string locator = "";
            if (toks.Length > 1)
            {
                locator = toks[1];
            }
            string text = "";
            if (toks.Length > 2)
            {
                text = toks[2];
            }

            BrowserCommands browserCommand = (BrowserCommands)Enum.Parse(typeof(BrowserCommands), commandName);
            By by;
            if (locator.Contains("//") || locator.Contains("/"))
            {
                by = By.XPath(locator);
            }
            if (locator.StartsWith("$"))
            {
                by = By.XPath("//*[contains(text(),'" + locator.Substring(1) + "')]");

            }
            else
            {
                by = By.CssSelector(locator);    
            }
            

            if (browserCommand == BrowserCommands.Open)
            {
                text = toks[1];
                by = null;
            }

            return new Command(browserCommand,by,text);
        }

        private List<Command> getCommands()
        {
            var commands = new List<Command>();
            foreach (var column in this.commandStrings)
            {
                commands.Add(getCommand(column));
            }

            return commands;
        }

        private TestCommands getTestCommand()
        {
            return testCommand;
        }

        public void runCommands()
        {
            var commands = getCommands();
            foreach (var command in commands)
            {
                CommandExecutor executor = new CommandExecutor();
                executor.executeCommand(command);
            }
        }

    }
}
