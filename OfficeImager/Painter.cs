using App.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Spire
{
    public class Painter
    {
        /// <summary>获取图片转化器</summary>
        public static IImager GetImager(string fileName)
        {
            var ext = fileName.GetFileExtension();
            if (ext.Contains("doc"))
                return new WordImager();
            if (ext.Contains("xls"))
                return new ExcelImager();
            if (ext.Contains("ppt"))
                return new PptImager();
            if (ext.Contains("pdf"))
                return new PdfImager();
            return null;
        }

        /// <summary>获取水印器</summary>
        public static IWatermarker GetWatermarker(string fileName)
        {
            var ext = fileName.GetFileExtension();
            if (ext.Contains("doc"))
                return new WordWatermarker();
            if (ext.Contains("xls"))
                return new ExcelWatermarker();
            if (ext.Contains("ppt"))
                return new PptWatermarker();
            if (ext.Contains("pdf"))
                return new PdfWatermarker();
            return null;
        }

        /// <summary>创建文字图片</summary>
        public static Image CreateTextImage(
            String text, 
            SizeF size, 
            Font textFont = null,
            Color? textColor = null, 
            int padding = 100,
            float angle = -45, 
            Color? backColor = null)
        {
            // 参数校对
            textFont = textFont ?? new Font("arial", 28);  // 最小28，不知道为什么
            backColor = backColor ?? Color.Transparent;
            textColor = textColor ?? Color.FromArgb(30, 200, 200, 200);

            // 创建图像
            float width = size.Width;
            float height = size.Height;
            Image img = new Bitmap((int)width, (int)height);
            Graphics g = Graphics.FromImage(img);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.CompositingQuality = CompositingQuality.HighQuality;
            SizeF textSize = g.MeasureString(text, textFont);

            // 旋转
            g.TranslateTransform((int)width/2, (int)height/2);
            g.RotateTransform(angle);
            g.TranslateTransform(-(int)width/2, -(int)height/2);
            
            // 绘制
            g.Clear(backColor.Value);
            Brush textBrush = new SolidBrush(textColor.Value);

            // 平铺绘制文字
            var countX = width * 2 / (textSize.Width + padding);
            var countY = height/ (textSize.Height + padding);
            g.TextRenderingHint = TextRenderingHint.AntiAlias;
            for (int i=0; i<countX; i++)
            {
                for (int j=0; j<countY; j++)
                {
                    var x = i * (textSize.Width + padding) - width/2;   // 左偏移，可填满旋转后的空余空间
                    var y = j * (textSize.Height + padding);
                    g.DrawString(text, textFont, textBrush, x, y);
                }
            }
            g.Save();

            //var fileName = string.Format("watermark-{0:yyMMdd-HHmmss}.png", DateTime.Now);
            //img.Save(fileName);
            return img;
        }
    }
}
