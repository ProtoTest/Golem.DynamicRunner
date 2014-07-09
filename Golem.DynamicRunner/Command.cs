using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MbUnit.Framework;
using OpenQA.Selenium;

namespace Golem.DynamicRunner
{
    public abstract class Command
    {
        public BrowserCommands BrowserCommandName { get; set; }
        public By by { get; set; }
        public string param { get; set; }
        public string commandString { get; set; }
        public string[] toks { get; set; }
        

    /// <summary>
        /// Click(locator)
        /// Type(locator,text)
        /// Back()
        /// Fforward()
        /// Open(url)
        /// Verify(locator)
        /// Wait(locator,time)
        /// </summary>
        /// <param name="commandString"></param>
        protected Command(string commandString)
        {
            this.commandString = commandString;
            char[] parens = new[] { '(', ')', ',' };
            //toks[0] = CommandName
            toks= commandString.Split(parens);
            string commandName = toks[0];
            BrowserCommandName = (BrowserCommands)Enum.Parse(typeof(BrowserCommands), commandName);
            ParseCommand();
            VerifyCommandSyntax();
        }

        protected abstract void ParseCommand();

        protected abstract void VerifyCommandSyntax();

        public abstract void ExecuteCommand();

        public By getByFromString(string locator)
        {
            By byLocator;
            if (locator.Contains("//") || locator.Contains("/"))
            {
                byLocator = By.XPath(locator);
            }
            if (locator.StartsWith("$"))
            {
                byLocator = By.XPath("//*[contains(text(),'" + locator.Substring(1) + "')]");

            }
            else
            {
                byLocator = By.CssSelector(locator);
            }
            return byLocator;
        }
      
    }
}
