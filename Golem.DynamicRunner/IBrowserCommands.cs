using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace Golem.DynamicRunner
{
    interface IBrowserCommands
    {
        BrowserCommands Commands { get; set; }
        By by { get; set; }
        string text { get; set; }
        string[] toks { get; set; }
        void VerifyCommandSyntax();
        void ParseCommandString();
    }
}
