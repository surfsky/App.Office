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

namespace Watermarker
{
    class Program
    {
        static int Main(string[] args)
        {
            // 提示
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("Office Watermarker");
            Console.WriteLine("Version: {0}", IO.GetVersion(typeof(Program)));
            Console.WriteLine("Update: 2020-03-12");
            Console.WriteLine("Author: http://surfsky.github.com/");
            Console.WriteLine("Parameters: ");
            Console.WriteLine("  sourceFile : source office file path");
            Console.WriteLine("  targetFile : target file path");
            Console.WriteLine("  extFile    : target file extension");
            Console.WriteLine("  text       : watermark text");
            Console.WriteLine("-------------------------------------------");

            // 参数
            if (args.Length < 4)
            {
                Console.WriteLine("Please check parameters");
                Console.ReadKey();
                return -1;
            }
            var sourceFile = args[0];
            var targetFile = args[1];
            var ext = args[2];
            var text = args[3];
            var folder = IO.AssemblyDirectory;
            if (!sourceFile.Contains("\\"))
                sourceFile = IO.CombinePath(folder, sourceFile);
            if (!targetFile.Contains("\\"))
                targetFile = IO.CombinePath(folder, targetFile);

            // 处理
            try
            {
                Console.WriteLine("Processing...");
                ProtectOffice(sourceFile, targetFile, ext, text);
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
        private static void ProtectOffice(string sourceFile, string targetFile, string ext, string text)
        {
            var watermarker = DrawHelper.GetWatermarker(sourceFile);
            if (watermarker != null)
            {
                if (watermarker is WordWatermarker && ext == ".pdf")
                    watermarker.Config.TextColor = Color.FromArgb(255, 0, 0, 0);
                watermarker.Process(sourceFile, targetFile, true, text, "");
            }
        }
    }
}
