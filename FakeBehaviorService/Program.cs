using NLog;
using System;


namespace FakeBehaviorService
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger logger = LogManager.GetLogger("information");
            logger.Info("Test log");
        }
    }
}
