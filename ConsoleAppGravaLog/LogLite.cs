using DAL.SQLlite.Classes;
using DAL.SQLlite.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppGravaLog
{
    public static class LogLite
    {
        private static LogRepository _repo { get; set; } = new LogRepository();

        public static async Task<bool> LogWarn(LogException log)
          =>  await _repo.GravaLog(log);
    }
}
