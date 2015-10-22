using System;

namespace Sharpit.Util.Logging
{
    public class LoggingLevel
    {

        public static readonly LoggingLevel WARNING = new LoggingLevel(ConsoleColor.Yellow,"WARNING");
        public static readonly LoggingLevel CRITICAL = new LoggingLevel(ConsoleColor.Red, "CRITICAL");
        public static readonly LoggingLevel INFO = new LoggingLevel(ConsoleColor.Cyan, "INFO");
        public static readonly LoggingLevel DEBUG = new LoggingLevel(ConsoleColor.Green, "DEBUG");


        public ConsoleColor ConsoleColor { get; set; }
        public string Name { get; set; }

        public LoggingLevel(ConsoleColor consoleColor, string name)
        {
            ConsoleColor = consoleColor;
            this.Name = name;
        }
    }
}