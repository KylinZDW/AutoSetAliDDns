using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDNSClient
{
    public class CGlobalConfig
    {
        public static string AccessKey
        {
            get
            {
                return ConfigurationManager.AppSettings["AccessKey"];
            }
        }
        public static string APIUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["APIUrl"];
            }
        }
        public static string AccessKeySecret
        {
            get
            {
                return ConfigurationManager.AppSettings["AccessKeySecret"];
            }
        }
        public static string IPUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["IPUrl"];
            }
        }
        public static string DomainName
        {
            get
            {
                return ConfigurationManager.AppSettings["DomainName"];
            }
        }
    }
}
