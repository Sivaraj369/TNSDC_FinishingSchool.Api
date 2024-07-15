using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace TNSDC_FinishingSchool.Bussiness.Common
{
    public class APIRequestHandler
    {
        public async Task<APIResponse> CallApiAsync(string url, HttpMethod method, string? body = default, Dictionary<string, string>? header = default, string? authType = null, Dictionary<string, string>? queryParams = null, bool IsSoapRequest = false)
        {
            APIResponse apiresponse = new APIResponse();

            try
            {
                //ssl certificate issue
                var httpClientHandler = new HttpClientHandler();
                httpClientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

                using HttpClient _client = new HttpClient(httpClientHandler);
                //using HttpClient _client = new HttpClient();
                _client.Timeout = TimeSpan.FromMinutes(40);

                var request = new HttpRequestMessage
                {
                    Method = method
                };
                if (queryParams != null && queryParams.Count > 0)
                {
                    var uriBuilder = new UriBuilder(url);
                    var query = HttpUtility.ParseQueryString(uriBuilder.Query);
                    foreach (var kvp in queryParams)
                    {
                        query[kvp.Key] = kvp.Value;
                    }
                    uriBuilder.Query = query.ToString();
                    request.RequestUri = uriBuilder.Uri;
                }
                else
                    request.RequestUri = new Uri(url);

                if (header != null)
                {
                    foreach (var kvp in header)
                    {
                        request.Headers.Add(kvp.Key, kvp.Value);
                    }
                }

                if (method != HttpMethod.Get && !string.IsNullOrEmpty(body) && !IsSoapRequest)
                {
                    request.Content = new StringContent(body, Encoding.UTF8, "application/json");
                }

                if (method != HttpMethod.Get && !string.IsNullOrEmpty(body) && IsSoapRequest)
                {
                    request.Content = new StringContent(body, Encoding.UTF8, "application/soap+xml");
                }

                var response = await _client.SendAsync(request);

                apiresponse.StatusCode = response.StatusCode;
                if (response.IsSuccessStatusCode)
                {
                    apiresponse.DisplayMessage = APIErrorCodeMessages.SUCCESS;
                    // Read the content as a string
                    string responseContent = await response.Content.ReadAsStringAsync();
                    apiresponse.Result = responseContent;
                    return apiresponse;
                }
                else
                {
                    apiresponse.DisplayMessage = APIErrorCodeMessages.FAILURE;
                    //log.ApiStatus = apiresponse.DisplayMessage;

                    if (response?.Content != null)
                    {
                        apiresponse.Errors = [ new APIError(response.ReasonPhrase)];
                        apiresponse.Result = await response.Content.ReadAsStringAsync();
                        //log.Response = apiresponse.Result;
                        return apiresponse;
                    }
                    apiresponse.Errors = [new("something went wrong in API call")];
                    //log.Response = apiresponse.Response;
                    return apiresponse;
                }

            }
            catch (Exception ex)
            {
                //log.ApiStatus = ApiErrorCodeMessages.FAILURE;
                //log.Response = ex.Message;
                throw;
            }
            
        }
    }
}
