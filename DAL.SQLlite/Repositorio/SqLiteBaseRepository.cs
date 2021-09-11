using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DAL.SQLlite.Repositorio
{
    public class SqLiteBaseRepository
    {
        public static string DbFile { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\dados\Logs.db";

        public static SqliteConnection SimpleDbConnection()
        => new SqliteConnection("Data Source=" + DbFile);
        

        public static bool CriarArquivoDb()
        {
            try
            {
                DbFile = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\dados\Logs.db";

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
    }

}
