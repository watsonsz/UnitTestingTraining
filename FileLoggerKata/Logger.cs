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

        public string FileLocation { get; set; }
        public DirectoryInfo FileRoot { get; set; }
        public Logger()
        {
            FileRoot = Directory.GetParent(Directory.GetCurrentDirectory());

            
        }
        public async Task Log(string message)
        {
            string todaysLogFile;
            if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday || DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            {
                todaysLogFile = "weekend.txt";
                
            }
            else
            {
                todaysLogFile = $"{DateTime.Now.ToString("yyyyMMdd")}.txt";
            }

            FileLocation = Path.Combine(FileRoot.ToString(), todaysLogFile);

            if (!File.Exists(FileLocation))
            {
                File.Create(FileLocation);
            }

            using (var writer = new StreamWriter(FileLocation))
            {
                var logString = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + message;
                await writer.WriteLineAsync(logString);
            };
            
        }

        public async Task Log(string message, DateTime alternateDateTime)
        {
            string todaysLogFile;
            bool isWeekend = false;
            if(DateTime.Now.DayOfWeek == DayOfWeek.Saturday || DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            {
                todaysLogFile = "weekend.txt";
                isWeekend = true;
            }
            else
            {
                todaysLogFile = $"{alternateDateTime.ToString("yyyyMMdd")}.txt";
            }
            
            FileLocation = Path.Combine(FileRoot.ToString(), todaysLogFile);
            //TODO
            if (!File.Exists(FileLocation) && !isWeekend)
            {
                File.Create(FileLocation);
            }
            else if (File.Exists(FileLocation) && isWeekend)
            {
                CacheWeekendFile();
                File.Create(FileLocation);
            }
            

            using (var writer = new StreamWriter(FileLocation))
            {
                var logString = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + message;
                await writer.WriteLineAsync(logString);
            };

        }

        private void CacheWeekendFile()
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            File.Delete(FileLocation);
        }
    }
}
