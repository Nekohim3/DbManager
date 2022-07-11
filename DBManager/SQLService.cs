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
            Logger.Info("GetSQLInstances()");
            try
            {
                var registryView = Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32;
                using (var hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, registryView))
                {
                    var instanceKey = hklm.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SQL Server\Instance Names\SQL", false);

                    var lst = instanceKey?.GetValueNames().Select(instanceName => instanceName).ToList();
                    Logger.Info("GetSQLInstances() succ");
                    if (lst != null)
                        foreach (var item in lst)
                            Logger.Info(item);

                    return lst;
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorQ(ex, "GetSQLInstances() -> Exception");
                return null;
            }
        }

        public static void RestoreDatabase(string serverName, string databaseName, string bakFilePath, string dataDirPath, MWVM vm)
        {
            Logger.Info("RestoreDatabase()");
            
            try
            {
                var conn = new ServerConnection
                           {
                               ServerInstance   = serverName,
                               StatementTimeout = int.MaxValue,
                               LoginSecure      = false,
                               Login            = g.Settings.Login,
                               Password         = g.Settings.Pass
                };

                var srv = new Server(conn);
                srv.KillAllProcesses(databaseName);
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
                Logger.Info("RestoreDatabase() succ");
            }
            catch (SmoException ex)
            {
                Logger.ErrorQ(ex, "RestoreDatabase -> SmoException");
            }
            catch (IOException ex)
            {
                Logger.ErrorQ(ex, "RestoreDatabase -> IOException");
            }
            catch (Exception ex)
            {
                Logger.ErrorQ(ex, "RestoreDatabase -> Exception");
            }
        }

        public static List<string> GetDatabases(string serverName)
        {
            Logger.Info($"GetDatabases({serverName})");
            try
            {
                var conn = new ServerConnection
                           {
                               ServerInstance   = serverName,
                               StatementTimeout = int.MaxValue,
                               LoginSecure      = false,
                               Login            = g.Settings.Login,
                               Password         = g.Settings.Pass
                };

                var srv = new Server(conn);

                var lst = (from Database db in srv.Databases where db.Name != "master" && db.Name != "tempdb" && db.Name != "model" && db.Name != "msdb" select db.Name).ToList();
                conn.Disconnect();
                Logger.Info($"GetDatabases({serverName}) succ");
                foreach (var item in lst)
                    Logger.Info(item);

                return lst;

            }
            catch (Exception ex)
            {
                Logger.ErrorQ(ex, $"GetDatabases({serverName}) -> Exception");
                return null;
            }
            
        }

        public static bool BackupDatabase(string serverName, string databaseName, string filePath, MWVM vm)
        {
            Logger.Info($"BackupDatabase({serverName}, {databaseName}, {filePath}, vm)");
            try
            {
                var conn = new ServerConnection
                           {
                               ServerInstance   = serverName,
                               StatementTimeout = int.MaxValue,
                               LoginSecure      = false,
                               Login            = g.Settings.Login,
                               Password         = g.Settings.Pass
                };
                var srv = new Server(conn);

                var bkp = new Backup
                          {
                              Action   = BackupActionType.Database,
                              Database = databaseName
                          };

                bkp.Devices.AddDevice(filePath, DeviceType.File);
                bkp.Incremental = false;

                bkp.PercentCompleteNotification =  5;
                bkp.PercentComplete             += (sender, args) => { vm.ProcessText = $"Создание бэкапа, плис вэит...{args.Percent}%"; };
                bkp.SqlBackup(srv);

                conn.Disconnect();
                Logger.Info($"BackupDatabase({serverName}, {databaseName}, {filePath}, vm) succ");
                return true;
            }

            catch (SmoException ex)
            {
                Logger.ErrorQ(ex, "BackupDatabase -> SmoException");
                return false;
            }
            catch (IOException ex)
            {
                Logger.ErrorQ(ex, "BackupDatabase -> IOException");
                return false;
            }
            catch (Exception ex)
            {
                Logger.ErrorQ(ex, "BackupDatabase -> Exception");
                return false;
            }
        }

        public static List<BackupClass> GetBackups(DataBase db)
        {
            Logger.Info($"GetBackups({db.Instance}_{db.Name})");
            try
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

                Logger.Info($"GetBackups({db.Instance}_{db.Name}) succ");
                return lst;
            }
            catch (Exception ex)
            {
                Logger.ErrorQ(ex, $"GetBackups({db.Instance}_{db.Name})");
                return null;
            }
        }

        public static bool RemoveDatabase(string serverName, string dbName)
        {
            Logger.Info($"RemoveDatabase({serverName}_{dbName})");
            try
            {
                var conn = new ServerConnection
                           {
                               ServerInstance   = serverName,
                               StatementTimeout = int.MaxValue,
                               LoginSecure      = false,
                               Login            = g.Settings.Login,
                               Password         = g.Settings.Pass
                };

                var srv = new Server(conn);

                foreach (Database item in srv.Databases)
                {
                    if (item.Name != dbName) continue;
                    srv.KillAllProcesses(item.Name);
                    item.Drop();

                    Logger.Info($"RemoveDatabase({dbName}_{dbName}) succ");
                    return true;
                }

                Logger.Info($"RemoveDatabase({dbName}_{dbName}) fail (db not found)");
                return false;
            }

            catch (SmoException ex)
            {
                Logger.ErrorQ(ex, "RemoveDatabase -> SmoException");
                return false;
            }
            catch (IOException ex)
            {
                Logger.ErrorQ(ex, "RemoveDatabase -> IOException");
                return false;
            }
            catch (Exception ex)
            {
                Logger.ErrorQ(ex, "RemoveDatabase -> Exception");
                return false;
            }
        }

        public static string GetUsedBackup(DataBase db)
        {
            Logger.Info($"GetUsedBackup({db.Instance}\\{db.Name})");
            foreach (var item in Directory.GetDirectories($"{g.Settings.DirForDbData}\\{db.Instance}_{db.Name}"))
            {
                var files = Directory.GetFiles(item);
                if (files.Length > 2)
                {
                    Logger.Info($"GetUsedBackup({db.Instance}\\{db.Name}) succ");
                    return Path.GetFileNameWithoutExtension(files.First(x => x.EndsWith(".mdf")));
                }
            }
            Logger.Info($"GetUsedBackup({db.Instance}\\{db.Name}) failed (not found)");
            return "";
        }
    }
}