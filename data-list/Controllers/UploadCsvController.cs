using data_list.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace data_list.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [Route("api/UploadCsv")]
    public class UploadCsvController : ApiController
    {
        // GET: api/UploadCsv
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/UploadCsv/5
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        [Microsoft.AspNetCore.Mvc.Consumes("multipart/form-data")]

        // POST: api/DataList
        public async Task<IHttpActionResult> Post([FromBody] IFormFile file)
        {

        //    var file = HttpContext.Current.Request.Files.Count > 0 ?
        //HttpContext.Current.Request.Files[0] : null;

            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            var fileName = file.FileName;
            var filePath = Path.Combine("./data-list/Data", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            //return Ok("File uploaded successfully!");


            var csv = new List<string[]>(); // or, List<YourClass>
            //var lines = System.IO.File.ReadAllLines(path_to_file.path);
            var lines = System.IO.File.ReadAllLines("./data-list/Data" + fileName);
            foreach (string line in lines)
                csv.Add(line.Split(',')); // or, populate YourClass          
            string json = new
                System.Web.Script.Serialization.JavaScriptSerializer().Serialize(csv);

            return Created("Ok", json);

        }


        // PUT: api/UploadCsv/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/UploadCsv/5
        public void Delete(int id)
        {
        }
    }
}
