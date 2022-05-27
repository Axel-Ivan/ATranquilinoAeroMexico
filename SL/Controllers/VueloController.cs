using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SL.Controllers
{
    public class VueloController : ApiController
    {
        // GET: api/Vuelo
        [HttpGet]
        [Route("api/Vuelo/GetAll/{FechaInicio}, {FechaFin}")]
        public IHttpActionResult GetAll(string FechaInicio, string FechaFin)
        {
            ML.Result result = BL.Vuelo.GetAll(FechaInicio, FechaFin);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return InternalServerError();
            }
        }
    }
}
