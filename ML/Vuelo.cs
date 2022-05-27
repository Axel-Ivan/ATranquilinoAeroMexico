using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Vuelo
    {
        public string VueloNumero { get; set; }
        public string Origen { get; set; }
        public string Destino { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        public string FechaSalida { get; set; }
        public List<object> Vuelos { get; set; }
    }
}
