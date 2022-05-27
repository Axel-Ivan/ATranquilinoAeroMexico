using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Vuelo
    {
        public static ML.Result GetAll (string FechaInicio, string FechaFin)
        {
            ML.Result result = new ML.Result ();

            try
            {
                using (DL.ATranquilinoAeroMexicoEntities context = new DL.ATranquilinoAeroMexicoEntities())
                {
                    var procedure = context.VueloGetAll(FechaInicio, FechaFin).ToList();
                    result.Objects = new List<object>();

                    if(procedure != null)
                    {
                        foreach(var obj in procedure)
                        {
                            ML.Vuelo vuelo = new ML.Vuelo();
                            vuelo.VueloNumero = obj.VueloNumero;
                            vuelo.Origen = obj.Origen;
                            vuelo.Destino = obj.Destino;
                            vuelo.FechaInicio = Convert.ToString(obj.FechaInicio);
                            vuelo.FechaFin = Convert.ToString(obj.FechaFin);
                            vuelo.FechaSalida = Convert.ToString(obj.FechaSalida);
                            result.Objects.Add(vuelo);
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
        
    }
}
