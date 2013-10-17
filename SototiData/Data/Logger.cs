using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SototiData.Data
{
    using NLog;

    using SototiCore.Data;

    public class Logger : ILogger
    {
        private readonly NLog.Logger logger = LogManager.GetCurrentClassLogger();

        public void Error(string format, params object[] args)
        {
            logger.Error(String.Format(format, args));
        }

        public void Warn(string format, params object[] args)
        {
            logger.Warn(String.Format(format, args));
        }

        public void Warn(Exception exception, string format, params object[] args)
        {
            logger.WarnException(String.Format(format, args), exception);
        }

        public void Fatal(Exception exception, string format, params object[] args)
        {
            logger.FatalException(String.Format(format, args), exception);
        }

        public void Trace(string format, params object[] args)
        {
            logger.Trace(String.Format(format, args));
        }
    }
}
