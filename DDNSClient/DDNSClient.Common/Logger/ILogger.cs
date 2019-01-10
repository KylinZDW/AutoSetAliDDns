using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DDNSClient.Common
{
    /// <summary>
    /// 日志记录接口
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="eTag">日志标记</param>
        /// <param name="strContent">日志内容</param>
        void WriteLog(ELoggerTags eTag, string strContent);
    }
}
