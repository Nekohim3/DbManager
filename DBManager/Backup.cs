using System;

namespace DBManager
{
    [Serializable]
    public class BackupClass
    {
        public string   Name       { get; set; }
        public string   Desc       { get; set; }
        public DateTime CreateTime { get; set; }
    }
}