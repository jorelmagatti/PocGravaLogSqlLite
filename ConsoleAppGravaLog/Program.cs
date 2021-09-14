using DAL.SQLlite;
using DAL.SQLlite.Classes;
using DAL.SQLlite.Interfaces;
using System;
using System.Reflection;

namespace ConsoleAppGravaLog
{
    class Program
    {
        static void Main(string[] args)
        {           
            Console.WriteLine("Gerando objeto de log!");
            string fullMethodName = $"{MethodBase.GetCurrentMethod().ReflectedType.FullName}.{MethodBase.GetCurrentMethod().Name}";
            try
            {
                throw new Exception("erro programado");
                //throw new Exception("erro programado");
            }
            catch(Exception ex)
            {
                LogLite.LogWarn(LogExceptionInterface.getLogException(ex, fullMethodName, "exceção produzida na Main")).Wait();
                LogLite.LogError(LogExceptionInterface.getLogException(ex, fullMethodName, "exceção produzida na Main")).Wait();
                LogLite.LogUrgency(LogExceptionInterface.getLogException(ex, fullMethodName, "exceção produzida na Main")).Wait();
                LogLite.LogCaltion(LogExceptionInterface.getLogException(ex, fullMethodName, "exceção produzida na Main")).Wait();
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }
    }
}
