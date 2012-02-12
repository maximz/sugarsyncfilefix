using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SugarSyncFileFix
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Enter the directory you want to fix.");
            var directoryToSearchIn = Console.ReadLine(); // = @"C:\Users\Test\Desktop\code\";
            Console.WriteLine("Enter the relevant device name.");
            var searchString = " (from "+Console.ReadLine()+")"; // = " (from My-Device)";

            var filePaths = GetFiles(directoryToSearchIn, searchString);
            foreach(var s in filePaths)
            {
                Console.WriteLine(s);
            }
            Console.WriteLine(); Console.WriteLine(); Console.WriteLine();
            Console.WriteLine("Start changes?");
            Console.ReadLine();

            MakeChanges(searchString, filePaths);

            Console.WriteLine("Done! Hit any key to exit.");
            Console.ReadLine();
        }

        /// <summary>
        /// Returns list of merged files that need to be changed.
        /// </summary>
        /// <param name="dir">The directory we're looking in.</param>
        /// <param name="query">Search terms.</param>
        /// <returns></returns>
        public static string[] GetFiles(string dir, string query)
        {
            return Directory.GetFiles(dir, "*"+query+"*", SearchOption.AllDirectories);
        }
        /// <summary>
        /// Makes changes to file names of merged files.
        /// </summary>
        /// <param name="partOfName">The search terms/what part of the file name we're replacing.</param>
        /// <param name="paths">The files that need to be replaced.</param>
        public static void MakeChanges(string partOfName, string[] paths)
        {
            foreach (var i in paths)
            {
                var newPath = i.Remove(i.IndexOf(partOfName), partOfName.Length);
                // rename regular -> .old
                if (File.Exists(newPath))
                {
                    File.Move(newPath, newPath + ".old");
                }

                // rename (from Maxim-laptop) -> regular
                File.Move(i, newPath);
            }
        }
    }
}
