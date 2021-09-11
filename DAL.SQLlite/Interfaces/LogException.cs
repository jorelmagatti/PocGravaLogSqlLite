using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.SQLlite.Interfaces
{
    public class LogException
    {
        public static LogException getLogException(Exception ex, string localmethod, string detalhes)
        => new LogException()
            {
                ID = 0,
                Data_Log = DateTime.Now,
                Message = !string.IsNullOrEmpty(ex.Message) ? ex.Message : "",
                StackTrace = !string.IsNullOrEmpty(ex.StackTrace) ? ex.StackTrace : "",
                Source = !string.IsNullOrEmpty(ex.Source) ? ex.Source : "",
                LocalMethod = !string.IsNullOrEmpty(localmethod) ? localmethod : "",
                Detalhes = !string.IsNullOrEmpty(detalhes) ? detalhes : ""
        };

        public static LogException getLogException(Exception ex)
        => new LogException()
        {
            ID = 0,
            Data_Log = DateTime.Now,
            Message = !string.IsNullOrEmpty(ex.Message) ? ex.Message : "",
            StackTrace = !string.IsNullOrEmpty(ex.StackTrace) ? ex.StackTrace : "",
            Source = !string.IsNullOrEmpty(ex.Source) ? ex.Source : "",
            LocalMethod = string.Empty,
            Detalhes = string.Empty
        };


        public long ID { get; set; } = 0;
        public DateTime Data_Log { get; set; } = DateTime.Now;
        public string LocalMethod { get; set; } = string.Empty;
        public string Detalhes { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string StackTrace { get; set; } = string.Empty;
        public string Source { get; set; } = string.Empty;
    }
}
