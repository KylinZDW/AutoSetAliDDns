using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DDNSClient.Common
{
    /// <summary>
    /// 日志标记
    /// </summary>
    public enum ELoggerTags
    {
        /// <summary>
        /// 消息
        /// </summary>
        Info = 0,
        /// <summary>
        /// 成功
        /// </summary>
        Success,
        /// <summary>
        /// 警告
        /// </summary>
        Warn,
        /// <summary>
        /// 错误
        /// </summary>
        Error,
        /// <summary>
        /// 调试信息
        /// </summary>
        Debug
    }
}
