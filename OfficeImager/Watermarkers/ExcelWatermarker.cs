using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Core;
using Spire.Xls;

namespace App.Spire
{
    /// <summary>
    /// PDF 水印处理器
    /// </summary>
    public class ExcelWatermarker : IWatermarker
    {
        public string Filter { get; set; } = "xls|*.xls|xlsx|*.xlsx";
        public WatermarkConfig Config { get; set; } = new WatermarkConfig()
        {
            TextColor = Color.FromArgb(30, 100, 100, 100),
            TextFont = new Font("Arial", 28),
            Padding = 100,
            Angle = -45
        };

        /// <summary>生成水印文件</summary>
        public void Process(string docPath, string savePath, bool useText, string text, string imgPath)
        {
            Workbook doc = new Workbook();
            doc.LoadFromFile(docPath);
            var ext = savePath.GetFileExtension();
            if (ext.Contains("pdf"))
            {
                doc.SaveToFile(savePath, FileFormat.PDF);
                doc.Dispose();
                new PdfWatermarker().Process(savePath, savePath, useText, text, imgPath);
            }
            else
            {
                // 水印图片
                var size = new SizeF((float)doc.Worksheets[0].PageSetup.PageWidth, (float)doc.Worksheets[0].PageSetup.PageHeight);
                Image img = useText
                    ? Painter.CreateTextImage(text, size, Config.TextFont, Config.TextColor, Config.Padding, Config.Angle)
                    : Core.Painter.LoadImage(imgPath)
                    ;

                // 设置水印背景图片
                foreach (Worksheet sheet in doc.Worksheets)
                {
                    // 设置背景方式，在手机端无法查看
                    //sheet.PageSetup.BackgoundImage = img as Bitmap;

                    //加载图片，添加到指定单元格
                    ExcelPicture picture = sheet.Pictures.Add(1, 1, img);
                    picture.Width = img.Width;
                    picture.Height = img.Height;
                    //picture.LeftColumnOffset = 75;
                    //picture.TopRowOffset = 20;
                }

                doc.SaveToFile(savePath, ExcelVersion.Version2010);
                doc.Dispose();
            }
        }
    }
}
