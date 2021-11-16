using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using iTextSharp.text.pdf;
using System.Net;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using static AZ_PA_mergefiles2.Model;

namespace AZ_PA_mergefiles2
{
    public static class Merge_pdf
    {
        [FunctionName("mergepdf")]
        [OpenApiOperation(operationId: "Merge", tags: new[] { "PDF" })]
       // [OpenApiParameter(name: "file", Required = true, Type = typeof(Object), Description = "The **Files Content** parameter")]
        [OpenApiRequestBody("application/json", typeof(RequestBodyModel), Required = true, Description = "The **Files Content** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "Byte Array", bodyType: typeof(ResponseBodyModel), Description = "Bytes array of merge PDF")]
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
                    if (file["$content-type"] != "application/pdf")
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

                    using (var ms = new MemoryStream())
                    {
                        var outputDocument = new iTextSharp.text.Document();
                        var writer = new PdfCopy(outputDocument, ms);
                        outputDocument.Open();

                        foreach (var file in bytesarray)
                        {
                            var reader = new PdfReader(file);
                            for (var i = 1; i <= reader.NumberOfPages; i++)
                            {
                                writer.AddPage(writer.GetImportedPage(reader, i));
                            }
                            writer.FreeReader(reader);
                            reader.Close();
                        }

                        writer.Close();
                        outputDocument.Close();
                        var allPagesContent = ms.GetBuffer();
                        ms.Flush();

                        if (allPagesContent != null)
                        {
                           // System.IO.File.WriteAllBytes(@"C:\Users\Sahil\Desktop\hello.pdf", allPagesContent);
                            return new OkObjectResult(allPagesContent);
                        }
                        else
                        {
                            return new OkObjectResult("fail");
                        }
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
