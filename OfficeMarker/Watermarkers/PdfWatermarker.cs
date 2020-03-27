using App.Core;
using Spire.Pdf;
using Spire.Pdf.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Spire
{
    /// <summary>
    /// PDF 水印处理器
    /// </summary>
    public class PdfWatermarker : IWatermarker
    {
        public string Filter { get; set; } = "pdf|*.pdf;";
        public WatermarkConfig Config { get; set; } = new WatermarkConfig()
        {
            TextColor = Color.FromArgb(255, 100, 100, 100),
            TextFont = new Font("Arial", 20),
            Padding = 100,
            Angle = -45
        };

        /// <summary>生成水印文件</summary>
        public void Process(string docPath, string savePath, bool useText, string text, string imgPath)
        {
            // 统一用图片水印
            var size = new SizeF(1200, 1200);
            var bgColor = Color.White;
            Image img = useText
                ? DrawHelper.CreateTextImage(text, size, Config.TextFont, Config.TextColor, Config.Padding, Config.Angle, bgColor)
                : Painter.LoadImage(imgPath)
                ;

            //创建一个新的PDF实例,导入PDF文件 
            PdfDocument doc = new PdfDocument();
            doc.LoadFromFile(docPath);
            foreach (PdfPageBase page in doc.Pages)
            {
                // 背景图片方式，pdf会变得一片漆黑
                //page.BackgroundImage = img;

                // 放置前景图片方式
                page.Canvas.Save();
                page.Canvas.SetTransparency(0.15f);
                PdfImage pdfImage = PdfImage.FromImage(img);
                page.Canvas.DrawImage(pdfImage, 0, 0);
                page.Canvas.Restore();
            }

            // 保存
            doc.SaveToFile(savePath);
            doc.Close();
        }
    }
}
