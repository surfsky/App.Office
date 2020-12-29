using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Core;
using Spire.Presentation;
using Spire.Presentation.Drawing;

namespace App.Spires
{
    /// <summary>
    /// PPT -> Pdf
    /// </summary>
    public class PptPdfMaker : IPdfMaker
    {
        public string Filter { get; set; } = "ppt|*.ppt|pptx|*.pptx";

        /// <summary>处理</summary>
        public void Process(string docPath, string savePath)
        {
            var ppt = new Presentation();
            ppt.LoadFromFile(docPath);
            ppt.SaveToFile(savePath, FileFormat.PDF);
            ppt.Dispose();
        }
    }
}
