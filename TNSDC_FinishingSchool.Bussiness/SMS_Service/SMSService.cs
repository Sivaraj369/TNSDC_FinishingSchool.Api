using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TNSDC_FinishingSchool.Bussiness.Common;

namespace TNSDC_FinishingSchool.Bussiness.SMS_Service
{
    public class SMSService
    {
        private readonly IConfiguration _config;
        private readonly APIRequestHandler _requestHandler;

        public SMSService(IConfiguration config, APIRequestHandler requestHandler)
        {
            _config = config;
            _requestHandler = requestHandler;
        }
        public async Task<APIResponse> SendSms(string sms_Content, string template, long mobileNo, Dictionary<string, string> smsBodyReplace = default)
        {
            try
            {
                string username = _config.GetSection("SMS_Service")["Username"];
                string password = _config.GetSection("SMS_Service")["Password"];
                string token = _config.GetSection("SMS_Service")["Token"];

                string templateId = _config.GetSection("SMS_Service:SMS_Template")[template];
                string contentPath = Path.Combine(_config.GetSection("SMS_Service:SMS_Template")[sms_Content]);
                string smsBody = File.ReadAllText(contentPath);

                //smsBody = smsBodyReplace.Aggregate(smsBody, (currentUrl, pair) => currentUrl.Replace(pair.Key, pair.Value));

                //smsBodyReplace.Select(x => smsBody = smsBody.Replace(x.Key, x.Value));

                Dictionary<string, string> urlReplaceDictionary = new Dictionary<string, string>()
                {
                    {"@@username",username },
                    {"@@password",password},
                    {"@@token",token},
                    {"@@templateId", templateId },
                    {"@@smsBody", smsBody },
                    {"@@mobileNo",mobileNo.ToString() },

                };
                if (smsBodyReplace != null)
                    urlReplaceDictionary = urlReplaceDictionary.Union(smsBodyReplace).ToDictionary(x => x.Key, x => x.Value);

                string apiUrl = _config.GetSection("SMS_Service")["apiUrl"];
                apiUrl = urlReplaceDictionary.Aggregate(apiUrl, (current, pair) => current.Replace(pair.Key, pair.Value));

                var resp = await _requestHandler.CallApiAsync(apiUrl, HttpMethod.Get);
                return resp;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
