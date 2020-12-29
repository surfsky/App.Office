using App.Core;
using Spire.Pdf;
using Spire.Pdf.HtmlConverter;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Spires
{
    public static class Helper
    {
        /// <summary>根据文件扩展名获取对应的处理器</summary>
        public static IPdfMaker GetMaker(string fileName)
        {
            var ext = fileName.GetFileExtension();
            if (ext.Contains("doc"))  return new WordPdfMaker();
            if (ext.Contains("xls"))  return new ExcelPdfMaker();
            if (ext.Contains("ppt"))  return new PptPdfMaker();
            if (ext.Contains("txt"))  return new TxtPdfMaker();
            if (ext.Contains("md"))   return new MdPdfMaker();
            if (ext.Contains("htm"))  return new HtmlPdfMaker();
            return null;
        }

        /// <summary>将 Html 文本保存为 Pdf 文件</summary>
        /// <param name="html">html 文本</param>
        /// <param name="savePath">pdf 文件完整路径</param>
        public static void SavePdfFromHtml(string html, string savePath)
        {
            // html -> pdf
            var doc = new PdfDocument();
            var setting = new PdfPageSettings();
            setting.Size = new SizeF(1000, 1000);
            setting.Margins = new Spire.Pdf.Graphics.PdfMargins(20);
            var format = new PdfHtmlLayoutFormat();
            format.IsWaiting = true;

            //doc.LoadFromHTML(html, true, setting, format);  // 直接写会报错，参考 https://blog.csdn.net/m0_37693130/article/details/86687509
            var thread = new Thread(() => { doc.LoadFromHTML(html, true, setting, format); });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();

            doc.SaveToFile(savePath);
            doc.Close();
        }

    }
}
