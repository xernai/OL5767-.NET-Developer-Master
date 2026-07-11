using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventHubsTajamar
{
    public class Alumno
    {
        public Alumno(int idAlumno, string nombre, string apellidos, string curso)
        {
            IdAlumno = idAlumno;
            Nombre = nombre;
            Apellidos = apellidos;
            Curso = curso;
        }

        public int IdAlumno { get; set; }
        public String Nombre { get; set; }
        public String Apellidos { get; set; }
        public String Curso { get; set; }
    }
}
