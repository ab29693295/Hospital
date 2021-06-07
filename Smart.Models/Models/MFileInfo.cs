using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Models
{
    [Serializable]
    /// <summary>
    /// 文件描述类
    /// </summary>
    public class MFileInfo
    {
        /// <summary>
        /// 上传名称名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 后缀
        /// </summary>
        public string extraName { get; set; }
        /// <summary>
        /// 文件大小
        /// </summary>
        public int Size { get; set; }
    }
}
