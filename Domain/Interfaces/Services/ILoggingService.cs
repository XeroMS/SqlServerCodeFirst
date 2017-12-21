using System;
using System.Collections.Generic;
using Domain.Model.General;

namespace Domain.Interfaces.Services
{
    public partial interface ILoggingService
    {
        void Error(string message);
        void Error(Exception ex);
        void Initialize(int maxLogSize);
        IList<LogEntry> ListLogFile();
        void Recycle();
        void ClearLogFiles();
    }
}
