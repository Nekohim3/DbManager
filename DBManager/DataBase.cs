using System;

namespace DBManager
{
    [Serializable]
    public class DataBase
    {
        public string Instance { get; set; }
        public string Name     { get; set; }

        public override string ToString()
        {
            return $"{Instance}\\{Name}";
        }
    }
}