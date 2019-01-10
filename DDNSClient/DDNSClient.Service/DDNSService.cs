using Aliyun.Acs.Alidns.Model.V20150109;
using DDNSClient.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DDNSClient.Service
{
    partial class DDNSService : ServiceBase
    {
        public DDNSService()
        {
            InitializeComponent();
        }

        private Thread m_Thread = null;
        private bool m_IsStopped = false;

        protected override void OnStart(string[] args)
        {
            // TODO: 在此处添加代码以启动服务。
            m_Thread = new Thread(() =>
            {
                //重试的次数
                int times = 1;
                while (true)
                {
                    if (m_IsStopped)
                    { break; }
                    try
                    {
                        Thread.Sleep(1000 * 10);//方便调试，延迟10秒
                        string ip = GetNowIP();
                        if (string.IsNullOrEmpty(ip))
                        {
                            //没有获取到 IP 时 1分钟后重试。
                            Thread.Sleep(1000 * 60 * times);
                            times++;
                            if (times > 54)
                            {
                                //超过一天仍然没有获取到 IP 则重置次数。
                                times = 1;
                            }
                            continue;
                        }
                        //获取到 IP 后将次数重置
                        times = 1;
                        DescribeDomainRecordsResponse.Record[] records = GetRecords("A", "home");
                        if (records != null && records.Length > 0)
                        {
                            for(int i =0; i< records.Length; i++)
                            {
                                if (records[i].Value != ip)
                                {
                                    CLogHelper.WriteInfo("当前 IP:" + ip + " 域名解析 IP:" + records[i].Value + " 开始修改解析记录。 ");
                                    records[i].Value = ip;
                                    string strContent;
                                    bool bResult = CDomainHelper.UpdateDomainRecords(records[i], out strContent);
                                    if (bResult)
                                    {
                                        CLogHelper.WriteInfo("修改解析成功，将" + records[i].RR + "." + records[i].DomainName + " 解析到：" + records[i].Value);
                                    }
                                    else
                                    {
                                        CLogHelper.WriteInfo("修改解析失败：" + strContent);
                                    }
                                }
                                else
                                {
                                    CLogHelper.WriteInfo("当前 IP 与域名解析 IP 相同，不需要修改解析。");
                                }
                            }
                        }
                        else
                        {
                            CLogHelper.WriteInfo("没有找到解析需要新增解析记录。");
                            //record = new DescribeDomainRecordsResponse.Record();
                            //record.RR = "dev";
                            //record.DomainName = CGlobalConfig.DomainName;
                            //record.Type = "A";
                            //record.Value = ip;
                            //string strContent;
                            //bool bResult = CDomainHelper.AddDomainRecords(record, out strContent);
                            //if (bResult)
                            //{
                            //    CLogHelper.WriteInfo("新增解析成功，将" + record.RR + "." + record.DomainName + " 解析到：" + record.Value);
                            //}
                            //else
                            //{
                            //    CLogHelper.WriteInfo("新增解析失败：" + strContent);
                            //}
                        }

                        //睡眠 3 分钟后再次解析
                        Thread.Sleep(1000 * 60 * 3);
                        //Thread.Sleep(1000 * 20);
                    }
                    catch (ThreadInterruptedException)
                    {
                        break;
                    }
                    catch (Exception ex)
                    {
                        this.EventLog.WriteEntry("服务在运行过程中发送异常：" + ex.Message);
                        CLogHelper.WriteError(ex);
                    }
                }
                this.EventLog.WriteEntry("线程已经跳出，服务已经停止。");
                CLogHelper.WriteInfo("线程已经跳出，服务已经停止。");
            });
            m_Thread.IsBackground = true;
            m_Thread.Start();
        }

        private DescribeDomainRecordsResponse.Record GetRecord(string strType, string strRR)
        {
            List<DescribeDomainRecordsResponse.Record> list = CDomainHelper.GetDomainRecords(CGlobalConfig.DomainName);
            return list.Find(rec => rec.Type == strType && rec.RR == strRR);
        }

        private DescribeDomainRecordsResponse.Record[] GetRecords(string strType, string strRR)
        {
            List<DescribeDomainRecordsResponse.Record> list = CDomainHelper.GetDomainRecords(CGlobalConfig.DomainName);
            for(int i = list.Count - 1; i >= 0; i--)
            {
                if(list[i].Type != strType || list[i].RR != strRR)
                {
                    list.RemoveAt(i);
                }
            }
            return list.ToArray();
        }


        private string GetNowIP()
        {
            string ip = "";
            try
            {
                HttpWebRequest request = HttpWebRequest.Create(CGlobalConfig.IPUrl) as HttpWebRequest;
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("GB2312"));
                    string str = reader.ReadToEnd();
                    reader.Close();
                    reader.Dispose();

                    int start = str.IndexOf(":");
                    str = str.Substring(start + 1);
                    start = str.IndexOf("\"");
                    if (start > -1)
                    {
                        int length = str.Substring(start + 1).IndexOf("\"");
                        ip = str.Substring(start + 1, length);
                        CLogHelper.WriteInfo("当前 IP 地址：" + ip);
                        //int start1 = str.IndexOf("来自：");
                        //int end1 = str.IndexOf("</center></body></html>");
                        //if (start1 > -1 && end1 > -1)
                        //{
                        //    string from = str.Substring(start1, end - start);
                        //    CLogHelper.WriteInfo(from);
                        //}
                    }
                    else
                    {
                        CLogHelper.WriteInfo(str);
                    }
                }
            }
            catch (Exception ex)
            {
                CLogHelper.WriteError(" GetNowIP 发送异常：" + ex.ToString());
            }
            return ip;
        }

        protected override void OnStop()
        {
            // TODO: 在此处添加代码以执行停止服务所需的关闭操作。
            try
            {
                m_IsStopped = true;
                m_Thread.Interrupt();
            }
            catch (Exception ex)
            {
                this.EventLog.WriteEntry("服务在停止时发送异常：" + ex.Message);
                CLogHelper.WriteError("服务在停止时发送异常：" + ex.ToString());
            }
        }
    }
}
