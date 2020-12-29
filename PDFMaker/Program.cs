using App.Spires;
using App.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;
using System.IO;

namespace App.Spires
{
    class Program
    {
        static int Main(string[] args)
        {
            // 提示
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("PDF Maker");
            Console.WriteLine("Version: {0}", IO.GetVersion(typeof(Program)));
            Console.WriteLine("Update: 2020-12-28");
            Console.WriteLine("Author: http://surfsky.github.com/");
            Console.WriteLine("Parameters: ");
            Console.WriteLine("  sourceFile : source office file path");
            Console.WriteLine("  targetFile : target file path");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("");

            // 参数
            var sourceFile = "";
            var targetFile = "";
            if (args.Length < 2)
            {
                Console.Write("Source file: ");
                sourceFile = Console.ReadLine();
                Console.Write("Target file: ");
                targetFile = Console.ReadLine();
                if (sourceFile.IsEmpty() || targetFile.IsEmpty())
                {
                    Console.Write("Miss parameter, program will close.");
                    return -1;
                }
            }
            else
            {
                sourceFile = args[0];
                targetFile = args[1];
            }
            var folder = IO.AssemblyDirectory;
            if (!sourceFile.Contains("\\"))
                sourceFile = IO.CombinePath(folder, sourceFile);
            if (!targetFile.Contains("\\"))
                targetFile = IO.CombinePath(folder, targetFile);

            // 处理
            try
            {
                Console.WriteLine("Processing...");
                Process(sourceFile, targetFile);
                Console.WriteLine("OK {0}", targetFile);
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -2;
            }
        }

        /// <summary>创建Office保护文件</summary>
        private static void Process(string sourceFile, string targetFile)
        {
            var maker = Helper.GetMaker(sourceFile);
            if (maker != null)
            {
                maker.Process(sourceFile, targetFile);
            }
        }
    }
}
