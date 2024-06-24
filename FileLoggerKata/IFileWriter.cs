using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileLoggerKata
{
    public interface IFileWriter
    {
        public bool WriteFile(string path, string content);
        public bool FileExists(string path);
        public bool CreateFile(string path);
        public DateTime GetLastEditedDatetime(string path);
        public void RenameFile(string originalPath, string newPath);
    }
}
