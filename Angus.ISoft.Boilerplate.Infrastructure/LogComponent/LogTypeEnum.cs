using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angus.ISoft.Boilerplate.Infrastructure.Log
{
    public enum LogTypeEnum
    {
        /// <summary>
        /// 调试信息
        /// </summary>
        Debug = 1,
        /// <summary>
        /// 输出信息
        /// </summary>
        Info = 2,
        /// <summary>
        /// 警告信息
        /// </summary>
        Warn = 3,
        /// <summary>
        /// 错误信息
        /// </summary>
        Error = 4,
        /// <summary>
        /// 严重错误
        /// </summary>
        Fatal = 5
    }

    public enum LogWriteDestinationEnum
    {
        /// <summary>
        /// 写入文件
        /// </summary>
        WriteToFile = 1,
        /// <summary>
        /// 写入数据库
        /// </summary>
        WriteToDb = 2
    }
}
