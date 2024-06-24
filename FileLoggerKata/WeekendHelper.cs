using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileLoggerKata
{
    public class WeekendHelper
    {
        private readonly IFileWriter _writer;
        private readonly ISystemDateTime _dateTime;
        public WeekendHelper(IFileWriter writer, ISystemDateTime systemDateTime)
        {
            _writer = writer;
            _dateTime = systemDateTime;
        }
        public bool HandleWeekendFile(string FilePath, DirectoryInfo fileRoot)
        {
            //Determine if this Exists
            //GetLastEditedDateTime
            var currentDate = _dateTime.Now();
            DateTime lastEdited = _writer.GetLastEditedDatetime(FilePath);
            if (lastEdited.Day != currentDate.Day && lastEdited.Day != currentDate.AddDays(-1).Day)
            {
                CacheWeekendFile(FilePath, lastEdited, fileRoot.ToString());
                return true;
            }
            return false;
        }

        public string CacheWeekendFile(string filePath, DateTime lastEdited, string fileRoot)
        {
            string newFileName = string.Empty;
            //DetermineIFSaturdayAndGetSaturdayDatetimeIFNOT
            if(lastEdited.DayOfWeek == DayOfWeek.Saturday)
            {
                newFileName = $"weekend-{lastEdited.ToString("YYYYMMDD")}.txt";
            }
            else
            {
                newFileName = $"weekend-{lastEdited.AddDays(-1).ToString("YYYYMMDD")}.txt";
            }
            _writer.RenameFile(filePath, Path.Combine(fileRoot, newFileName));
            return newFileName;
        }
    }
}
