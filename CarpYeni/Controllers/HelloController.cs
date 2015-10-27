using CarpYeni.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CarpYeni.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class HelloController : ApiController
    {
        /// <summary>
        /// This method will be used to wake up web service
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Get()
        {
            Score score;
            try
            {
                using (var db = new CarpmaContext())
                {
                    score = db.Scores.First();
                }
                return Ok(score);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}
