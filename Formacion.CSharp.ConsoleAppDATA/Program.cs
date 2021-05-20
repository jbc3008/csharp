using System;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using Formacion.CSharp.Data.Models;


namespace Formacion.CSharp.ConsoleAppDATA
{
    class Program
    {
        static void Main(string[] args)
        {
            TrabajandoConEF();
        }

        static void TrabajandoConADONET()
        {
            //ADO.NET Access Data Object (manejamos la base de datos con Transat-SQL)

            //Consulta de Datos - SELECT
            //Equivalente a: SELECT * FROM Customers

            //Creamos un objeto para definir la cadena de conexión
            var connectionString = new SqlConnectionStringBuilder() 
            { 
                DataSource = "LOCALHOST",
                InitialCatalog = "NORTHWIND",
                UserID = "",
                Password = "",
                IntegratedSecurity = true,
                ConnectTimeout = 60
            };

            //Muestra la cadena de conexión resultante con los datos introducidos
            Console.WriteLine("Cadena de Conexión: {0}", connectionString.ToString());

            //Creamos un objeto conexión, representa la conexión con la base de datos
            var connect = new SqlConnection() 
            { 
                ConnectionString = connectionString.ToString()
            };

            Console.WriteLine($"Estado: {connect.State.ToString()}");
            connect.Open();
            Console.WriteLine($"Estado: {connect.State.ToString()}");

            //Creamos un objeto Command que nos permite lanzar comando contra la base de datos
            var command = new SqlCommand() { 
                Connection = connect,
                CommandText = "SELECT * FROM dbo.Customers"
            };

            //Creamos un objeto que funcione como curso, permitiendo recorrer los datos retornados por la base de datos
            var reader = command.ExecuteReader();

            if (reader.HasRows == false) Console.WriteLine("Registros no encontrados.");
            else 
            {
                while (reader.Read() == true)
                {
                    Console.WriteLine($"ID: {reader["CustomerID"]}");
                    Console.WriteLine($"Empresa: {reader.GetValue(1)}");
                    Console.WriteLine($"Pais: {reader["Country"]}" + Environment.NewLine);
                }
            }

            //Cerramos conexiones y destruimos variables
            reader.Close();
            command.Dispose();
            connect.Close();
            connect.Dispose();
        }

        static void TrabajandoConEF()
        {
            //EntityFramework (manejamos las base de datos como colecciones)

            //Consulta de Datos - SELECT
            //Equivalente a: SELECT * FROM Customers

            var context = new ModelNorthwind();


            var clientes = context.Customers
                .ToList();

            var clientes2 = from c in context.Customers
                            select c;

            foreach (var cliente in clientes)
            {
                Console.WriteLine($"ID: {cliente.CustomerID}");
                Console.WriteLine($"Empresa: {cliente.CompanyName}");
                Console.WriteLine($"Pais: {cliente.Country}" + Environment.NewLine);
            }

        }
    }
}
