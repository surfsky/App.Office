using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Core;
using Spire.Presentation;
using Spire.Presentation.Drawing;

namespace App.Spire
{
    /// <summary>
    /// PDF 水印处理器
    /// </summary>
    public class PptWatermarker : IWatermarker
    {
        public string Filter { get; set; } = "ppt|*.ppt|pptx|*.pptx";
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
            // 加载PowerPoint文档
            Presentation ppt = new Presentation();
            ppt.LoadFromFile(docPath);
            var ext = savePath.GetFileExtension();
            if (ext.Contains("pdf"))
            {
                ppt.SaveToFile(savePath, FileFormat.PDF);
                ppt.Dispose();
                new PdfWatermarker().Process(savePath, savePath, useText, text, imgPath);
            }
            else
            {
                // 图片
                var size = ppt.SlideSize.Size;
                Image img = useText
                    ? DrawHelper.CreateTextImage(text, size, Config.TextFont, Config.TextColor, Config.Padding, Config.Angle)
                    : Painter.LoadImage(imgPath)
                    ;
                IImageData image = ppt.Images.Append(img);

                // 设置背景
                foreach (ISlide slide in ppt.Slides)
                {
                    /*
                    //设置文档的背景填充模式为图片填充（由于ppt是有很多图片构成的，背景图片往往会被覆盖掉，设置背景图片的方式不合适）
                    slide.SlideBackground.Type = Spire.Presentation.Drawing.BackgroundType.Custom;
                    slide.SlideBackground.Fill.FillType = FillFormatType.Picture;
                    slide.SlideBackground.Fill.PictureFill.FillType = PictureFillType.Stretch;
                    slide.SlideBackground.Fill.PictureFill.Picture.EmbedImage = image;
                    */

                    // 创建图像Shape
                    RectangleF rect = new RectangleF(0, 0, size.Width, size.Height);
                    var shape = slide.Shapes.AppendEmbedImage(ShapeType.Rectangle, image, rect);
                    shape.Line.FillFormat.SolidFillColor.Color = Color.Transparent;
                }

                // 保存文档
                ppt.SaveToFile(savePath, FileFormat.Pptx2010);
                ppt.Dispose();
            }
        }
    }
}
