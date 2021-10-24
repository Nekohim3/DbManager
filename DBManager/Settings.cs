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
            if (DbList.Count(x => x.Instance == db.Instance && x.Name == db.Name) != 0) return false;

            DbList.Add(db);
            Save();
            return true;

        }

        public void RemoveDb(DataBase db)
        {
            DbList.Remove(db);
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