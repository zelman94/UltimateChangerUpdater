using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateChangerUpdater
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Update In Progress ...");
            closeUltimateChanger();
            //deleteOldFiles();            
            try
            {
                //copyNewFiles(args[0]);


                List<string> listOfFiles = Directory.GetFiles(args[0]).ToList();
                foreach (var item in listOfFiles)
                {
                    try
                    {
                        File.Copy(item, $"C:\\Program Files\\UltimateChanger\\{Path.GetFileName(item)}",true); // kopiowanie glownych plikow
                    }
                    catch (Exception x)
                    {
                        Console.WriteLine(x.ToString());
                    }

                }


                //copyNewFiles(@"\\10.128.3.1\DFS_data_SSC_FS_Images-SSC\PAZE\change_market\Multi_Changer\v_3.0.0_TEST");
            }
            catch (Exception x)
            {
                Console.WriteLine("Error \n" + x.ToString());
            }
            CreateShortcut("shortcut Ultimate Changer", @"C:\Program Files\UltimateChanger\", @"C:\Program Files\UltimateChanger\Ultimate Changer.exe");


            runNewUltimateChanger();

        }

        public static void closeUltimateChanger()
        {
            Process[] proc = Process.GetProcessesByName("Ultimate Changer");
            Process[] localAll = Process.GetProcesses();
            foreach (Process item in localAll)
            {
                string tmop = item.ProcessName;
                if (tmop == "Ultimate Changer")
                {
                    item.Kill();
                    return;
                }
            }
        }

        public static void deleteOldFiles()
        {
           List<string> listOfFiles = Directory.GetFiles(@"C:\Program Files\UltimateChanger").ToList();
           List<string> listOfFiles_reku = Directory.GetFiles(@"C:\Program Files\UltimateChanger\reku").ToList();

            foreach (var item in listOfFiles)
            {
                try
                {
                    File.Delete(item);
                }
                catch (Exception x)
                {
                    Console.WriteLine(x.ToString());                 
                }
                
            }
            foreach (var item in listOfFiles_reku)
            {
                try
                {
                    File.Delete(item);
                }
                catch (Exception x)
                {
                    Console.WriteLine(x.ToString());
                }

            }
        }

        public static void copyNewFiles(string from)
        {
            List<string> listOfFiles = Directory.GetFiles(from).ToList();
            foreach (var item in listOfFiles)
            {
                try
                {
                    File.Copy(item, $"C:\\Program Files\\UltimateChanger\\{Path.GetFileName(item)}"); // kopiowanie glownych plikow
                }
                catch (Exception x)
                {
                    Console.WriteLine(x.ToString());
                }

            }

            listOfFiles = Directory.GetFiles(from +"\\reku").ToList();
            foreach (var item in listOfFiles)
            {
                try
                {
                    File.Copy(item, $"C:\\Program Files\\UltimateChanger\\reku\\{Path.GetFileName(item)}",true); // kopiowanie rekurencji
                }
                catch (Exception x)
                {
                    Console.WriteLine(x.ToString());
                }

            }
            try
            {
                //Now Create all of the directories
                foreach (string dirPath in Directory.GetDirectories(from + "\\Downgrade", "*",
                    SearchOption.AllDirectories))
                    Directory.CreateDirectory(dirPath.Replace(from + "\\Downgrade", $"C:\\Program Files\\UltimateChanger\\Downgrade"));

                //Copy all the files & Replaces any files with the same name
                foreach (string newPath in Directory.GetFiles(from + "\\Downgrade", "*.*",
                    SearchOption.AllDirectories))
                    File.Copy(newPath, newPath.Replace(from + "\\Downgrade", $"C:\\Program Files\\UltimateChanger\\Downgrade"), true);
            }
            catch (Exception x)
            {
                Console.WriteLine(x.ToString());
            }


        }

        public static void runNewUltimateChanger()
        {
            Process.Start(@"C:\Program Files\UltimateChanger\Ultimate Changer.exe");
        }


        public static void CreateShortcut(string shortcutName, string shortcutPath, string targetFileLocation)
            {

            try // usuwan stary
            {
                File.Delete(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),"Ultimate Changer.lnk"));
            }
            catch (Exception)
            {

            }

                string shortcutLocation = System.IO.Path.Combine(shortcutPath, shortcutName + ".lnk");
            IWshRuntimeLibrary.WshShell shell = new IWshRuntimeLibrary.WshShell();
            IWshRuntimeLibrary.IWshShortcut shortcut = (IWshRuntimeLibrary.IWshShortcut)shell.CreateShortcut(shortcutLocation);

                shortcut.Description = "Ultimate Changer";   // The description of the shortcut
                shortcut.IconLocation = @"C:\Program Files\UltimateChanger\icon.ico";           // The icon of the shortcut
                shortcut.TargetPath = targetFileLocation;                 // The path of the file that will launch when the shortcut is run
                shortcut.WorkingDirectory = @"C:\Program Files\UltimateChanger";
                shortcut.Save();                                    // Save the shortcut

            File.Move(shortcutLocation, System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Ultimate Changer.lnk"));

            }

        }
}
