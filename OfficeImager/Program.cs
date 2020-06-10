using App.Spire;
using App.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;
using System.IO;

namespace App.Spire
{
    class Program
    {
        static int Main(string[] args)
        {
            // 提示
            Console.WriteLine("---------------------------------------------------------");
            Console.WriteLine("Office File To Images");
            Console.WriteLine("Version: {0}", IO.GetVersion(typeof(Program)));
            Console.WriteLine("Update: 2020-03-15");
            Console.WriteLine("Author: http://surfsky.github.com/");
            Console.WriteLine("Parameters: ");
            Console.WriteLine("  sourceFile : source office file path");
            Console.WriteLine("  targetFolder : target folder to contains images");
            Console.WriteLine("---------------------------------------------------------");

            // 参数
            var sourceFile = "";
            var targetFolder = "";
            if (args.Length < 2)
            {
                Console.Write("Source files : ");
                sourceFile = Console.ReadLine();
                Console.Write("Target folder : ");
                targetFolder = Console.ReadLine();
            }
            else
            {
                sourceFile = args[0];
                targetFolder = args[1];
            }
            var folder = IO.AssemblyDirectory;
            if (!sourceFile.Contains("\\"))
                sourceFile = IO.CombinePath(folder, sourceFile);
            if (!targetFolder.Contains("\\"))
                targetFolder = IO.CombinePath(folder, targetFolder);

            // 处理
            try
            {
                Console.WriteLine("Processing...");
                IO.DeleteDir(targetFolder);
                IO.PrepareDirectory(targetFolder);
                Process(sourceFile, targetFolder);
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -2;
            }
        }

        /// <summary>处理</summary>
        private static void Process(string sourceFile, string targetFolder)
        {
            var convertor = Painter.GetImager(sourceFile);
            if (convertor != null)
            {
                var paths = convertor.Process(sourceFile, targetFolder);
                foreach (var path in paths)
                    Console.WriteLine(path);
            }
        }
    }
}
