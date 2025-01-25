using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Brokers
{
    internal class LoggingBroker
    {
        public void LoggingError(string message)
        {
            Console.WriteLine($"[XATO]:{message}");
        }
    }
}
