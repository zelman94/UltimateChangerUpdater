using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
[assembly: System.Reflection.AssemblyVersion("1.0.0.0")]
namespace UpdaterCopy
{
    class CopyUpdaterMain
    {
        static void Main(string[] args)
        {
            string from = "";
            string runUC3 = "";
            try
            {
                    from = args[0];
                    runUC3 = args[1]; // true/false
            }
            catch (Exception)
            {
                Console.WriteLine("No Arg 0 \n what to copy");
                Console.ReadKey();
                return;
            }


            List<string> listOfFiles = Directory.GetFiles(from + "\\Updater").ToList();
            foreach (var item in listOfFiles)
            {
                try
                {
                    File.Copy(item, $"C:\\Program Files\\UltimateChanger\\Updater\\{Path.GetFileName(item)}", true); // kopiowanie settingow
                }
                catch (Exception x)
                {
                    Console.WriteLine(x.ToString());
                }
            }
            if (runUC3=="true")
            {
                runNewUltimateChanger();
            }
            
            return;
        }


        public static void runNewUltimateChanger()
        {
            Process.Start(@"C:\Program Files\UltimateChanger\Ultimate Changer.exe");
        }

    }
}
