using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.Win32;

namespace DBManager
{
    public static class SQLService
    {
        public static List<string> GetSQLInstances()  
        {  
            var registryView = Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32;  
            using (var hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, registryView))  
            {  
                var instanceKey = hklm.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SQL Server\Instance Names\SQL", false);

                return instanceKey?.GetValueNames().Select(instanceName => instanceName).ToList();
            }  
        }

        public static void RestoreDatabase(string serverName, string databaseName, string bakFilePath, string dataDirPath)
        {

            var conn = new ServerConnection
            {
                    ServerInstance = serverName
            };

            var srv  = new Server(conn);
            var path = Path.Combine(g.Settings.DirForDbData, databaseName);

            try
            {
                var res = new Restore();

                res.Devices.AddDevice(bakFilePath, DeviceType.File);

                //var MDF = res.ReadFileList(srv).Rows[0][1].ToString();
                var DataFile = new RelocateFile
                {
                        LogicalFileName  = res.ReadFileList(srv).Rows[0][0].ToString(),
                        PhysicalFileName = $"{path}\\"
                };

                var LogFile = new RelocateFile();
                var       LDF     = res.ReadFileList(srv).Rows[1][1].ToString();
                LogFile.LogicalFileName  = res.ReadFileList(srv).Rows[1][0].ToString();
                LogFile.PhysicalFileName = @"C:\WORK\qwe.ldf";

                var FileStore = new RelocateFile();
                var       FS     = res.ReadFileList(srv).Rows[2][1].ToString();
                FileStore.LogicalFileName  = res.ReadFileList(srv).Rows[2][0].ToString();
                FileStore.PhysicalFileName = @"C:\WORK\FileStore";

                res.RelocateFiles.Add(DataFile);
                res.RelocateFiles.Add(LogFile);
                res.RelocateFiles.Add(FileStore);

                res.Database = databaseName;
                res.NoRecovery = false;
                res.ReplaceDatabase = true;
                res.SqlRestore(srv);
                conn.Disconnect();
            }
            catch (SmoException ex)
            {
                throw new SmoException(ex.Message, ex.InnerException);
            }
            catch (IOException ex)
            {
                throw new IOException(ex.Message, ex.InnerException);
            }
        }
        
        public static List<string> GetDatabases(string serverName)
        {
            var conn = new ServerConnection
            {
                    ServerInstance = serverName
            };

            var srv = new Server(conn);

            var lst = (from Database db in srv.Databases where db.Name != "master" && db.Name != "tempdb" && db.Name != "model" && db.Name != "msdb" select db.Name).ToList();
            conn.Disconnect();
            return lst;
        }
        
        public static void BackupDatabase(string serverName, string databaseName, string filePath)
        {
            var conn                = new ServerConnection();
            conn.ServerInstance = serverName;
            var srv                 = new Server(conn);

            try
            {
                Backup bkp = new Backup();

                bkp.Action   = BackupActionType.Database;
                bkp.Database = databaseName;

                bkp.Devices.AddDevice(filePath, DeviceType.File);
                bkp.Incremental = false;

                bkp.SqlBackup(srv);

                conn.Disconnect();
            }

            catch (SmoException ex)
            {
                throw new SmoException(ex.Message, ex.InnerException);
            }
            catch (IOException ex)
            {
                throw new IOException(ex.Message, ex.InnerException);
            }
        }
    }
}