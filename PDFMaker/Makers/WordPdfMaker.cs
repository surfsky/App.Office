using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Core;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;

namespace App.Spires
{
    /// <summary>
    /// Word -> Pdf
    /// </summary>
    public class WordPdfMaker : IPdfMaker
    {
        public string Filter { get; set; } = "doc|*.doc|docx|*.docx";

        /// <summary>处理</summary>
        public void Process(string docPath, string savePath)
        {
            var doc = new Document();
            doc.LoadFromFile(docPath);
            doc.SaveToFile(savePath, FileFormat.PDF);
            doc.Close();
        }
    }
}
