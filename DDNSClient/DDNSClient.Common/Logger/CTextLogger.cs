using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace DDNSClient.Common
{
    /// <summary>
    /// 日志记录器
    /// </summary>
    public class CTextLogger
    {
        private string m_FilePath = "";

        /// <summary>
        /// 获取或设置日志记录器文件的路径
        /// </summary>
        public string FilePath
        {
            get { return m_FilePath; }
            set { m_FilePath = value; }
        }
        private Encoding m_Encoding = null;

        /// <summary>
        /// 实例化日志记录器
        /// </summary>
        /// <param name="strFilePath">文件名</param>
        public CTextLogger(string strFilePath, Encoding encoding)
        {
            m_FilePath = strFilePath;
            m_Encoding = encoding;
            string path = Path.GetDirectoryName(m_FilePath);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public void WriteLog(ELoggerTags eTag, string strContent)
        {
            string content = GetString(eTag, strContent);
            WriteLine(content, m_FilePath, true, m_Encoding);
        }


        private string GetString(ELoggerTags eTag, string strContent)
        {
            string tag = GetTagText(eTag);
            string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            int id = Thread.CurrentThread.ManagedThreadId;
            return "[" + id + "]\t" + time + "\t" + tag + "\t" + strContent;
        }

        private string GetTagText(ELoggerTags eTag)
        {
            switch (eTag)
            {
                case ELoggerTags.Error: return "ERROR";
                case ELoggerTags.Success: return "SUCCESS";
                case ELoggerTags.Warn: return "WARN";
                case ELoggerTags.Debug: return "DEBUG";
                default: return "INFO";
            }
        }

        private void WriteLine(string strText, string strFilePath, bool bIsAppend, Encoding encoding)
        {
            StreamWriter writer = null;
            try
            {
                string path = Path.GetDirectoryName(strFilePath);
                if (!Directory.Exists(path))
                {
                    try
                    {
                        Directory.CreateDirectory(path);
                    }
                    catch
                    {
                    }
                }
                writer = new StreamWriter(strFilePath, bIsAppend, encoding);
                writer.WriteLine(strText);
            }
            catch
            {
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                    writer.Dispose();
                }
            }
        }
    }
}
