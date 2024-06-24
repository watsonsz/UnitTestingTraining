using FileLoggerKata;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileLoggerTests
{
    public static class MockServiceHelper
    {
        public static IFileWriter GetFileWriterService()
        {
            var mockLogger = new Mock<IFileWriter>();
            mockLogger.Setup(service => service.FileExists(It.IsAny<string>())).Returns(true);
            mockLogger.Setup(service => service.GetLastEditedDatetime(It.IsAny<string>())).Returns(new DateTime(2024, 05, 12));
            mockLogger.Setup(service => service.WriteFile(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            mockLogger.Setup(service => service.RenameFile(It.IsAny<string>(), It.IsAny<string>()));
            return mockLogger.Object;
        }

        public static ISystemDateTime GetSystemDateTimeService(DateTime desiredDatetime)
        {
            var  mockDateTime = new Mock<ISystemDateTime>();
            mockDateTime.Setup(service => service.Now()).Returns(desiredDatetime);
            return mockDateTime.Object;
        }
    }
}
