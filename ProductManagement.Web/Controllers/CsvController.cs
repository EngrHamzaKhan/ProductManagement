using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Dynamic;
using ProductManagement.Web.Data;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace ProductManagement.Web.Controllers
{

    public class CsvController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CsvController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> DownloadCsv()
        {
            var products = await _context.Products.ToListAsync(); 

            if (products == null || !products.Any())
            {
                return BadRequest("No product data available.");
            }

            var csvData = GenerateCsv(products);
            var fileName = "products.csv";

            var fileBytes = Encoding.UTF8.GetBytes(csvData);

            return File(fileBytes, "text/csv", fileName);
        }

        private string GenerateCsv<T>(List<T> data)
        {
            var sb = new StringBuilder();
            var properties = typeof(T).GetProperties();

            sb.AppendLine(string.Join(",", properties.Select(p => p.Name)));

            foreach (var item in data)
            {
                var values = properties.Select(p => p.GetValue(item, null)?.ToString() ?? "");
                sb.AppendLine(string.Join(",", values));
            }
            return sb.ToString();
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UploadCsv(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return Json(new { success = false, message = "Please upload a CSV file." });
            }

            try
            {
                var data = ParseCsv(file);
                return Json(new { success = true, data }); 
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error processing file: {ex.Message}" });
            }
        }

        private List<ExpandoObject> ParseCsv(IFormFile file)
        {
            var result = new List<ExpandoObject>();

            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                var headers = reader.ReadLine()?.Split(',').Select(h => h.Trim()).ToArray();
                if (headers == null) throw new Exception("CSV file is empty or invalid.");

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var values = line.Split(',').Select(v => v.Trim()).ToArray();
                    if (values.Length != headers.Length) continue; 

                    dynamic obj = new ExpandoObject();
                    var dict = (IDictionary<string, object>)obj;

                    for (int i = 0; i < headers.Length; i++)
                    {
                        dict[headers[i]] = values[i];
                    }

                    result.Add(obj);
                }
            }

            return result;
        }
    }

}
