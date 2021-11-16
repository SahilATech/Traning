using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Linq;

namespace AZ_PA_MergeFiles
{
    public static class Merge_CSV
    {
        [FunctionName("Merge_CSV")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req)
        {
            try
            {
                bool ext = true;
                foreach(var v in req.Form.Files)
                {
                    if(v.ContentType != "text/csv")
                    {
                        ext = false;
                    }
                }
            
                if (ext)
                {
                    IFormFileCollection files = req.Form.Files;
                    byte[][] bytesarray = filebytes.convertbyte(files);

                    byte[] rv = new byte[bytesarray.Sum(a => a.Length)];
                    int offset = 0;
                    foreach (byte[] array in bytesarray)
                    {
                        System.Buffer.BlockCopy(array, 0, rv, offset, array.Length);
                        offset += array.Length;
                    }

                    if (rv != null)
                    {
                        System.IO.File.WriteAllBytes(@"C:\Users\Sahil\Desktop\hello.csv", rv);
                        return new OkObjectResult("done");
                    }
                    else
                    {
                        return new OkObjectResult("error");
                    }

                }
                return new OkObjectResult("unsupported file formate");
            }
            catch (Exception ex)
            {

                return new OkObjectResult(ex.Message);
            }
        }
        
    }
}
