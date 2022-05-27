using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class VueloController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Vuelo vuelo = new ML.Vuelo();
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();

            vuelo.FechaInicio = (vuelo.FechaInicio == null) ? "0" : vuelo.FechaInicio; //Operador ternario
            vuelo.FechaFin = (vuelo.FechaFin == null) ? "0" : vuelo.FechaFin;

            string urlAPI = System.Configuration.ConfigurationManager.AppSettings["URLapi"];

            //Inicia Servicio
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlAPI);

                var responseTask = client.GetAsync("Vuelo/GetAll/" + vuelo.FechaInicio + ", " + vuelo.FechaFin);
                responseTask.Wait();

                var resultService = responseTask.Result;

                if (resultService.IsSuccessStatusCode)
                {
                    var readTask = resultService.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    foreach (var resultItem in readTask.Result.Objects)
                    {
                        vuelo = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Vuelo>(resultItem.ToString());
                        result.Objects.Add(vuelo);
                        vuelo.Vuelos = result.Objects;
                    }
                }
            }

            return View(vuelo);
        }

        [HttpPost]
        public ActionResult GetAll(ML.Vuelo vuelo)
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();

            vuelo.FechaInicio = (vuelo.FechaInicio == null) ? "0" : vuelo.FechaInicio; //Operador ternario
            vuelo.FechaFin = (vuelo.FechaFin == null) ? "0" : vuelo.FechaFin;

            string urlAPI = System.Configuration.ConfigurationManager.AppSettings["URLapi"];

            //Inicia Servicio
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlAPI);

                var responseTask = client.GetAsync("Vuelo/GetAll/" + vuelo.FechaInicio + ", " + vuelo.FechaFin);
                responseTask.Wait();

                var resultService = responseTask.Result;

                if (resultService.IsSuccessStatusCode)
                {
                    var readTask = resultService.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    foreach (var resultItem in readTask.Result.Objects)
                    {
                        vuelo = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Vuelo>(resultItem.ToString());
                        result.Objects.Add(vuelo);
                        vuelo.Vuelos = result.Objects;
                    }
                }
            }

            return View(vuelo);
        }
    }
}