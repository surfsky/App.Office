using App.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Spires
{
    /// <summary>
    /// PDF 生成器接口
    /// </summary>
    public interface IPdfMaker
    {
        /// <summary>文件过滤器。如 doc|*.doc</summary>
        string Filter { get; }

        /// <summary>处理</summary>
        void Process(string filePath, string savePath);
    }
}
