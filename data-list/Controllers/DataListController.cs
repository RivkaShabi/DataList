using data_list.Models;
using data_list.DAL.Repositories;
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
using Newtonsoft.Json;


namespace data_list.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [Route("api/DataList")]
    public class DataListController : ApiController
    {
        private readonly IDataRepository dataRepository;

        public DataListController()
        {
            dataRepository = new DataRepository(); 
        }
        // GET: api/DataList
        public List<DataPerson> Get()
        {
            return dataRepository.GetlistData();
        }

        [HttpPut]
        public List<DataPerson> Put([FromBody] SearchWord wordSearch)
        {
            return dataRepository.SearchlistData( wordSearch);
        }
        //[HttpPost]
        //[Route("ConvertCsvToJson")]
        //public IHttpActionResult ConvertCsvToJson([FromBody] PathToFile path_to_file)
        //{
        //    var csv = new List<string[]>(); // or, List<YourClass>
        //    var lines = System.IO.File.ReadAllLines(path_to_file.path);
        //    foreach (string line in lines)
        //        csv.Add(line.Split(',')); // or, populate YourClass          
        //    //string json = new
        //    //    System.Web.Script.Serialization.JavaScriptSerializer().Serialize(csv);

        //    return Created("הצליח להוסיף", csv);

        //}
        // GET: api/DataList/5
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] PathToFile path_to_file)
        {
            try { 
            var csv = new List<DataPerson>();
            string[] array1;
            DataPerson person;
            //var rootPath = _hostingEnvironment.ContentRootPath;
            //var filePath = Path.Combine(dataFolderPath, "text.csv");
            //var filePath = Path.Combine(rootPath, "Data", "text.csv");
            var filePath = System.Web.Hosting.HostingEnvironment.MapPath("~/Data/text.csv");

            var lines = System.IO.File.ReadAllLines(filePath);
            //var lines = System.IO.File.ReadAllLines(path_to_file.path);
            foreach (string line in lines)
            {
                array1 = line.Split(',');
                person = new DataPerson(array1[0], array1[1], array1[2]);
                csv.Add(person);
            }
            string json = new  System.Web.Script.Serialization.JavaScriptSerializer().Serialize(csv);

            if (dataRepository != null)
            {
                dataRepository.SetlistData(JsonConvert.DeserializeObject<List<DataPerson>>(json));
            }
            if (path_to_file.searchWord != null&& path_to_file.searchWord.searchWord != null)
            {

                 json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(dataRepository.SearchlistData(path_to_file.searchWord));
            }
            return Created("Ok",json);
            }
            catch (Exception)
            {
                throw;
            }
        }

   

        //[HttpPut]
        //[Microsoft.AspNetCore.Mvc.Consumes("multipart/form-data")]

        //// POST: api/DataList
        //public async Task<IHttpActionResult> Put([FromBody]  IFormFile file)
        //{
        //    if (file == null || file.Length == 0)
        //    {
        //        return BadRequest("No file uploaded.");
        //    }

        //    var fileName = file.FileName;
        //    var filePath = Path.Combine("./data-list/Data", fileName);

        //    using (var stream = new FileStream(filePath, FileMode.Create))
        //    {
        //        await file.CopyToAsync(stream);
        //    }
        //    //return Ok("File uploaded successfully!");


        //    var csv = new List<string[]>(); 
        //    //var lines = System.IO.File.ReadAllLines(path_to_file.path);
        //    var lines = System.IO.File.ReadAllLines("./data-list/Data"+fileName);
        //    foreach (string line in lines)
        //        csv.Add(line.Split(','));          
        //    string json = new
        //        System.Web.Script.Serialization.JavaScriptSerializer().Serialize(csv);

        //    return Created("Ok", json);

        //}

        //// PUT: api/DataList/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        // DELETE: api/DataList/5
        public void Delete(int id)
        {
        }
    }
}
