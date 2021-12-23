using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RPA.UiPath.Classlib.Common.Tools
{
    public class Logger
    {
        private readonly string _logFolder = "C:\\UIPATHCOMPOSELOGS";

        private readonly string _logFile = "C:\\UIPATHCOMPOSELOGS\\default.log";

        private StreamWriter _writer;

        public Logger()
        {
            Init();
        }

        public Logger(string logFile)
        {
            _logFolder = Path.GetDirectoryName(logFile);
            _logFile = logFile;
            Init();
        }

        private void Init()
        {
            if (!Directory.Exists(_logFolder))
            {
                Directory.CreateDirectory(_logFolder);
            }
            if (!File.Exists(_logFile))
            {
                File.Create(_logFile).Close();
            }

            _writer = new StreamWriter(_logFile);
        }

        public void WriteLog(string message)
        {
            var content = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} {message}";
            _writer.WriteLine(content);
        }

        public void WriteLog(Exception ex)
        {
            var content = $"异常信息：{ex.Message} 内部信息：{ex.InnerException.Message} 堆栈跟踪：{ex.StackTrace}";
            WriteLog(content);
        }
    }
}
