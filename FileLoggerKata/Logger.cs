using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FileLoggerKata
{
    public interface ISystemDateTime
    {
        public DateTime Now();
    }

    public class Logger
    {
        //realistically this would be a dependency-injected File Handler

        public string FileLocation { get; set; }
        public DirectoryInfo FileRoot { get; set; }
        private readonly IFileWriter _writer;
        private const string WEEKEND_FILENAME = "weekend.txt";
        private readonly ISystemDateTime _dateTime;
        public Logger(IFileWriter writer, ISystemDateTime systemDateTime)
        {
            FileRoot = Directory.GetParent(Directory.GetCurrentDirectory());
            _writer = writer;
            _dateTime = systemDateTime;
        }
        public bool Log(string message)
        {
            var currentDate = _dateTime.Now();
            string fileName = GetFileName(currentDate);

            FileLocation = Path.Combine(FileRoot.ToString(), fileName);
            if(!_writer.FileExists(fileName))
            {
                _writer.CreateFile(fileName);
            }

            return _writer.WriteFile(FileLocation, message);
        }

        public string GetFileName(DateTime currentDate)
        {
            if(currentDate.DayOfWeek == DayOfWeek.Saturday || currentDate.DayOfWeek == DayOfWeek.Sunday)
            {
                FileLocation = Path.Combine(FileRoot.ToString(), WEEKEND_FILENAME);
                var helper = new WeekendHelper(_writer, _dateTime);
                helper.HandleWeekendFile(FileLocation, FileRoot);
                return WEEKEND_FILENAME;
            }
            else
            {
                return $"{currentDate.ToString("yyyyMMdd")}.txt";
            }
        }

       

        public async Task Delete()
        {
            File.Delete(FileLocation);
        }
    }
}
