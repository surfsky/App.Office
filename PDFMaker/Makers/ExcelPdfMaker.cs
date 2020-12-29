using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Core;
using Spire.Xls;

namespace App.Spires
{
    /// <summary>
    /// Excel -> PDF
    /// </summary>
    public class ExcelPdfMaker : IPdfMaker
    {
        public string Filter { get; set; } = "xls|*.xls|xlsx|*.xlsx";

        /// <summary>处理</summary>
        public void Process(string docPath, string savePath)
        {
            Workbook doc = new Workbook();
            doc.LoadFromFile(docPath);
            doc.SaveToFile(savePath, FileFormat.PDF);
            doc.Dispose();
        }
    }
}
