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

namespace AZ_PA_MergeFiles
{
    public static class Merge_Word
    {
        [FunctionName("Merge_Word")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req)
        {
            try
            {
                bool ext = true;
                foreach (var v in req.Form.Files)
                {
                    if (v.ContentType != "application/vnd.openxmlformats-officedocument.wordprocessingml.document")
                    {
                        ext = false;
                    }
                }
                if (ext)
                {
                    IFormFileCollection files = req.Form.Files;
                    byte[][] bytesarray = filebytes.convertbyte(files);

                    List<Source> documentBuilderSources = new List<Source>();
                    foreach (byte[] documentByteArray in bytesarray)
                    {
                        documentBuilderSources.Add(new Source(new WmlDocument(string.Empty, documentByteArray), false));
                    }

                    WmlDocument mergedDocument = DocumentBuilder.BuildDocument(documentBuilderSources);
                    var st = mergedDocument.DocumentByteArray;

                    if (st != null)
                    {

                        System.IO.File.WriteAllBytes(@"C:\Users\Sahil\Desktop\hello.docx", st);
                        return new OkObjectResult("done");
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
