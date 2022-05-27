using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SL.Controllers
{
    public class VueloPasajeroController : ApiController
    {
        [HttpGet]
        [Route("api/VueloPasajero/GetById/{IdPasajero}")]
        public IHttpActionResult VueloGetByIdPasajero(int IdPasajero)
        {
            ML.Result result = BL.VueloPasajero.VueloGetByIdPasajero(IdPasajero);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("api/VueloPasajero/VueloGetNoAsignados/{IdPasajero}")]
        public IHttpActionResult VueloGetNoAsignados(int IdPasajero)
        {
            ML.Result result = BL.VueloPasajero.VueloGetNoAsignado(IdPasajero);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("api/VueloPasajero/VueloGetNoAsignados")]
        public IHttpActionResult ReservacionAdd([FromBody]ML.VueloPasajero vueloPasajero)
        {
            string vueloNumero = (string)vueloPasajero.VueloPasajeros[vueloPasajero.IdVueloPasajero];
            ML.Result result = BL.VueloPasajero.ReservacionAdd(vueloNumero, vueloPasajero.Pasajero.IdPasajero);
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
