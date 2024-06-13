using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FileLoggerKata
{
    public class Logger
    {
        //realistically this would be a dependency-injected File Handler
        private static string _FileLocation;
        public Logger(string FileLocation)
        {
            if(!File.Exists(FileLocation))
            {
                File.Create(FileLocation);
            }
        }
        public void Log(string message)
        {

        }
    }
}
