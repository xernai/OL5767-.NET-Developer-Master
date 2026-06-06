using System;

namespace Utilities
{
    public class Calculator
    {
        public int Divide(int a, int b)
        {
            int resultado = 0;
            try
            {
               resultado = a / b;
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.ToString());
            }
            return resultado;
        }
    }
}
