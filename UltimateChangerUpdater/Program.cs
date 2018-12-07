using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
[assembly: System.Reflection.AssemblyVersion("1.0.3.0")]
namespace UltimateChangerUpdater
{
    class Program
    {

        static void Main(string[] args)
        /* arg 0 - path do update / "run: - uruchamia UC3 z updatera 
         * arg 1 - copyReku
         * arg 2 - copySettings      
         * arg 3 - copyUpdater
         * arg 4 - copyResources  
         * arg 5 - copyImages
         * 
         * dopisac podprogram ktory zrobi kopie updatera po zamknieciu update :
         *  kopiujemy wszystkie pliki a gdy przychodzi kopia updatera to zamykamy updater i uruchamiamy drugi program z Resources ? 
         *  gdy kopia się dokona zamykamy i odpalamy UC3 z nowego updatera bez dokonywania update ? nie bedzie problemow z miejscem odpalenia aplikacji chyba ze ten podprogram bedzie w Update
         *  
         */
        
        {
            Console.WriteLine("Update In Progress ...\n");

            closeUltimateChanger();
            Console.WriteLine("Ultimate Changer off\n");
            System.Threading.Thread.Sleep(2000);

            deleteOldFiles();
            Console.WriteLine("Old Files Deleted\n");
            try
            {
                if (args[0] == "run")
                    //if ("" == "run")
                    {
                    runNewUltimateChanger();
                    return;
                }
                else
                {
                    //copyNewFiles(@"\\10.128.3.1\DFS_data_SSC_FS_Images-SSC\PAZE\change_market\Multi_Changer\currentVersion\update", "false", "true", "false", "false", "false");
                    copyNewFiles(args[0], args[1], args[2], args[3], args[4], args[5]);
                    Console.WriteLine("New Files - Done\n");
                }

               
            }
            catch (Exception x)
            {
                Console.WriteLine("Error \n" + x.ToString());
            }
            try
            {
                CreateShortcut("shortcut Ultimate Changer", @"C:\Program Files\UltimateChanger\", @"C:\Program Files\UltimateChanger\Ultimate Changer.exe");
                Console.WriteLine("Shortcut Ultimate Changer - Done\n");
            }
            catch (Exception)
            {

            }
            System.Threading.Thread.Sleep(2000);

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

            foreach (var item in listOfFiles_reku)
            {
                try
                {
                    File.Delete(item);
                }
                catch (Exception)
                {
                    try
                    {
                        File.SetAttributes(item, FileAttributes.Normal);
                        File.Delete(item);
                    }
                    catch (UnauthorizedAccessException y)
                    {
                        Directory.Delete(@"C:\Program Files\UltimateChanger\reku", true);
                        listOfFiles_reku = null;
                    }
                    catch (Exception x)
                    {
                        Console.WriteLine("error in deleteOldFiles \n" + x.ToString());
                    }

                }

            }
        }

        public static void copyNewFiles(string from,string copyReku , string copySettings, string copyUpdater,string copyResources,string copyImages)
        {
            List<string> listOfFiles = Directory.GetFiles(from).ToList();
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

            //dopisanie funkcji na kopiowanie folderow i zawartości w folderze z przelacznikami co dokladnie kopiowac 

            if (copyReku == "true") // kopiowanie rekurencjion
            {
                listOfFiles = Directory.GetFiles(from + "\\reku").ToList();
                foreach (var item in listOfFiles)
                {
                    try
                    {
                        File.Copy(item, $"C:\\Program Files\\UltimateChanger\\reku\\{Path.GetFileName(item)}", true); // kopiowanie rekurencji
                    }
                    catch (Exception x)
                    {
                        Console.WriteLine(x.ToString());
                    }
                }
            }

            if (copySettings == "true")
            {


                List<string> listOfFiles_ = Directory.GetFiles(from + "\\Settings").ToList();

                
                foreach (var item in listOfFiles_)
                {
                    try
                    {
                        File.Copy(item, $"C:\\Program Files\\UltimateChanger\\Settings\\{Path.GetFileName(item)}", true); // kopiowanie settingow
                    }
                    catch (Exception x)
                    {
                        Console.WriteLine(x.ToString());
                    }
                }
            }



            if (copyResources == "true")
            {


                List<string> listOfFiles_ = Directory.GetFiles(@"C:\Program Files\UltimateChanger\Resources").ToList();

                foreach (var item in listOfFiles_)
                {
                    try
                    {
                        File.Delete(item);
                    }
                    catch (UnauthorizedAccessException y)
                    {
                        Directory.Delete(@"C:\Program Files\UltimateChanger\Resources",true);
                        listOfFiles_ = null;
                    }
                    catch (Exception x)
                    {
                        Console.WriteLine(x.ToString());
                    }

                }



                listOfFiles = Directory.GetFiles(from + "\\Resources").ToList();
                foreach (var item in listOfFiles)
                {
                    try
                    {
                        File.Copy(item, $"C:\\Program Files\\UltimateChanger\\Resources\\{Path.GetFileName(item)}", true); // kopiowanie settingow
                    }
                    catch (Exception x)
                    {
                        Console.WriteLine(x.ToString());
                    }
                }
            }

            if (copyImages == "true")
            {

                List<string> listOfFiles_ = Directory.GetFiles(@"C:\Program Files\UltimateChanger\Images").ToList();

                foreach (var item in listOfFiles_)
                {
                    try
                    {
                        File.Delete(item);
                    }
                    catch (UnauthorizedAccessException y)
                    {
                        Directory.Delete(@"C:\Program Files\UltimateChanger\Images", true);
                        listOfFiles_ = null;
                    }
                    catch (Exception x)
                    {
                        Console.WriteLine(x.ToString());
                    }

                }


                listOfFiles = Directory.GetFiles(from + "\\Images").ToList();
                foreach (var item in listOfFiles)
                {
                    try
                    {
                        File.Copy(item, $"C:\\Program Files\\UltimateChanger\\Images\\{Path.GetFileName(item)}", true); // kopiowanie settingow
                    }
                    catch (Exception x)
                    {
                        Console.WriteLine(x.ToString());
                    }
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

            if (copyUpdater == "true") // jezeli kopiowanie updatera to uruchamiamy podprogram z obecną tu logika po zamknieciu tego programu a nastepnie uruchamiamy UC3 z nowego updatera - dopisac przelacznik 
            {
                CreateShortcut("shortcut Ultimate Changer", @"C:\Program Files\UltimateChanger\", @"C:\Program Files\UltimateChanger\Ultimate Changer.exe");
                Process.Start(@"C:\Program Files\UltimateChanger\UpdaterCopy.exe", from +" true");
                return;
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
