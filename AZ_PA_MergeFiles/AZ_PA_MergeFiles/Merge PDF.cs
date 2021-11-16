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

namespace AZ_PA_MergeFiles
{
   
    public static class Merge_PDF
    {
        [FunctionName("Merge_PDF")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req)
        {
            try
            {
                //string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                //dynamic input = JsonConvert.DeserializeObject(requestBody);
                //dynamic cnt = input.body;
                //byte[] cont = cnt["$content"];
                bool ext = true;
                foreach (var v in req.Form.Files)
                {
                    if (v.ContentType != "application/pdf")
                    {
                        ext = false;
                    }
                }

                if (ext)
                {
                    IFormFileCollection files = req.Form.Files;
                    byte[][] bytesarray = filebytes.convertbyte(files);


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
                            
                            System.IO.File.WriteAllBytes(@"C:\Users\Sahil\Desktop\hello.pdf", allPagesContent);
                            return new OkObjectResult("done");
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

        public class response
        {

        }
    }
}
