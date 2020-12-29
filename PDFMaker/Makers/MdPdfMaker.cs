using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Core;
using Spire.Pdf;
using MarkdownSharp;
using System.IO;
using Spire.Pdf.HtmlConverter;
using System.Threading;

namespace App.Spires
{
    /// <summary>
    /// Markdown -> HTML -> PDF
    /// </summary>
    public class MdPdfMaker : IPdfMaker
    {
        public string Filter { get; set; } = "markdown|*.md";
        public static Markdown m = new Markdown();

        /// <summary>处理</summary>
        public void Process(string docPath, string savePath)
        {
            var txt = File.ReadAllText(docPath);
            var html = m.Transform(txt);
            Helper.SavePdfFromHtml(html, savePath);
        }


    }
}
