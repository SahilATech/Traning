using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
/*
 string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                dynamic data = JsonConvert.DeserializeObject(requestBody);
 */
namespace Az_Custom_Connector
{
    public static class Function1
    {
        [FunctionName("add")]
        public static async Task<IActionResult> adddata([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                dynamic values = JsonConvert.DeserializeObject(requestBody);
                
                var sum = 0;
                foreach (var v in values)
                {
                    sum += Convert.ToInt32(v.Value);
                }
                return new OkObjectResult(sum);
            }
            catch (Exception ex)
            {

                return new OkObjectResult(ex.Message);
            }
              
        }
        
        [FunctionName("sub")]
        public static async Task<IActionResult> subdata([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req)
        {
            try
            {

                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                dynamic values = JsonConvert.DeserializeObject(requestBody);
                var sub = 0;
                var st = 0;
                foreach (var v in values)
                {
                    st = Convert.ToInt32(v.Value);
                    sub = st - sub;
                }
                return new OkObjectResult(sub);
            }
            catch (Exception ex)
            {

                return new OkObjectResult(ex.Message);
            }

        }

        [FunctionName("multiple")]
        public static async Task<IActionResult> mulltipledata([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                dynamic values = JsonConvert.DeserializeObject(requestBody);
                var mlt = 1;
                foreach (var v in values)
                {
                    mlt *= Convert.ToInt32(v.Value);
                }
                return new OkObjectResult(mlt);
            }
            catch (Exception ex)
            {

                return new OkObjectResult(ex.Message);
            }

        }

        [FunctionName("div")]
        public static async Task<IActionResult> divdata([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                dynamic values = JsonConvert.DeserializeObject(requestBody);
                var val = 0;
                var rlt = 1;

                foreach(var v in values)
                {
                    val = Convert.ToInt32(v.Value);
                    rlt = val / rlt;
                }
               
                
                return new OkObjectResult(rlt);
            }
            catch (Exception ex)
            {

                return new OkObjectResult(ex.Message);
            }

        }
    }
}
