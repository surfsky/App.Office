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
    /// 转化为图片
    /// </summary>
    public interface IImager
    {
        /// <summary>文件过滤器。如 doc|*.doc</summary>
        string Filter { get; }

        /// <summary>处理</summary>
        List<string> Process(string filePath, string saveFolder);
    }
}
