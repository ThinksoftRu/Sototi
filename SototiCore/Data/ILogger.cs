using System;

namespace SototiCore.Data
{
    public interface ILogger
    {
        void Error(string format, params object[] args);

        void Warn(string format, params object[] args);

        void Warn(Exception exception, string format, params object[] args);

        void Fatal(Exception exception, string format, params object[] args);

        void Trace(string format, params object[] args);
    }
}
