using System;

namespace Sharpit.Command
{
    public abstract class Command
    {
        public string Name { get; set; }
        public string[] Aliases { get; set; }

        public Command(string name, string[] aliases)
        {
            Name = name;
            Aliases = aliases;
        }

        public Command(string name)
        {
            Name = name;
            Aliases = null;
        }

        public abstract void execute(CommandSender sender, String message);
    }
}