using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using Microsoft.Win32;

namespace DBManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MWVM();
            //SQLService.BackupDatabase("DESKTOP-GDLC74K\\MSSQLSERVER12", "SOFTMARINE_COMPANY", @"C:\WORK\123123.bak");
            //var q = SQLService.GetDatabases("DESKTOP-GDLC74K\\MSSQLSERVER12");
            //BackupRestore.RestoreDatabase("DESKTOP-GDLC74K\\MSSQLSERVER12", "SOFTMARINE_SHIP", @"C:\WORK\SHIP_v47_EI.bak");
        }
    }
    
    
}