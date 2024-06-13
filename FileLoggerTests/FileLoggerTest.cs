using FileLoggerKata;
using Shouldly;
using Xunit;

namespace FileLoggerTests
{
    public class FileLoggerTest
    {
        [Fact]
        public async Task Logger_OnLog_FileContainsMessage()
        {
            var logger = new Logger();
            var log_string = "This is a new Log";
            await logger.Log(log_string);
            using (var reader = File.OpenText(logger.FileLocation))
            {
                var log = reader.ReadToEnd();
                log.ShouldContain(log_string);
                log.ShouldContain($"{DateTime.Now.Hour.ToString()}:{DateTime.Now.Minute.ToString()}");
            };


            //logger.Delete();
        }

        [Fact]
        public async Task Logger_OnLog_GivenAlternateDatetime_NewFileIsCreated()
        {
            var logger = new Logger();
            var log_string = "This is a new Log";

            var alternateDateTime = DateTime.Now.AddDays(1);
            await logger.Log(log_string, alternateDateTime);
            //TODO
            var originalPath = Path.Combine(logger.FileRoot.ToString(), $"{DateTime.Now.ToString("yyyyMMdd")}.txt");
            File.Exists(originalPath).ShouldBeTrue();
            File.Exists(Path.Combine(logger.FileRoot.ToString(), $"{alternateDateTime}.txt"));
        }
    }
}