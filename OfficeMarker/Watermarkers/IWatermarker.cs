using App.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Spire
{
    /// <summary>
    /// 水印处理器接口
    /// </summary>
    public interface IWatermarker
    {
        /// <summary>文件过滤器。如 doc|*.doc</summary>
        string Filter { get; }

        /// <summary>配置信息</summary>
        WatermarkConfig Config { get; set; }

        /// <summary>处理</summary>
        void Process(string filePath, string savePath, bool useText, string text, string imgPath);
    }

    /// <summary>
    /// 水印配置信息
    /// </summary>
    public class WatermarkConfig
    {
        public Color TextColor { get; set; }
        public Font TextFont { get; set; }
        public int Padding { get; set; }
        public int Angle { get; set; }
    }
}
