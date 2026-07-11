using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceptorMensagesTajamar
{
    public class Mensaje
    {
        public Mensaje(Alumno alumno, Mes mes, int faltas)
        {
            Alumno = alumno;
            Mes = mes;
            Faltas = faltas;
        }

        public Alumno Alumno { get; set; }
        public Mes Mes { get; set; }
        public int Faltas { get; set; }
    }
}
