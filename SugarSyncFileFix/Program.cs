using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SugarSyncFileFix
{
    class Program
    {
        static void Main(string[] args)
        {
            var directoryToSearchIn = @"C:\Users\Maxim\Desktop\code\androidplotold";
            var searchString = " (from Maxim-laptop)";
            string[] filePaths = Directory.GetFiles(directoryToSearchIn, "*"+searchString+"*", SearchOption.AllDirectories);
            foreach(var s in filePaths)
            {
                Console.WriteLine(s);
            }
            Console.ReadLine();

            foreach(var i in filePaths)
            {
                var newPath = i.Remove(i.IndexOf(searchString), searchString.Length);
                // rename regular -> .old
                if(File.Exists(newPath))
                {
                    File.Move(newPath, newPath + ".old");
                }
                
                // rename (from Maxim-laptop) -> regular
                File.Move(i, newPath);
                
            }
        }
    }
}
