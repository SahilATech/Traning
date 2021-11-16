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
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net;
using System.Text;

namespace FunctionApp3
{
    public static class Program
    {

        [FunctionName("GetEmployees")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "GET", Route = null)] HttpRequest req, ILogger log)
        {
            List<Employee> employeelist = new List<Employee>();
            try
            {
                using (SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable("SqlConnectionString")))
                {
                    connection.Open();
                    var query = @"Select * from employee";
                    SqlCommand command = new SqlCommand(query, connection);
                    var reader = await command.ExecuteReaderAsync();
                    while (reader.Read())
                    {
                        Employee task = new Employee()
                        {
                            emp_id = (string)reader["emp_id"],
                            emp_name = (string)reader["emp_name"],
                            emp_salary = (Decimal)reader["emp_salary"],
                            emp_manager_id = (string)reader["emp_manager_id"].ToString(),
                            dept_id = (int)reader["dept_id"]
                        };
                        employeelist.Add(task);
                    }
                }
            }
            catch (Exception e)
            {
                log.LogError(e.ToString());
            }
            if (employeelist.Count > 0)
            {
                return new OkObjectResult(employeelist);
            }
            else
            {
                return new NotFoundResult();
            }
        }

        [FunctionName("GetEmployeeById")]
        public static IActionResult GetTaskById(
        [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "GetEmployeeById/{id}")] HttpRequest req, ILogger log, string id)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable("SqlConnectionString")))
                {
                    connection.Open();
                    var query = @"Select * from employee Where emp_id = @Id";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", id);
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    da.Fill(dt);
                }
            }
            catch (Exception e)
            {
                log.LogError(e.ToString());
            }
            if (dt.Rows.Count == 0)
            {
                return new NotFoundResult();
            }
            return new OkObjectResult(dt);
        }

        [FunctionName("AddEmployee")]
        public static async Task<IActionResult> AddEmployee(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "AddEmployee")] HttpRequest req, ILogger log)
        {

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            //var input = JsonConvert.DeserializeObject<Employee>(requestBody);
            dynamic input = JsonConvert.DeserializeObject(requestBody);
            try
            {
                using (SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable("SqlConnectionString")))
                {
                    connection.Open();
                    foreach (var v in input )
                    {
                        var query = $"INSERT INTO employee (emp_id,emp_name,emp_salary,emp_manager_id,dept_id) VALUES('{v.emp_id}', '{v.emp_name}' , '{v.emp_salary}', '{v.emp_manager_id}', '{v.dept_id}')";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.ExecuteNonQuery();
                    }
            }

                var response = await triggerAzure(input);
                
                    return new OkObjectResult(response);
                
               
            }
            catch (Exception e)
            {
                log.LogError(e.ToString());
                return new OkObjectResult(e);
            }


             async Task<IActionResult> triggerAzure(dynamic input)
            {
                var url = "https://azuretrigger20210714172224.azurewebsites.net/api/EmployeeAdded?code=SkJpnlE9HYZ0skSh7sSEuM6gLaGGdBnQK/N6qlDfjKFg8GpwCGbsVw==";
                string json = JsonConvert.SerializeObject(input);
                StringContent data = new StringContent(json, Encoding.UTF8, "application/json");

                var client = new HttpClient();

                var response = await client.PostAsync(url, data);
                string result = await response.Content.ReadAsStringAsync();
                client.Dispose();
                return new OkObjectResult(result);

             }

        }

    }


        

        public class Employee
        {
            [Key]
            public string emp_id { get; set; }
            public string emp_name { get; set; }
            public Nullable<decimal> emp_salary { get; set; }
            public string emp_manager_id { get; set; }
            public Nullable<int> dept_id { get; set; }

        }
}


