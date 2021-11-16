using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Spire.Xls;
using System.Data;
using CsvHelper.Configuration;
using System.Globalization;
using CsvHelper;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Text;
using OfficeOpenXml;
using ClosedXML.Excel;

namespace AZ_PA_MergeFiles
{
    public static class merge_Excel
    {
        [FunctionName("merge_Excel")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req)
        {
            try
            {

                bool ext = true;
                foreach (var v in req.Form.Files)
                {
                    if (v.ContentType != "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        ext = false;
                    }
                }

                if (ext)
                {
                    IFormFileCollection files = req.Form.Files;
                    byte[][] bytesarray = filebytes.convertbyte(files);

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

