using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Dapper;
using System.Threading.Tasks;
using DAL.SQLlite.Interfaces;
using System.Linq;

namespace DAL.SQLlite.Repositorio
{
    public class SqLiteBaseRepository
    {
        public static SqliteConnection SimpleDbConnection(string DbFile = "")
        => new SqliteConnection("Data Source=" + DbFile);
        
        internal static bool CriarArquivoDb(string DbFile = "")
        {
            try
            {
                if(!Directory.Exists(Path.GetDirectoryName(DbFile)))
                    Directory.CreateDirectory(Path.GetDirectoryName(DbFile));

                if(!File.Exists(DbFile))
                {
                    StreamWriter file = new StreamWriter(DbFile, true, Encoding.Default); 
                    file.Dispose();
                    return true;
                }

                return false;
            }
            catch(Exception)
            {
                return false;
            }
        }

        internal async Task<bool> ExecuteValidateSqLite<T>(T objeto, string DbFile, string script)
        {
            try
            {
                long validate = 0;

                if (objeto is null)
                    return false;

                if (!File.Exists(DbFile))
                        return false;

                await Task.Run(() => {
                    using (var cnn = SimpleDbConnection(DbFile))
                    {
                        cnn.Open();
                        validate = cnn.Query<long>(script, objeto).First();
                    }
                });
                return validate != 0 ? true : false;               
            }
            catch (Exception)
            {
                return false;
            }
        }

        internal bool ExecuteScriptSqLite(string DbFile, string script)
        {
            try
            {
                if (CriarArquivoDb())
                    using (var cnn = SimpleDbConnection(DbFile))
                    {
                        cnn.Open();
                        cnn.Query(script);
                    }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

}
