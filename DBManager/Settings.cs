using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace DBManager
{
    [Serializable]
    public class Settings
    {
        public string                         DirForDbData { get; set; }
        public ObservableCollection<DataBase> DbList       { get; set; }
        public string                         Login        { get; set; }
        public string                         Pass         { get; set; }

        public Settings()
        {
            DbList = new ObservableCollection<DataBase>();
        }

        public void SetDir(string path)
        {
            DirForDbData = path;
            Save();
        }

        public bool AddDb(DataBase db)
        {
            Logger.Info($"AddDb({db.Instance}_{db.Name})");
            if (DbList.Count(x => x.Instance == db.Instance && x.Name == db.Name) != 0)
            {
                Logger.Info($"AddDb({db.Instance}_{db.Name}) fail: alreadyExist");
                return false;

            }

            DbList.Add(db);
            Save();
            Logger.Info($"AddDb({db.Instance}_{db.Name}) succ");
            return true;

        }

        public void RemoveDb(DataBase db)
        {
            var q = DbList.Remove(db);
            Logger.Info($"RemoveDb({db.Instance}_{db.Name}) {(q ? "succ" : "fail (not found)")}");
            Save();
        }
        
        

        public static Settings Load()
        {
            if (!File.Exists("settings.xml")) return null;
            var formatter = new XmlSerializer(typeof(Settings));

            using (var fs = new FileStream("settings.xml", FileMode.Open))
                return (Settings)formatter.Deserialize(fs);
        }

        public void Save()
        {
            var formatter = new XmlSerializer(typeof(Settings));

            using (var fs = new FileStream("settings.xml", FileMode.Create))
                formatter.Serialize(fs, this);
        }
    }
}