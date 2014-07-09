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
        public TestCommands testCommand;
        public List<string> commandStrings;
        public List<Command> commands;
        public Char delim = ';';
        public TestRunner(string line)
        {
            this.commandStrings = line.Split(delim).ToList();
            this.testCommand = (TestCommands)Enum.Parse(typeof(TestCommands), commandStrings[0]);
            this.commandStrings.RemoveAt(0);
            this.commands = getCommandsFromString();

        }

        private List<Command> getCommandsFromString()
        {
            var commands = new List<Command>();
            foreach (var command in this.commandStrings)
            {
                if (command.StartsWith(BrowserCommands.Click.ToString()))
                {
                    commands.Add(new ClickCommand(command));
                }
                if (command.StartsWith(BrowserCommands.Type.ToString()))
                {
                    commands.Add(new TypeCommand(command));
                }
                if (command.StartsWith(BrowserCommands.Open.ToString()))
                {
                    commands.Add(new OpenCommand(command));
                }
                if (command.StartsWith(BrowserCommands.Back.ToString()))
                {
                    commands.Add(new BackCommand(command));
                }
                if (command.StartsWith(BrowserCommands.Forward.ToString()))
                {
                    commands.Add(new ForwardCommand(command));
                }
                if (command.StartsWith(BrowserCommands.Wait.ToString()))
                {
                    commands.Add(new WaitCommand(command));
                }
                if (command.StartsWith(BrowserCommands.Verify.ToString()))
                {
                    commands.Add(new VerifyCommand(command));
                }
            }

            return commands;
        }

        public void runCommands()
        {
            foreach (var command in commands)
            {
                command.ExecuteCommand();
            }
        }

    }
}
