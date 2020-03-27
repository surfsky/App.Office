using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Core;
using Spire.Presentation;
using Spire.Presentation.Drawing;

namespace App.Spire
{
    /// <summary>
    /// 
    /// </summary>
    public class PptImager : IImager
    {
        public string Filter { get; set; } = "ppt|*.ppt|pptx|*.pptx";

        /// <summary>处理</summary>
        public List<string> Process(string filePath, string saveFolder)
        {
            var doc = new Presentation();
            doc.LoadFromFile(filePath);

            var files = new List<string>();
            for (int i=0; i<doc.Slides.Count; i++)
            {
                var path = string.Format("{0}\\{1:00}.png", saveFolder, i);
                var slide = doc.Slides[i];
                var image = slide.SaveAsImage(1024, 768);
                image.Save(path, ImageFormat.Png);
                files.Add(path);
            }
            return files;
        }
    }
}
