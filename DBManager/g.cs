using System;
using System.IO;
using System.Xml.Serialization;
using DBManager.dbjson;

namespace DBManager
{
    public static class g
    {
        public static Settings  Settings { get; set; }
        public static string    CompName => Environment.MachineName;
        public static object    Lock = new object();
        public static DataStore Db { get; set; }

        public static void Init()
        {
            Db = new DataStore($"Database.db", reloadBeforeGetCollection: true);
        }
    }
}