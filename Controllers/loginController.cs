using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Newtonsoft.Json;
using saafcity_fyp.Models;

namespace saafcity_fyp.Controllers
{
    public class loginController : ApiController
    {
        private SaafCity_Database_2Entities db = new SaafCity_Database_2Entities();
        

        [HttpPost]
        public string Post([FromBody]Complainnat complainnat)
        {
                return JsonConvert.SerializeObject("User is existing in database");
            
        }
        [HttpGet]
        public string GetData()
        {
            return db.Departments.Sum(m => m.No_Of_Complaints).ToString();
        }


    }
}