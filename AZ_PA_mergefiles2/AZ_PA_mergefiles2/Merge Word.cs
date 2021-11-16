using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;

using OpenXmlPowerTools;
using Source = OpenXmlPowerTools.Source;
using System.Net;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using static AZ_PA_mergefiles2.Model;

namespace AZ_PA_MergeFiles
{
    public static class Merge_Word
    {
        [FunctionName("Merge_Word")]
        [OpenApiOperation(operationId: "Merge", tags: new[] { "WORD" })]
        // [OpenApiParameter(name: "file", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The **Files Content** parameter")]
        [OpenApiRequestBody("application/json", typeof(RequestBodyModel), Required = true, Description = "The **Files Content** parameter")]

        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "Byte Array", bodyType: typeof(ResponseBodyModel), Description = "Bytes array of merge WORD")]
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
                    if (file["$content-type"] != "application/vnd.openxmlformats-officedocument.wordprocessingml.document")
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

                    List<Source> documentBuilderSources = new List<Source>();
                    foreach (byte[] documentByteArray in bytesarray)
                    {
                        documentBuilderSources.Add(new Source(new WmlDocument(string.Empty, documentByteArray), false));
                    }

                    WmlDocument mergedDocument = DocumentBuilder.BuildDocument(documentBuilderSources);
                    var st = mergedDocument.DocumentByteArray;

                    if (st != null)
                    {

                        return new OkObjectResult(st);
                    }
                    else
                    {
                        return new OkObjectResult("fail");
                    }
                }
                
                return new OkObjectResult("Unsupported File Format");
                      
            }
            catch (Exception ex)
            {
                return new OkObjectResult(ex.Message);
            }
        }

       
    }
}
