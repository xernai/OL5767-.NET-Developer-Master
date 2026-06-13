using System;

namespace AbstractClass
{
    public abstract class People
    {
        public string Name { get; set; }

        public abstract void TramitarINE();

        public virtual void Casarse()
        {

        }

        protected People()
        {
                
        }
    }

    public class Trabajador : People
    {
        public Trabajador()
        {
                
        }
        public override void Casarse()
        {
            Console.WriteLine("Casarse");
        }

        public override void TramitarINE()
        {
            throw new NotImplementedException();
        }

        public void PagarISR()
        {
            throw new NotImplementedException();
        }

    }
    internal class Program
    {
        static void Main(string[] args)
        {
            // People people = new People();
            Trabajador trabajador = new Trabajador();
            trabajador.PagarISR();
        }
    }
}
