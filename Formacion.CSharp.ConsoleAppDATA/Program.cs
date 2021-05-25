using System;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using Formacion.CSharp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Formacion.CSharp.ConsoleAppDATA
{
    class Program
    {
        static void Main(string[] args)
        {
            TrabajandoConEFInclude();
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

            var context = new ModelNorthwind();


            //Consulta de Datos - SELECT
            //Equivalente a: SELECT * FROM Customers

            var clientes = context.Customers
                .ToList();

            var clientes2 = from c in context.Customers
                            select c;

            var clientes3 = context.Customers
                .Where(r => r.Country == "Spain")
                .OrderBy(r => r.City)
                .ToList();

            var clientes4 = from c in context.Customers
                            where c.Country == "Spain"
                            orderby c.City
                            select c;

            foreach (var c in clientes3)
            {
                Console.WriteLine($"ID: {c.CustomerID}");
                Console.WriteLine($"Empresa: {c.CompanyName}");
                Console.WriteLine($"Pais: {c.Country}" + Environment.NewLine);
            }


            //Insertar Datos - INSERT
            //Equivalente a: INSERT INTO Customers VALUES(..., ..., )

            var cliente = new Customers();
            
            cliente.CustomerID = "DEMO1";
            cliente.CompanyName = "Empresa Uno, SL";
            cliente.ContactName = "Borja Cabeza";
            cliente.ContactTitle = "Gerente";
            cliente.Address = "Avenida del Mediterraneo, 10";
            cliente.PostalCode = "28010";
            cliente.City = "Madrid";
            cliente.Country = "Spain";
            cliente.Phone = "910 000 001";
            cliente.Fax = "910 000 002";

            context.Customers.Add(cliente);
            context.SaveChanges();


            //Modificar Datos - UPDATE
            //Equivalente a: UPDATE Customers SET CompanyName = 'nuevo valor' WHERE CustomerID = 'DEMO1'

            var cliente2a = context.Customers
                .Where(r => r.CustomerID == "DEMO1")
                .FirstOrDefault();

            cliente2a.CompanyName = "Empresa Uno Dos y Tres, SL";
            cliente2a.PostalCode = "28014";


            var cliente2b = (from c in context.Customers
                             where c.CustomerID == "DEMO1"
                             select c).FirstOrDefault();

            cliente2b.CompanyName = "Empresa Uno Dos y Tres, SL";
            cliente2b.PostalCode = "28014";

            context.SaveChanges();


            //Eliminar Datos - DELETE
            //Equivalente a: DELETE Customers WHERE CustomerID = 'DEMO1'

            //Elimina el registro con CustomerID igual a DEMO1
            //context.Customers.Remove(context.Customers.Where(r => r.CustomerID == "DEMO1").FirstOrDefault());

            //Elimina todos los registros donde País es igual a Spain
            //context.Customers.RemoveRange(context.Customers.Where(r => r.Country == "Spain").ToList());

            var cliente3a = context.Customers
                .Where(r => r.CustomerID == "DEMO1")
                .FirstOrDefault();

            context.Customers.Remove(cliente3a);
            context.SaveChanges();
        }

        static void TrabajandoConEFInclude()
        {
            var context = new ModelNorthwind();

            //SELECT* FROM dbo.Customers WHERE CustomerID = 'ANATR';
            var cliente = context.Customers
                .Where(r => r.CustomerID == "ANATR")
                .FirstOrDefault();

            //SELECT* FROM dbo.Orders WHERE CustomerID = 'ANATR';
            var pedidos = context.Orders
                .Where(r => r.CustomerID == "ANATR")
                .ToList();

            //SELECT* FROM dbo.Customers
            //   JOIN dbo.Orders
            //   ON dbo.Customers.CustomerID = dbo.Orders.CustomerID
            //   WHERE dbo.Customers.CustomerID = 'ANATR';

            var cliente2 = context.Customers
                .Include(r => r.Orders)
                .Where(r => r.CustomerID == "ANATR")
                .FirstOrDefault();

            var cliente2b = (from c in context.Customers
                            join o in context.Orders on c.CustomerID equals o.CustomerID
                            where c.CustomerID == "ANATR"
                            select c).FirstOrDefault();

            Console.WriteLine("Cliente: {0}", cliente2.CompanyName);

            foreach (var p in cliente2.Orders)
            {
                Console.WriteLine("Pedido Núm: {0}", p.OrderID);
            }
        }
    }
}
