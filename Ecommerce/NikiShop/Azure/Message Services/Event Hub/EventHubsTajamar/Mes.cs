using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventHubsTajamar
{
    public class Mes
    {
        public Mes(int idMes, string nombre, int faltas)
        {
            IdMes = idMes;
            Nombre = nombre;
            Faltas = faltas;
        }

        public int IdMes { get; set; }
        public String Nombre { get; set; }
        public int Faltas { get; set; }
    }
}
