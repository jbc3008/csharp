using System;
using Formacion.CSharp.Objects;

namespace Formacion.CSharp.ConsolaApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            ///////////////////////////////////////////////////////////////
            //
            //  Declaración de variables
            //  [tipo] [nombre variable] = [valor inicial (opcional)]
            //
            //  Valor por defecto para variables de tipo valor (númericas), 0
            //  Valor por defecto para variables de tipo referencía Null
            //
            ///////////////////////////////////////////////////////////////
            
            string texto = "Hola Mundo !!!";
            string otroTexto;
            int numero = 10;
            int otroNumero;
            decimal a, b, c;


            ///////////////////////////////////////////////////////////////
            //
            //  Declaración de variables que contienen objetos
            //  [tipo] [nombre variable] = [new constructor (opcional)]
            //
            ///////////////////////////////////////////////////////////////

            //Instanciamos un objeto y modificamos sus propiedades o variables
            Alumno alumno = new Alumno()
            {
                Nombre = "Julian",
                Apellidos = "Sánchez",
                Edad = 24
            };

            //Instanciamos un objeto y modificamos sus propiedades o variables
            Alumno alumno1 = new Alumno();
            alumno1.Nombre = "Julian";
            alumno1.Nombre = "Sánchez";
            alumno1.Edad = 24;

            //Instaciamos un objeto con VAR, OBJECT y DYNAMIC
            var alumno2 = new Alumno();
            Object alumno3 = new Alumno();
            dynamic alumno4 = new Alumno();

            Console.WriteLine("Tipo Variable 1: " + alumno1.GetType());
            Console.WriteLine("Nombre: {0}", alumno1.Nombre);

            Console.WriteLine("Tipo Variable 2: {0}", alumno2.GetType());
            Console.WriteLine("Nombre: {0}", alumno2.Nombre);

            Console.WriteLine($"Tipo Variable 3: {alumno3.GetType()}");
            //Console.WriteLine("Nombre: {0}", alumno3.Nombre));
            Console.WriteLine("Nombre: {0}", ((Alumno)alumno3).Nombre);

            Console.WriteLine("Tipo Variable 4: " + alumno4.GetType());
            Console.WriteLine("Nombre: {0}", alumno4.Nombre);
            Console.WriteLine("Nombre: {0}", ((Alumno)alumno4).Nombre);


            ///////////////////////////////////////////////////////////////
            //
            //  Declaración de un Array
            //  [tipo][] [nombre variable] = [valor inicial]
            //
            ///////////////////////////////////////////////////////////////

            int[] numeros = new int[10];
            int[] numeros2 = { 10, 5, 345, 55, 13, 1000, 83 };

            numeros[7] = 32;
            Console.WriteLine(numeros2[5]);

            Alumno[] alumnos = new Alumno[20];
            Alumno[] alumnos2 = { new Alumno() { Nombre = "Julian", Apellidos = "Sánchez", Edad = 24 }, new Alumno(), new Alumno() };
            Alumno[] alumnos3 = { new Alumno(), new Alumno(), new Alumno() };

            alumnos3[1].Nombre = "Ana María";
            alumnos3[1].Apellidos = "Sánchez";
            alumnos3[1].Edad = 24;
            Console.WriteLine(alumnos3[1].Nombre);


            ///////////////////////////////////////////////////////////////
            //
            //  Conversión de variables
            //
            ///////////////////////////////////////////////////////////////

            byte num1 = 10;        //8 bits
            int num2 = 32;         //32 bits
            string num3 = "42";

            Console.WriteLine("A: {0} - B: {1}", num1, num2);

            //Conversión implicita, SI es posible porque el receptor es de mayor tamaño en bits
            num2 = num1;

            //Conversión implicita, NO es posible porque el receptor es de menor tamaño en bits
            //num1 = num2;      

            //Conversión explicita, indicada por el programador
            num1 = (byte)num2;

            //Conversion explicita, con el objeto CONVERT
            num1 = Convert.ToByte(num2);
            num1 = Convert.ToByte(num3);

            //Conversión explicita utilizando el método Parse
            num1 = byte.Parse(num3);

            //Conversión explicita utilizando el método TryParse
            byte.TryParse(num3, out num1);  

            Console.WriteLine("A: {0} - B: {1}", num1, num2);
        }
    }
}

namespace Formacion.CSharp.Objects
{
    public class Alumno
    {
        public string Nombre = "Borja";
        public string Apellidos = "Cabeza";
        public int Edad = 46;

        public void Funcion()
        { }
    }
}

namespace Universidad
{
    class Alumno
    {
        string Nombre;
        string Apellidos;
        int Edad;
    }
}