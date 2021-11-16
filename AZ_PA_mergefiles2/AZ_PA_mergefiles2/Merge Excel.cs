using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using System.Net;
using Microsoft.OpenApi.Models;
using static AZ_PA_mergefiles2.Model;
using System.Data;
using ClosedXML.Excel;
using System.Linq;

namespace AZ_PA_MergeFiles
{
    public static class merge_Excel
    {
        [FunctionName("merge_Excel")]
        [OpenApiOperation(operationId: "Merge", tags: new[] { "Excel" })]
        // [OpenApiParameter(name: "file", In = ParameterLocation.Query, Required = true, Type = typeof(Object), Description = "The **Files Content** parameter")]
        [OpenApiRequestBody("application/json", typeof(RequestBodyModel), Required = true, Description = "The **Files Content** parameter")]

        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/pdf", bodyType: typeof(ResponseBodyModel), Description = "TBytes array of merge Excel")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req)
        {
                try
                {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                dynamic input = JsonConvert.DeserializeObject(requestBody);
                dynamic files = input.file;
                bool ext = true;
                    foreach (var file in files)
                    {
                        if (files.ContentType != "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
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

                    DataSet ds = new DataSet();
                        foreach (var postedFile in bytesarray)
                        {

                            MemoryStream ms = new MemoryStream(postedFile);
                            var stream = new StreamReader(ms);

                            using (XLWorkbook workBook = new XLWorkbook(ms))
                            {
                                IXLWorksheet workSheet = workBook.Worksheet(1);


                                DataTable dt = new DataTable();
                                bool firstRow = true;
                                foreach (IXLRow row in workSheet.Rows())
                                {
                                    if (firstRow)
                                    {
                                        foreach (IXLCell cell in row.Cells())
                                        {
                                            dt.Columns.Add(cell.Value.ToString());
                                        }
                                        firstRow = false;
                                    }
                                    else
                                    {
                                        if (row.Cells().Count() > 1)
                                            dt.Rows.Add();
                                        int i = 0;
                                        foreach (IXLCell cell in row.Cells())
                                        {
                                            dt.Rows[dt.Rows.Count - 1][i] = cell.Value.ToString();
                                            i++;
                                        }
                                    }
                                }
                                firstRow = true;

                                ds.Tables.Add(dt);
                            }
                        }

                        DataTable dtMerge = ds.Tables[0].Clone();
                        foreach (DataTable table in ds.Tables)
                        {
                            dtMerge.Merge(table);
                        }

                        using (XLWorkbook wb = new XLWorkbook())
                        {
                            wb.Worksheets.Add(dtMerge);
                            using (MemoryStream stream = new MemoryStream())
                            {
                                wb.SaveAs(stream);
                                System.IO.File.WriteAllBytes(@"C:\Users\Sahil\Desktop\hello.xlsx", stream.ToArray());
                            }
                        }
                    }

                    return null;
                }
                catch (Exception ex)
                {
                    return new OkObjectResult(ex.Message);
                }
            
        }
        
    }
}
