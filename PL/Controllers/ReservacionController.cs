using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Configuration;

namespace PL.Controllers
{
    public class ReservacionController : Controller
    {
        // GET: Reservacion
        [HttpGet]
        public ActionResult PasajeroGetAll()
        {
            //ML.Result resultPasajeros = BL.Pasajero.GetAll();
            ML.Pasajero pasajero = new ML.Pasajero();
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();

            string urlAPI = System.Configuration.ConfigurationManager.AppSettings["URLapi"];

            //Inicia Servicio
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlAPI);

                var responseTask = client.GetAsync("Pasajero/GetAll");
                responseTask.Wait();

                var resultService = responseTask.Result;

                if (resultService.IsSuccessStatusCode)
                {
                    var readTask = resultService.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    foreach (var resultItem in readTask.Result.Objects)
                    {
                        pasajero = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Pasajero>(resultItem.ToString());
                        result.Objects.Add(pasajero);
                        pasajero.Pasajeros = result.Objects;
                    }
                }

                ML.VueloPasajero vueloPasajero = new ML.VueloPasajero();
                vueloPasajero.Pasajero = new ML.Pasajero();

                if (pasajero.Pasajeros != null)
                {
                    vueloPasajero.Pasajero.Pasajeros = pasajero.Pasajeros;
                    return View(vueloPasajero);
                }
                else
                {
                    ViewBag.Message = "No se encontraron pasajeros";
                    return PartialView("ValidationModal");
                }
            }
        }

        [HttpGet]
        public ActionResult AsignarVuelo(int IdPasajero)
        {
            ML.Vuelo vuelo = new ML.Vuelo();
            ML.VueloPasajero vueloPasajero = new ML.VueloPasajero();
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            //ML.Result resultVuelos = BL.VueloPasajero.VueloGetByIdPasajero(IdPasajero);

            string urlAPI = System.Configuration.ConfigurationManager.AppSettings["URLapi"];

            //Inicia Servicio
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlAPI);

                var responseTask = client.GetAsync("VueloPasajero/GetById/" + IdPasajero);
                responseTask.Wait();

                var resultService = responseTask.Result;

                if (resultService.IsSuccessStatusCode)
                {
                    var readTask = resultService.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    foreach (var resultItem in readTask.Result.Objects)
                    {
                        vueloPasajero = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.VueloPasajero>(resultItem.ToString());
                        result.Objects.Add(vueloPasajero);
                        vueloPasajero.VueloPasajeros = result.Objects;
                    }
                }

                vueloPasajero.Pasajero = new ML.Pasajero();

                if (vueloPasajero.VueloPasajeros != null)
                {
                    vueloPasajero.Pasajero.IdPasajero = IdPasajero;
                    return View(vueloPasajero);
                }
                else
                {
                    ViewBag.Message = "No se encontraron pasajeros";
                    return PartialView("ValidationModal");
                }
            }
        }

        [HttpGet]
        public ActionResult VuelosNoAsignados(int IdPasajero)
        {
            ML.VueloPasajero vueloPasajero = new ML.VueloPasajero();
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            //ML.Result resultVuelos = BL.VueloPasajero.VueloGetNoAsignado(IdPasajero);

            string urlAPI = System.Configuration.ConfigurationManager.AppSettings["URLapi"];

            //Inicia Servicio
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlAPI);

                var responseTask = client.GetAsync("VueloPasajero/VueloGetNoAsignados/" + IdPasajero);
                responseTask.Wait();

                var resultService = responseTask.Result;

                if (resultService.IsSuccessStatusCode)
                {
                    var readTask = resultService.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    foreach (var resultItem in readTask.Result.Objects)
                    {
                        vueloPasajero = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.VueloPasajero>(resultItem.ToString());
                        result.Objects.Add(vueloPasajero);
                        vueloPasajero.VueloPasajeros = result.Objects;
                    }
                }

                vueloPasajero.Pasajero = new ML.Pasajero();

                if (vueloPasajero.VueloPasajeros != null)
                {
                    vueloPasajero.Pasajero.IdPasajero = IdPasajero;
                    return View(vueloPasajero);
                }
                else
                {
                    ViewBag.Message = "No se encontraron pasajeros";
                    return PartialView("ValidationModal");
                }
            }
        }

        [HttpPost]
        public ActionResult VuelosNoAsignados(ML.VueloPasajero vueloPasajero)
        {
            vueloPasajero.Vuelo = new ML.Vuelo();

            if(vueloPasajero.VueloPasajeros != null)
            {
                int i = 0;

                foreach (string vueloNumero in vueloPasajero.VueloPasajeros)
                {
                    //ML.Result result = BL.VueloPasajero.ReservacionAdd(vueloNumero, vueloPasajero.Pasajero.IdPasajero);
                    vueloPasajero.IdVueloPasajero = i;

                    //Inicia Servicio
                    using (var client = new HttpClient())
                    {
                        string urlAPI = System.Configuration.ConfigurationManager.AppSettings["URLapi"];
                        client.BaseAddress = new Uri(urlAPI);

                        var postTask = client.PostAsJsonAsync<ML.VueloPasajero>("VueloPasajero/VueloGetNoAsignados", vueloPasajero);
                        postTask.Wait();

                        var resultAdd = postTask.Result;
                        if (resultAdd.IsSuccessStatusCode)
                        {
                            ViewBag.Message = "El vuelo se asignó correctamente!!!";
                        }
                        else
                        {
                            ViewBag.Message = "Ocurrió un error al momento de asignar el vuelo";
                        }
                    }
                    i++;
                }
            }

            return PartialView("ValidationModal", vueloPasajero);
        }
    }
}