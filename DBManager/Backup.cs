using System;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Practices.Prism.ViewModel;

namespace DBManager
{
    [Serializable]
    public class BackupClass : NotificationObject
    {
        private bool     _inUse;
        public  string   Name       { get; set; }
        public  string   Desc       { get; set; }
        public  DateTime CreateTime { get; set; }

        [XmlIgnore]
        public bool InUse
        {
            get => _inUse;
            set
            {
                _inUse = value;
                RaisePropertyChanged(() => InUse);
            }
        }

        public BackupClass()
        {
        }

        public void Save(string path)
        {
            var formatter = new XmlSerializer(typeof(BackupClass));

            using (var fs = new FileStream(path, FileMode.Create))
                formatter.Serialize(fs, this);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}