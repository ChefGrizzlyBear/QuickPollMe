using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LiteDB;

namespace QuickPollDotMe.Controllers
{
    public class Summary
    {
        public int SummaryId { get; set; }
        public string Name { get; set; }

    }

    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        private static string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet("[action]")]
        public IEnumerable<WeatherForecast> WeatherForecasts()
        {
            using(var db = new LiteDatabase(@"MyData.db"))
            {
                // Get customer collection
                var sumaries = db.GetCollection<Summary>("summaries");

                // Create your new customer instance
                var sumary1 = new Summary
                { 
                    Name = "Chilly", 
                };
                var sumary2 = new Summary
                { 
                    Name = "Caliente", 
                };
                var sumary3 = new Summary
                { 
                    Name = "Cozy", 
                };

                // Insert new customer document (Id will be auto-incremented)
                sumaries.Insert(sumary1);
                sumaries.Insert(sumary2);
                sumaries.Insert(sumary3);

                // Index document using a document property
                sumaries.EnsureIndex(x => x.Name);

                // Use Linq to query documents
                var results = sumaries.Find(x => x.Name.StartsWith("C"));

                var resList = results.ToList();

                var rng = new Random();
                return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    DateFormatted = DateTime.Now.AddDays(index).ToString("d"),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = resList[rng.Next(resList.ToList().Count()-1)].Name
                });

            }
        }

        public class WeatherForecast
        {
            public string DateFormatted { get; set; }
            public int TemperatureC { get; set; }
            public string Summary { get; set; }

            public int TemperatureF
            {
                get
                {
                    return 32 + (int)(TemperatureC / 0.5556);
                }
            }
        }
    }
}
