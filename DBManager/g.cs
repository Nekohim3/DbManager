using System;

namespace DBManager
{
    public static class g
    {
        public static Settings Settings { get; set; }
        public static string   CompName => Environment.MachineName;
    }
}