using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using DocumentFormat.OpenXml.Spreadsheet;
using Aspose.Cells.Utility;
using System.Data;
using System.Text;
using System.ComponentModel.DataAnnotations;
using OfficeOpenXml;
using System.Collections.Generic;

namespace AzureTrigger
{
    public static class Function1
    {
        [FunctionName("EmployeeAdded")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "EmployeeAdded")] HttpRequest req,
            ILogger log)
        {

            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                dynamic input = JsonConvert.DeserializeObject(requestBody);
                                                
                var sb = new StringBuilder();

                foreach (var v in input)
                {
                    //dynamic row = JsonConvert.DeserializeObject<Employee>(v);
                    sb.AppendLine($"{v.emp_id} , {v.emp_name} , {v.emp_salary} , {v.emp_manager_id} , {v.dept_id}");
                }

               // File.WriteAllText(@"https://unknown1234-my.sharepoint.com/:x:/g/personal/shubham_unknown1234_onmicrosoft_com/ET98yOrWlMJJgQwRy_DL3EEBF1wA2iCQ9HX0xpXRmaQzWw?e=7xT84T", sb.ToString());
                return new OkObjectResult(sb.ToString());                

            }
            catch(Exception ex)
            {
                return new OkObjectResult(ex);
            }


            using (var ew = new ExcelWriter("C:\\temp\\test.xlsx"))
            {
                for (var row = 1; row <= 10; row++)
                {
                    for (var col = 1; col <= 5; col++)
                    {
                        ew.Write($"row:{row}-col:{col}", col, row);
                    }
                }
            }

        }


        
    }
}
