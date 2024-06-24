using FileLoggerKata;
using Moq;
using Shouldly;
using Xunit;

namespace FileLoggerTests
{
    public class FileLoggerTest
    {
        [Fact]
        public void Log_ReturnsTrue()
        {
            var logger = new Logger(MockServiceHelper.GetFileWriterService(), MockServiceHelper.GetSystemDateTimeService(new DateTime(2024, 06, 23)));
            logger.Log("test message").ShouldBeTrue();
        }

        [Fact]
        public void GetFileName_GivenWeekDay_ReturnsWeekDayFormat()
        {
            var logger = new Logger(MockServiceHelper.GetFileWriterService(), MockServiceHelper.GetSystemDateTimeService(new DateTime(2024, 06, 23)));
            var controlDateTime = new DateTime(2024, 06, 24);
            logger.GetFileName(controlDateTime).ShouldBe($"{controlDateTime.ToString("yyyyMMdd")}.txt");
        
        }

        [Fact]
        public void GetFileName_GivenWeekend_ReturnsWeekendFormat()
        {
            var logger = new Logger(MockServiceHelper.GetFileWriterService(), MockServiceHelper.GetSystemDateTimeService(new DateTime(2024, 06, 23)));
            var controlDateTime = new DateTime(2024, 06, 23);
            logger.GetFileName(controlDateTime).ShouldBe($"weekend.txt");
        }

        
    }

    public class WeekendHelperTest
    {
        [Fact]
        public void HandleWeekendFile_GivenNewWeekend_ReturnsTrue()
        {
            var newWeekend = new DateTime(2024, 06, 23);
            var FileRoot = Directory.GetParent(Directory.GetCurrentDirectory());
            var weekendHelper = new WeekendHelper(MockServiceHelper.GetFileWriterService(), MockServiceHelper.GetSystemDateTimeService(newWeekend));
            weekendHelper.HandleWeekendFile(string.Empty, FileRoot).ShouldBeTrue();
        }

        [Fact]
        public void HandleWeekendFile_GivenSameWeekend_ReturnsFalse()
        {
            var sameWeekend = new DateTime(2024, 05, 12);
            var FileRoot = Directory.GetParent(Directory.GetCurrentDirectory());
            var weekendHelper = new WeekendHelper(MockServiceHelper.GetFileWriterService(), MockServiceHelper.GetSystemDateTimeService(sameWeekend));
            weekendHelper.HandleWeekendFile(string.Empty, FileRoot).ShouldBeFalse();
        }

        [Fact]
        public void CacheWeekendFile_GivenSaturdayDate_FileNameMatchesDate()
        {
            var saturdayDate = new DateTime(2024, 06, 15);
            var FileRoot = Directory.GetParent(Directory.GetCurrentDirectory());
            var weekendHelper = new WeekendHelper(MockServiceHelper.GetFileWriterService(), MockServiceHelper.GetSystemDateTimeService(saturdayDate));
            weekendHelper.CacheWeekendFile(string.Empty, saturdayDate, FileRoot.ToString()).ShouldBe($"weekend-{saturdayDate.ToString("YYYYMMDD")}.txt");
        }

        [Fact]
        public void CacheWeekendFile_GivenSundayDate_FileNameIsPreviousSaturday()
        {
            var saturdayDate = new DateTime(2024, 06, 15);
            var sundayDate = new DateTime(2024, 06, 14);
            var FileRoot = Directory.GetParent(Directory.GetCurrentDirectory());
            var weekendHelper = new WeekendHelper(MockServiceHelper.GetFileWriterService(), MockServiceHelper.GetSystemDateTimeService(saturdayDate));
            weekendHelper.CacheWeekendFile(string.Empty, sundayDate, FileRoot.ToString()).ShouldBe($"weekend-{saturdayDate.ToString("YYYYMMDD")}.txt");

        }
    }
}