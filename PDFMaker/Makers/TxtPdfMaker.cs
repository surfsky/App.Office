using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Core;
using Spire.Xls;
using Spire.Pdf;
using System.IO;
using Spire.Pdf.Graphics;

namespace App.Spires
{
    /// <summary>
    /// Txt -> PDF
    /// </summary>
    public class TxtPdfMaker : IPdfMaker
    {
        public string Filter { get; set; } = "txt file|*.txt";

        /// <summary>处理</summary>
        public void Process(string docPath, string savePath)
        {
            var txt = File.ReadAllText(docPath);
            var doc = new PdfDocument();
            var page = doc.Pages.Add();
            page.Canvas.DrawString(
                txt,
                new PdfFont(PdfFontFamily.TimesRoman, 30f),
                new PdfSolidBrush(Color.Black), 
                10, 
                10
            );
            doc.SaveToFile(savePath);
            doc.Close();
        }
    }
}
