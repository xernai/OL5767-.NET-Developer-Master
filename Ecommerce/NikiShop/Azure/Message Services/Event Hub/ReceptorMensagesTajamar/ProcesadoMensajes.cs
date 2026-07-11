
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceptorMensagesTajamar
{
    public class ProcesadorMensajes : IEventProcessor
    {
        public Task CloseAsync(PartitionContext context, CloseReason reason)
        {
            Console.WriteLine($"Proceso apagando. Particion "
                + context.Lease.PartitionId
                + ", Razon: " + reason + ".");
            return Task.CompletedTask;
        }

        public Task OpenAsync(PartitionContext context)
        {
            Console.WriteLine($"Proceso abriendo. Particion "
                + context.Lease.PartitionId);
            return Task.CompletedTask;
        }

        public Task ProcessEventsAsync(PartitionContext context, IEnumerable<EventData> messages)
        {
            //RECORREMOS TODOS LOS MENSAJES
            foreach (EventData mensaje in messages)
            {
                //RECUPERAMOS LOS DATOS DEL MENSAJE
                String json = Encoding.UTF8.GetString(mensaje.GetBytes());
                //DESERIALIZAMOS EN MENSAJE QUE HABIAMOS SERIALIZADO A UN STRING JSON
                //PARA USARLO COMO OBJETO
                Mensaje newmesaje = JsonConvert.DeserializeObject<Mensaje>(json);
                String datos = ""; 
                //COMPROBAMOS QUE ALUMNOS SE HAN PASADO DEL MAXIMO DE FALTAS PERMITIDAS POR MES
                //Y SI ES ASI MOSTRAMOS UN MENSAJE EN ROJO EN LA CONSOLA CON EL ALUMNO
                if (newmesaje.Faltas > newmesaje.Mes.Faltas)
                {
                    datos = "El alumno: " + newmesaje.Alumno.Nombre + ", con id: " + newmesaje.Alumno.IdAlumno + " ha sobrepasa el numero de faltas permitidas en el mes: " + newmesaje.Mes.Nombre + ", con un total de faltas de: " + newmesaje.Faltas + ". Avisar a Juan.";
                    String msj = string.Format("Mensaje recibido. " + datos);
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine(msj);
                    Console.ResetColor();
                }
                //SI NO, MOSTRAMOS LOS ALUMNOS Y LAS FALTAS QUE HA TENIDO ESE MES
                else
                {
                    datos = "El alumno: " + newmesaje.Alumno.Nombre + ", con id: " + newmesaje.Alumno.IdAlumno + " ha faltado en el mes: " + newmesaje.Mes.Nombre + ", " + newmesaje.Faltas + " veces.";
                    String msj = string.Format("Mensaje recibido. "+ datos);
                    Console.WriteLine(msj);
                }
                
            }
            return context.CheckpointAsync();
        }
    }
}
