using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
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

        public static void RestoreDatabase(string serverName, string databaseName, string bakFilePath, string dataDirPath, MWVM vm)
        {

            var conn = new ServerConnection
            {
                    ServerInstance   = serverName,
                    StatementTimeout = int.MaxValue
            };
            var srv  = new Server(conn);
            var path = Path.Combine(g.Settings.DirForDbData, databaseName);

            try
            {
                var res = new Restore();
                res.Devices.AddDevice(bakFilePath, DeviceType.File);

                res.RelocateFiles.Add(new RelocateFile
                                      {
                                          LogicalFileName  = res.ReadFileList(srv).Rows[0][0].ToString(),
                                          PhysicalFileName = $"{dataDirPath}\\{Path.GetFileNameWithoutExtension(bakFilePath)}.mdf"
                                      });
                res.RelocateFiles.Add(new RelocateFile
                                                {
                                                    LogicalFileName  = res.ReadFileList(srv).Rows[1][0].ToString(),
                                                    PhysicalFileName = $"{dataDirPath}\\{Path.GetFileNameWithoutExtension(bakFilePath)}.ldf"
                                                });
                res.RelocateFiles.Add(new RelocateFile
                                      {
                                          LogicalFileName  = res.ReadFileList(srv).Rows[2][0].ToString(),
                                          PhysicalFileName = $"{dataDirPath}\\Filestore"
                                      });

                
                res.Database                    =  databaseName;
                res.NoRecovery                  =  false;
                res.ReplaceDatabase             =  true;
                res.PercentCompleteNotification =  5;
                res.PercentComplete             += (sender, args) => { vm.ProcessText = $"Восстановление бэкапа, плис вэит...{args.Percent}%"; };
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
        
        public static bool BackupDatabase(string serverName, string databaseName, string filePath, MWVM vm)
        {
            var conn = new ServerConnection
                       {
                           ServerInstance   = serverName,
                           StatementTimeout = int.MaxValue
                       };
            var srv                 = new Server(conn);

            try
            {
                Backup bkp = new Backup();

                bkp.Action   = BackupActionType.Database;
                bkp.Database = databaseName;

                bkp.Devices.AddDevice(filePath, DeviceType.File);
                bkp.Incremental = false;

                bkp.PercentCompleteNotification = 5;
                bkp.PercentComplete += (sender, args) => { vm.ProcessText = $"Создание бэкапа, плис вэит...{args.Percent}%"; };
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
            catch (Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }

            return true;
        }

        public static List<BackupClass> GetBackups(DataBase db)
        {
            if (!Directory.Exists($"{g.Settings.DirForDbData}\\{db.Instance}_{db.Name}"))
                Directory.CreateDirectory($"{g.Settings.DirForDbData}\\{db.Instance}_{db.Name}");

            var lst = new List<BackupClass>();

            foreach (var item in Directory.GetDirectories($"{g.Settings.DirForDbData}\\{db.Instance}_{db.Name}"))
            {
                if (!File.Exists($"{item}\\data.xml")) continue;
                var formatter = new XmlSerializer(typeof(BackupClass));

                using (var fs = new FileStream($"{item}\\data.xml", FileMode.Open))
                    lst.Add((BackupClass)formatter.Deserialize(fs));
            }

            return lst;
        }

        public static bool RemoveDatabase(string serverName, string dbName)
        {
            var conn = new ServerConnection { ServerInstance = serverName };

            var srv = new Server(conn);

            try
            {
                foreach (Database item in srv.Databases)
                {
                    if (item.Name != dbName) continue;
                    item.Drop();
                    return true;
                }
            }
            catch (SmoException ex)
            {
                throw new SmoException(ex.Message, ex.InnerException);
            }
            catch (IOException ex)
            {
                throw new IOException(ex.Message, ex.InnerException);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

            return true;
        }

        public static string GetUsedBackupNameFromDb(string serverName, string dbName)
        {
            var conn = new ServerConnection { ServerInstance = serverName };

            var srv = new Server(conn); 
            try
            {
                foreach (Database item in srv.Databases)
                {
                    if (item.Name != dbName) continue;
                    return Path.GetFileNameWithoutExtension(item.PrimaryFilePath);
                }
            }
            catch (SmoException ex)
            {
                throw new SmoException(ex.Message, ex.InnerException);
            }
            catch (IOException ex)
            {
                throw new IOException(ex.Message, ex.InnerException);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

            return "";
        }

        public static string GetUsedBackup(DataBase db)
        {
            foreach (var item in Directory.GetDirectories($"{g.Settings.DirForDbData}\\{db.Instance}_{db.Name}"))
            {
                var files = Directory.GetFiles(item);
                if (files.Length > 2) return Path.GetFileNameWithoutExtension(files.First(x => x.EndsWith(".mdf")));
            }

            return "";
        }
    }
}