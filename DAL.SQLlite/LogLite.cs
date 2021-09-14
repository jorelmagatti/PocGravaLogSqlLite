using DAL.SQLlite.Classes;
using DAL.SQLlite.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.SQLlite
{
    public class LogLite
    {
        public enum ExLogType
        {
            Warning,
            Error,
            Urgency,
            Caltion
        }
        private static LogRepository _repo { get; set; } = new LogRepository();

        public static async Task<bool> LogWarn(LogExceptionInterface log)
        {
            log.Tipo = ExLogType.Warning.ToString();
            return await _repo.GravaLog(log);
        }

        public static async Task<bool> LogError(LogExceptionInterface log)
        {
            log.Tipo = ExLogType.Error.ToString();
            return await _repo.GravaLog(log);
        }

        public static async Task<bool> LogUrgency(LogExceptionInterface log)
        {
            log.Tipo = ExLogType.Urgency.ToString();
            return await _repo.GravaLog(log);
        }

        public static async Task<bool> LogCaltion(LogExceptionInterface log)
        {
            log.Tipo = ExLogType.Caltion.ToString();
            return await _repo.GravaLog(log);
        }
    }
}
