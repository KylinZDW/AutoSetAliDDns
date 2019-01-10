using Aliyun.Acs.Alidns.Model.V20150109;
using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Exceptions;
using Aliyun.Acs.Core.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDNSClient.Common
{
    public class CDomainHelper
    {
        /// <summary>
        /// 根据域名获取解析记录
        /// </summary>
        /// <param name="domain">域名</param>
        /// <returns></returns>
        public static List<DescribeDomainRecordsResponse.Record> GetDomainRecords(string domain)
        {
            try
            {
                IClientProfile clientProfile = DefaultProfile.GetProfile("cn-hangzhou", CGlobalConfig.AccessKey, CGlobalConfig.AccessKeySecret);
                DefaultAcsClient client = new DefaultAcsClient(clientProfile);
                DescribeDomainRecordsRequest request = new DescribeDomainRecordsRequest();
                request.DomainName = domain;
                try
                {
                    DescribeDomainRecordsResponse response = client.GetAcsResponse(request);
                    List<DescribeDomainRecordsResponse.Record> list = response.DomainRecords;
                    return list;
                }
                catch (ServerException e)
                {
                    CLogHelper.WriteError(" GetDomainRecords 发送异常：" + e.ErrorCode + "\t" + e.ErrorMessage);
                }
                catch (ClientException e)
                {
                    CLogHelper.WriteError(" GetDomainRecords 发送异常：" + e.ErrorCode + "\t" + e.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                CLogHelper.WriteError(" GetDomainRecords 发送异常：" + ex.ToString());
            }
            return new List<DescribeDomainRecordsResponse.Record>();
        }

        /// <summary>
        /// 新增解析记录
        /// </summary>
        /// <param name="record"></param>
        /// <param name="strContent"></param>
        /// <returns></returns>
        public static bool AddDomainRecords(DescribeDomainRecordsResponse.Record record, out string strContent)
        {
            try
            {
                IClientProfile clientProfile = DefaultProfile.GetProfile("cn-hangzhou", CGlobalConfig.AccessKey, CGlobalConfig.AccessKeySecret);
                DefaultAcsClient client = new DefaultAcsClient(clientProfile);
                AddDomainRecordRequest request = new AddDomainRecordRequest();
                request.DomainName = record.DomainName;
                request.RR = record.RR;
                request.Type = record.Type;
                request.Value = record.Value;
                try
                {
                    AddDomainRecordResponse response = client.GetAcsResponse(request);
                    strContent = response.RecordId;
                    return !string.IsNullOrEmpty(strContent);
                }
                catch (ServerException e)
                {
                    strContent = " AddDomainRecords 发送异常：" + e.ErrorCode + "\t" + e.ErrorMessage;
                    CLogHelper.WriteError(strContent);
                }
                catch (ClientException e)
                {
                    strContent = " AddDomainRecords 发送异常：" + e.ErrorCode + "\t" + e.ErrorMessage;
                    CLogHelper.WriteError(strContent);
                }
            }
            catch (Exception ex)
            {
                strContent = " AddDomainRecords 发送异常：" + ex.ToString();
                CLogHelper.WriteError(strContent);
            }
            return false;
        }

        /// <summary>
        /// 修改解析记录
        /// </summary>
        /// <param name="record"></param>
        /// <param name="strContent"></param>
        /// <returns></returns>
        public static bool UpdateDomainRecords(DescribeDomainRecordsResponse.Record record, out string strContent)
        {
            try
            {
                IClientProfile clientProfile = DefaultProfile.GetProfile("cn-hangzhou", CGlobalConfig.AccessKey, CGlobalConfig.AccessKeySecret);
                DefaultAcsClient client = new DefaultAcsClient(clientProfile);
                UpdateDomainRecordRequest request = new UpdateDomainRecordRequest();
                request.RecordId = record.RecordId;
                request.RR = record.RR;
                request.Type = record.Type;
                request.Value = record.Value;
                request.Line = record.Line;
                try
                {
                    UpdateDomainRecordResponse response = client.GetAcsResponse(request);
                    strContent = response.RecordId;
                    return !string.IsNullOrEmpty(strContent);
                }
                catch (ServerException e)
                {
                    strContent = " UpdateDomainRecords 发送异常：" + e.ErrorCode + "\t" + e.ErrorMessage;
                    CLogHelper.WriteError(strContent);
                }
                catch (ClientException e)
                {
                    strContent = " UpdateDomainRecords 发送异常：" + e.ErrorCode + "\t" + e.ErrorMessage;
                    CLogHelper.WriteError(strContent);
                }
            }
            catch (Exception ex)
            {
                strContent = " UpdateDomainRecords 发送异常：" + ex.ToString();
                CLogHelper.WriteError(strContent);
            }
            return false;
        }
    }
}
