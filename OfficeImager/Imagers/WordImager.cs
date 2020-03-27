using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Core;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;

namespace App.Spire
{
    /// <summary>
    /// 
    /// </summary>
    public class WordImager : IImager
    {
        public string Filter { get; set; } = "doc|*.doc|docx|*.docx";

        /// <summary>处理</summary>
        public List<string> Process(string filePath, string saveFolder)
        {
            var doc = new Document();
            doc.LoadFromFile(filePath);

            var files = new List<string>();
            for (int i = 0; i < doc.BuiltinDocumentProperties.PageCount; i++)
            {
                var path = string.Format("{0}\\{1:00}.png", saveFolder, i);
                var image = doc.SaveToImages(i, ImageType.Metafile);
                image.Save(path, ImageFormat.Png);
                files.Add(path);
            }
            return files;
        }
    }
}
