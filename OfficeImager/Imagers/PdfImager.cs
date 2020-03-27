using App.Core;
using Spire.Pdf;
using Spire.Pdf.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Spire
{
    /// <summary>
    /// 
    /// </summary>
    public class PdfImager : IImager
    {
        public string Filter { get; set; } = "pdf|*.pdf;";

        /// <summary>处理</summary>
        public List<string> Process(string filePath, string saveFolder)
        {
            var doc = new PdfDocument();
            doc.LoadFromFile(filePath);

            var files = new List<string>();
            for (int i=0; i<doc.Pages.Count; i++)
            {
                var path = string.Format("{0}\\{1:00}.png", saveFolder, i);
                var image = doc.SaveAsImage(i, PdfImageType.Metafile);
                image.Save(path, ImageFormat.Png);
                files.Add(path);
            }
            return files;
        }
    }
}
