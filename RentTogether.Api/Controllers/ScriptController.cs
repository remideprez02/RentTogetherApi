using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RentTogether.Entities;
using RentTogether.Interfaces.Dal;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RentTogether.Api.Controllers
{
    
    public class ScriptController : Controller
    {
        private readonly RentTogetherDbContext _rentTogetherDbContext;
        public ScriptController(RentTogetherDbContext rentTogetherDbContext)
        {
            _rentTogetherDbContext = rentTogetherDbContext;
        }
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [Route("api/Script")]
        [HttpGet]
        public string ChangePasswordToSha256()
        {
            using(var sha256 = SHA256.Create())  
                {  
                    // Send a sample text to hash.  
                    var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes("hello world"));  
                    // Get the hashed string.  
                    var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();  
                    // Print the string.   
                    Console.WriteLine(hash);  
                } 
            return "value";
        }

        [Route("api/Script/csv")]
        [HttpGet]
        public string BatchCsv(){

            var file = "/Users/remi/Downloads/laposte_hexasmal.csv";
            var count = 1;
            using (var streamReader = System.IO.File.OpenText(file))
            {
                
                while (!streamReader.EndOfStream)
                {
                    var line = streamReader.ReadLine();
                    if (count != 1)
                    {


                        var data = line.Split(new[] { ';' });
                        var postalCode = new PostalCode()
                        {
                            InseeCode = data[0],
                            Libelle = data[1],
                            PostalCodeId = data[2],
                            Libelle2 = data[4],
                            Gps = data[5]
                        };
                        _rentTogetherDbContext.PostalCodes.Add(postalCode);
                    }
                    count = 0;
                }
                _rentTogetherDbContext.SaveChanges();


            }
            return "OK";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
