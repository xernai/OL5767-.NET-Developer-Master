using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EventHubsTajamar
{
  public class Program
    {
        static void Main(string[] args)
        {
            //ACCEDEMOS A LA CLAVE EVENT HUB DE APPSETTINGS
            string claveeventhub = ConfigurationManager.AppSettings["eventhub"];
            //NOMBRE DEL EVENT HUB CONTENEDOR
            String eventhubname = "auladisereventhub";
            //CREAMOS UN CLIENTE PARA LOS MENSAJES
            EventHubClient cliente = EventHubClient.CreateFromConnectionString(claveeventhub, eventhubname);
            //RECUPERAMOS LAS MESES Y LOS ALUMNOS PARA NUESTRA LOGICA
           
            List<Mes> listaMeses = GetMeses();
            List<Alumno> listaAlumnos = GetAlumnos();
            //VAMOS A REALIZAR UN BUCLE DE LOS MESES Y DE LOS ALUMNOS PARA CREAR MENSAJES
            //Y ENVIARLOS AL PROCESO DE SERVICE BUS


            //RECORREMOS LOS MESES QUE HAY LECTIVOS
            foreach (Mes mes in listaMeses)
            {
                //CAMBIAMOS EL COLOR DE LA CONSOLA PARA QUE SE VEA VERDE EL MES Y LUEGO LO RESETEAMOS PARA QUE EL RESTO DE MENSAJES SE VEAN NORMALES
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("---MENSAJES PARA EL MES: " + mes.Nombre +" MAXIMO NUMERO DE FALTAS: " + mes.Faltas);
                Console.ResetColor();
                //CREAMOS UN SPIN PARA QUE PAREZCA QUE SE ESTAN CARGANDO LOS MENSAJES
                ConsoleSpiner spin = new ConsoleSpiner();
                Console.Write("Cargando mensajes...");
                int c = 0;
                //HACEMOS QUE EL SPIN SE MUEVA CADA SEGUNDO 6 VECES
                while (c < 6)
                {
                    Thread.Sleep(1000);
                    spin.Turn();
                    c++;
                }
                Console.WriteLine();
                //RECORREMOS LOS ALUMNOS Y CREAMOS LOS MENSAJES POR CADA MES
                foreach (Alumno alumno in listaAlumnos)
                {
                    try
                    {
                        
                        //RECUPERAMOS EL MENSAJE QUE DESEAMOS ENVIAR
                        Mensaje mensaje = CrearMensajeUsuario(mes, alumno);
                        //ESCRIBIMOS MENSAJES POR PANTALLA DE LO QUE VAMOS A ENVIAR
                        //AL SERVICE BUS
                        Console.WriteLine("Fecha: "+DateTime.Now + ", Mes: "+ mensaje.Mes.Nombre +", Alumno: "
                            + mensaje.Alumno.Nombre +", con id: " + mensaje.Alumno.IdAlumno + " FALTAS: --> " + mensaje.Faltas);
                        //CONVERTIMOS LA INFORMACION QUE VAMOS A ENVIAR EN UN STING JSON
                        String json = JsonConvert.SerializeObject(mensaje);
                        //ENVIAMOS EL MENSAJE A NUESTRO EVENTO HUB
                        //EL SERVICIO RECIBE LA INFORMACION EN STREAM O BYTES
                        //VAMOS A TRANSFORMAR EL MENSAJE EN BYTE[]
                        cliente.Send(new EventData(Encoding.UTF8.GetBytes(json)));
                        
                        
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("---FIN MENSAJES DEL MES: " + mes.Nombre);
                Console.ResetColor();
                Console.WriteLine();
            }

        }
        //FUNCION PARA OBTENER LOS MESES
        public static List<Mes> GetMeses()
        {
            String recurso = "EventHubsTajamar.Meses.xml";
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(recurso);
            XDocument docxml = XDocument.Load(stream);
            var consulta = from datos in docxml.Descendants("mes")
                           select new Mes( int.Parse(datos.Element("idmes").Value),
                               datos.Element("nombre").Value,
                               int.Parse(datos.Element("faltas").Value));
                               
            return consulta.ToList();
        }
        //FUNCION PARA OBTENER LOS ALUMNOS
        public static List<Alumno> GetAlumnos()
        {
            String recurso = "EventHubsTajamar.AlumnosTajamar.xml";
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(recurso);
            XDocument docxml = XDocument.Load(stream);
            var consulta = from datos in docxml.Descendants("alumno")
                           select new Alumno(
                               int.Parse(datos.Element("idalumno").Value),
                               datos.Element("nombre").Value,
                               datos.Element("apellidos").Value,
                               datos.Element("curso").Value);
            return consulta.ToList();
        }
        //FUNCION PARA CREAR LOS MENSAJES
        public static Mensaje CrearMensajeUsuario(Mes mes, Alumno alumno )
        {
            Random rndfalta = new Random();
            int faltas = rndfalta.Next(0, 5);
            Mensaje mensaje = new Mensaje(alumno, mes, faltas);
            return mensaje;
        }
        //FUNCION CON PARA LA CREACION DEL SPIN
        public class ConsoleSpiner
        {
            int counter;
            public ConsoleSpiner()
            {
                counter = 0;
            }
            public void Turn()
            {
                counter++;
                switch (counter % 4)
                {
                    case 0: Console.Write("/"); break;
                    case 1: Console.Write("-"); break;
                    case 2: Console.Write("\\"); break;
                    case 3: Console.Write("|"); break;
                }
                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
            }
        }
    }
}
