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

namespace App.Spire
{
    /// <summary>
    /// PDF 水印处理器
    /// </summary>
    public class WordWatermarker : IWatermarker
    {
        public string Filter { get; set; } = "doc|*.doc|docx|*.docx";
        public WatermarkConfig Config { get; set; } = new WatermarkConfig()
        {
            TextColor = Color.FromArgb(255, 100, 100, 100),
            TextFont = new Font("Arial", 28),
            Padding = 100,
            Angle = -45
        };


        /// <summary>生成水印文件</summary>
        public void Process(string docPath, string savePath, bool useText, string text, string imgPath)
        {
            var doc = new Document();
            doc.LoadFromFile(docPath);
            var ext = savePath.GetFileExtension();
            if (ext.Contains("pdf"))
            {
                // 更改逻辑，先生成pdf，再打水印（这样可以实现每页都打上水印图片）
                doc.SaveToFile(savePath, FileFormat.PDF);
                doc.Close();
                new PdfWatermarker().Process(savePath, savePath, useText, text, imgPath);
            }
            else
            {
                // 统一用图片水印
                var size = new SizeF(800, 800);
                Image img = useText
                    ? Painter.CreateTextImage(text, size, Config.TextFont, Config.TextColor, Config.Padding, Config.Angle)
                    : Core.Painter.LoadImage(imgPath)
                    ;

                // 内置水印功能，会被前景挡住
                var picture = new PictureWatermark();
                picture.Picture = img;
                picture.Scaling = 80;
                doc.Watermark = picture;

                /*
                // 想给每页word都添加图片，现阶段实现不了。
                DocPicture picture = doc.Sections[0].Paragraphs[0].AppendPicture(img);
                picture.TextWrappingStyle = TextWrappingStyle.InFrontOfText;
                picture.HorizontalPosition = 0f;
                picture.VerticalPosition = 0f;
                picture.Width = img.Width;
                picture.Height = img.Height;
                */
                doc.SaveToFile(savePath);
                doc.Close();
            }
        }
    }
}
