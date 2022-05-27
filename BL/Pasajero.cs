using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Pasajero
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ATranquilinoAeroMexicoEntities context = new DL.ATranquilinoAeroMexicoEntities())
                {
                    var procedure = context.PasajeroGetAll().ToList();
                    result.Objects = new List<object>();

                    if (procedure != null)
                    {
                        foreach (var obj in procedure)
                        {
                            ML.Pasajero pasajero = new ML.Pasajero();
                            pasajero.IdPasajero = obj.IdPasajero;
                            pasajero.Nombres = obj.Nombres;
                            pasajero.Apellidos = obj.Apellidos;
                            result.Objects.Add(pasajero);
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
        public static ML.Result Add(ML.Pasajero pasajero)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ATranquilinoAeroMexicoEntities context = new DL.ATranquilinoAeroMexicoEntities())
                {
                    var procedure = context.PasajeroAdd(pasajero.Nombres, pasajero.Apellidos);

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
