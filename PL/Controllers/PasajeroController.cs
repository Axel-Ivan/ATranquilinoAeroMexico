using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class PasajeroController : Controller
    {
        // GET: Pasajero
        [HttpGet]
        public ActionResult Form()
        {
            ML.Pasajero pasajero = new ML.Pasajero();

            return View(pasajero);
        }

        [HttpPost]
        public ActionResult Form(ML.Pasajero pasajero)
        {
            //Inicia servicio
            using (var client = new HttpClient())
            {
                string urlAPI = System.Configuration.ConfigurationManager.AppSettings["URLapi"];
                client.BaseAddress = new Uri(urlAPI);

                var postTask = client.PostAsJsonAsync<ML.Pasajero>("Pasajero/Add", pasajero);
                postTask.Wait();

                var resultAdd = postTask.Result;
                if (resultAdd.IsSuccessStatusCode)
                {
                    ViewBag.Message = "El pasajero se ingresó correctamente!!!";
                }
                else
                {
                    ViewBag.Message = "Ocurrió un error al momento de ingresar el pasajero";
                }
            }

            return PartialView("ValidationModal");
        }
    }
}