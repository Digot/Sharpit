using System;

namespace Sharpit.Util.Logging
{
    public class Logger
    {
        private readonly Type loggerClass;

        Logger(Type loggerClass)
        {
            this.loggerClass = loggerClass;
        }

        public static Logger get(Type type)
        {
            return new Logger(type);
        }

        private void output(LoggingLevel level, String message)
        {
            ConsoleColor prevColor = Console.ForegroundColor;
            Console.Write("{0} [{1}] [", DateTime.Now.ToLongTimeString(), loggerClass.Name);
            Console.ForegroundColor = level.ConsoleColor;
            Console.Write("{0}", level.Name);
            Console.ForegroundColor = prevColor;
            Console.Write("] ");
            Console.ForegroundColor = level.ConsoleColor;
            Console.WriteLine(message);
            Console.ForegroundColor = prevColor;
        }

        public void debug(String message)
        {
#if (DEBUG)
            output(LoggingLevel.DEBUG, message);
#endif
        }

        public void info(string message)
        {
            output(LoggingLevel.INFO,message);
        }
    }
}