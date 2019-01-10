using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDNSClient.Common
{
    public class CLogHelper
    {
        private static Encoding encoding = Encoding.GetEncoding("GB2312");
        public static void WriteError(Exception ex)
        {
            string file = AppDomain.CurrentDomain.BaseDirectory + @"logs\" + DateTime.Now.ToString("yyyy-MM") + @"\" + DateTime.Now.ToString("yyyy-MM-dd") + ".log";
            CTextLogger loger = new CTextLogger(file, encoding);
            loger.WriteLog(ELoggerTags.Error, ex.ToString());
        }

        public static void WriteError(string strContent)
        {
            string file = AppDomain.CurrentDomain.BaseDirectory + @"logs\" + DateTime.Now.ToString("yyyy-MM") + @"\" + DateTime.Now.ToString("yyyy-MM-dd") + ".log";
            CTextLogger loger = new CTextLogger(file, encoding);
            loger.WriteLog(ELoggerTags.Error, strContent);
        }

        public static void WriteInfo(string strContent)
        {
            string file = AppDomain.CurrentDomain.BaseDirectory + @"logs\" + DateTime.Now.ToString("yyyy-MM") + @"\" + DateTime.Now.ToString("yyyy-MM-dd") + ".log";
            CTextLogger loger = new CTextLogger(file, encoding);
            loger.WriteLog(ELoggerTags.Info, strContent);
        }
        public static void WriteSuccess(string strContent)
        {
            string file = AppDomain.CurrentDomain.BaseDirectory + @"logs\" + DateTime.Now.ToString("yyyy-MM") + @"\" + DateTime.Now.ToString("yyyy-MM-dd") + ".log";
            CTextLogger loger = new CTextLogger(file, encoding);
            loger.WriteLog(ELoggerTags.Success, strContent);
        }
    }
}
