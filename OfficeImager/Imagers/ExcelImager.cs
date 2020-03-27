using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Core;
using Spire.Xls;

namespace App.Spire
{
    /// <summary>
    /// 
    /// </summary>
    public class ExcelImager : IImager
    {
        public string Filter { get; set; } = "xls|*.xls|xlsx|*.xlsx";

        /// <summary>处理</summary>
        public List<string> Process(string filePath, string saveFolder)
        {
            var doc = new Workbook();
            doc.LoadFromFile(filePath);

            var files = new List<string>();
            for (int i=0; i<doc.Worksheets.Count; i++)
            {
                var path = string.Format("{0}\\{1:00}.png", saveFolder, i);
                var sheet = doc.Worksheets[i];
                sheet.SaveToImage(path, ImageFormat.Png);
                files.Add(path);
            }
            return files;
        }
    }
}
