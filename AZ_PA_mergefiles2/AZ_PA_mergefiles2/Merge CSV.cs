using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using static AZ_PA_mergefiles2.Model;

namespace AZ_PA_MergeFiles
{
    public static class Merge_CSV
    {
        [FunctionName("Merge_CSV")]
        [OpenApiOperation(operationId: "Merge", tags: new[] { "CSV" })]
       // [OpenApiParameter(name: "file", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The **Files Content** parameter")]
        [OpenApiRequestBody("application/json", typeof(RequestBodyModel), Required = true, Description = "The **Files Content** parameter")]

        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "String", bodyType: typeof(ResponseBodyModel), Description = "Bytes array of merge CSV")]

        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                dynamic input = JsonConvert.DeserializeObject(requestBody);
                dynamic files = input.file;

                //  byte[] cont = cnt["$content"];


                bool ext = true;
                foreach (var file in files)
                {
                    if (file["$content-type"] != "text/csv")
                    {
                        ext = false;
                    }
                }

                if (ext)
                {
                    byte[][] bytesarray = new byte[files.Count][];
                    int j = 0;
                    foreach (var file in files)
                    {
                        bytesarray[j] = file["$content"];
                        j++;
                    }

                    byte[] rv = new byte[bytesarray.Sum(a => a.Length)];
                    int offset = 0;
                    foreach (byte[] array in bytesarray)
                    {
                        System.Buffer.BlockCopy(array, 0, rv, offset, array.Length);
                        offset += array.Length;
                    }

                    if (rv != null)
                    {
                        //System.IO.File.WriteAllBytes(@"C:\Users\Sahil\Desktop\hello.csv", rv);
                        return new OkObjectResult(rv);
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
