using System.Collections.Generic;
using System.Web.Http;

namespace Raumverwaltung.Controllers
{
    public class RaumController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id, string sessionID)
        {
            string CheckSession = "GET URL FÜR SESSION CHECKEN";
            CheckSession += "/session " + sessionID;
            // GET REQUEST MIT CheckSession string als URL, RÜCKGABE JSON OBJ;
            /*
            {
                "responseState" : bool,
                "username" : string,
                "rolename" : string,
                "permission_read" : bool,
                "permission_read" : bool,
                "permission_read" : bool,
            }
            */
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {

        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {

        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {

        }
    }
}