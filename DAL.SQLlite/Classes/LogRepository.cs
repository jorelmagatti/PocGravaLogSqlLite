using DAL.SQLlite.Interfaces;
using DAL.SQLlite.Repositorio;
using Dapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.SQLlite.Classes
{
    public class LogRepository : SqLiteBaseRepository
    {
        public async Task<bool> GravaLog(LogException log)
        {
            try
            {
                if (log is null)
                    return false;

                if (!File.Exists(DbFile))
                    if (!CriaDataBaseTabelaLog())
                        return false;

                await Task.Run(() => { 
                    using (var cnn = SimpleDbConnection())
                    {
                        cnn.Open();
                        log.ID = cnn.Query<long>(
                                    @"INSERT INTO LOG 
                                    (Data_Log, Message, LocalMethod, Detalhes, Source, StackTrace)
                                    VALUES 
                                    (@Data_Log, @Message, @LocalMethod, @Detalhes, @Source, @StackTrace);
                            select last_insert_rowid()", log).First();
                    }                
                });

                if (log.ID != 0)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }

        }

        private bool CriaDataBaseTabelaLog()
        {
            try
            {
                if (CriarArquivoDb())
                    using (var cnn = SimpleDbConnection())
                    {
                        cnn.Open();
                        cnn.Query(
                                    @"
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
                                            Detalhes    VARCHAR (500),
                                            Source      VARCHAR (500),
                                            StackTrace  VARCHAR (500) 
                                        );

                                        COMMIT TRANSACTION;
                                        PRAGMA foreign_keys = on;
                                    ");
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
