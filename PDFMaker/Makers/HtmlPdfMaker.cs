using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using App.Core;
using Spire.Pdf;
using Spire.Pdf.HtmlConverter;
using MarkdownSharp;

namespace App.Spires
{
    /// <summary>
    /// Html -> PDF
    /// </summary>
    public class HtmlPdfMaker : IPdfMaker
    {
        public string Filter { get; set; } = "html|*.html,*.htm";
        public static Markdown m = new Markdown();

        /// <summary>处理</summary>
        public void Process(string docPath, string savePath)
        {
            var html = File.ReadAllText(docPath);
            Helper.SavePdfFromHtml(html, savePath);
        }
    }
}
