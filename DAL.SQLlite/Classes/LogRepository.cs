using DAL.SQLlite.Interfaces;
using DAL.SQLlite.Repositorio;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.SQLlite.Classes
{
    public class LogRepository : SqLiteBaseRepository
    {
        private string DbFile { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + ConfigurationManager.AppSettings["FinalLocalLog"];
        
        public async Task<bool> GravaLog(LogExceptionInterface log)
        {
            try
            {
                if (log is null)
                    return false;

                if (!File.Exists(DbFile))
                    if (!CriaDataBaseTabelaLog(DbFile))
                        return false;

                string script = @"INSERT INTO LOG 
                                    (Data_Log, Message, LocalMethod,Tipo, Detalhes, Source, StackTrace)
                                    VALUES 
                                    (@Data_Log, @Message, @LocalMethod, @Tipo, @Detalhes, @Source, @StackTrace);
                            select last_insert_rowid()";

                return await ExecuteValidateSqLite<LogExceptionInterface>(log, DbFile, script);
            }
            catch (Exception)
            {
                return false;
            }

        }

        private bool CriaDataBaseTabelaLog(string DbFile)
        {
            try
            {
                string script = @"
                                        PRAGMA foreign_keys = off;
                                        BEGIN TRANSACTION;

                                        -- Table: LOG
                                        CREATE TABLE LOG (
                                            Id          INTEGER       PRIMARY KEY
                                                                        UNIQUE
                                                                        NOT NULL,
                                            Data_log    DATETIME,
                                            Message     VARCHAR (500),
                                            LocalMethod VARCHAR (500),
                                            Tipo    VARCHAR (500),
                                            Detalhes    VARCHAR (500),
                                            Source      VARCHAR (500),
                                            StackTrace  VARCHAR (500) 
                                        );

                                        COMMIT TRANSACTION;
                                        PRAGMA foreign_keys = on;
                                    ";
                if (CriarArquivoDb(DbFile))
                    return ExecuteScriptSqLite(DbFile, script);
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
