using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class VueloPasajero
    {
        public static ML.Result VueloGetByIdPasajero(int IdPasajero)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ATranquilinoAeroMexicoEntities context = new DL.ATranquilinoAeroMexicoEntities())
                {
                    var procedure = context.VueloGetByIdPasajero2(IdPasajero).ToList();
                    result.Objects = new List<object>();

                    if(procedure != null)
                    {
                        foreach(var obj in procedure)
                        {
                            ML.VueloPasajero vueloPasajero = new ML.VueloPasajero();
                            vueloPasajero.IdVueloPasajero = obj.IdVueloPasajero;
                            vueloPasajero.Vuelo = new ML.Vuelo();
                            vueloPasajero.Vuelo.VueloNumero = obj.VueloNumero;
                            vueloPasajero.Vuelo.Origen = obj.Origen;
                            vueloPasajero.Vuelo.Destino = obj.Destino;
                            vueloPasajero.Vuelo.FechaInicio = Convert.ToString(obj.FechaInicio);
                            vueloPasajero.Vuelo.FechaFin = Convert.ToString(obj.FechaFin);
                            vueloPasajero.Vuelo.FechaSalida = Convert.ToString(obj.FechaSalida);
                            
                            result.Objects.Add(vueloPasajero);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
        public static ML.Result VueloGetNoAsignado (int IdPasajero)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ATranquilinoAeroMexicoEntities context = new DL.ATranquilinoAeroMexicoEntities())
                {
                    var procedure = context.VueloGetNoAsignado(IdPasajero).ToList();
                    result.Objects = new List<object>();

                    if (procedure != null)
                    {
                        foreach (var obj in procedure)
                        {
                            ML.VueloPasajero vueloPasajero = new ML.VueloPasajero();
                            vueloPasajero.Vuelo = new ML.Vuelo();
                            vueloPasajero.Vuelo.VueloNumero = obj.VueloNumero;
                            vueloPasajero.Vuelo.Origen = obj.Origen;
                            vueloPasajero.Vuelo.Destino = obj.Destino;
                            vueloPasajero.Vuelo.FechaSalida = Convert.ToString(obj.FechaSalida);

                            result.Objects.Add(vueloPasajero);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
        public static ML.Result ReservacionAdd(string numeroVuelo, int IdPasajero)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ATranquilinoAeroMexicoEntities context = new DL.ATranquilinoAeroMexicoEntities())
                {
                    var procedure = context.ReservacionAdd(numeroVuelo, IdPasajero);

                    if(procedure >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
    }
}
