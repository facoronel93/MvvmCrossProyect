using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect.core.Services.Models
{
    public class DatosClima
    {

        public int Id { get; set; }

        public string Ciudad { get; set; }

        public string Pais { get; set; }

        public string Temperatura { get; set; }

        public string Humedad { get; set; }

        public string HoraConsulta { get; set; }

        public DatosClima()
        {
            Ciudad = string.Empty;
            Pais = string.Empty;
            Temperatura = string.Empty;
            Humedad = string.Empty;

        }
    }
}