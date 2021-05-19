using System;
using System.Collections.Generic;
using System.Linq;

namespace Formacion.CSharp.ConsoleAppLINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            //Escribe el método del ejemplo que quieres probar
            Agrupaciones();
        }

        /// <summary>
        /// Busquedas básicas con LINQ
        /// </summary>
        static void BusquedasBasicas()
        {
            //Equivalente a: SELECT * FROM ListaProductos

            var resultado1a = DataLists.ListaProductos
                .ToList();

            var resultado1b = from r in DataLists.ListaProductos
                            select r;


            //Equivalente a: SELECT * FROM ListaProductos WHERE precio > 2

            var resultado2a = DataLists.ListaProductos
                .Where(x => x.Precio > 2)
                .ToList();

            var resultado2b = from r in DataLists.ListaProductos
                             where r.Precio > 2
                             select r;


            //Equivalente a: SELECT * FROM ListaProductos WHERE precio > 2 ORDER BY precio DESC

            var resultado3a = DataLists.ListaProductos
                .Where(x => x.Precio > 2)
                .OrderByDescending(x => x.Precio)
                .ToList();

            var resultado3b = from r in DataLists.ListaProductos
                             where r.Precio > 2
                             orderby r.Precio descending
                             select r;


            //Equivalente a: SELECT Descripcion, Precio FROM ListaProductos WHERE precio > 2 ORDER BY precio DESC
            var resultado4a = DataLists.ListaProductos
                .Where(x => x.Precio > 2)
                .OrderByDescending(x => x.Precio)
                .Select(x => new { x.Descripcion, x.Precio })
                .ToList();

            var resultado4b = from r in DataLists.ListaProductos
                             where r.Precio > 2
                             orderby r.Precio descending
                             select new { r.Descripcion, r.Precio };

            foreach (var item in resultado1a)
            {
                Console.WriteLine($"{item.Descripcion}  {item.Precio}");
            }
        }

        static void Busquedas()
        {
            //Productos con precio superior a 2

            //Equivalente a: SELECT * FROM ListaProductos WHERE precio > 2
            var resultado1a = DataLists.ListaProductos
                .Where(x => x.Precio > 2)
                .ToList();

            var resultado1b = from r in DataLists.ListaProductos
                             where r.Precio > 2
                             select r;


            //Equivalente a: SELECT * FROM ListaProductos WHERE precio > 2 ORDER BY precio DESC
            var resultado2a = DataLists.ListaProductos
                .Where(x => x.Precio > 2)
                .OrderByDescending(x => x.Precio)
                .ToList();

            var resultado2b = from r in DataLists.ListaProductos
                             where r.Precio > 2
                             orderby r.Precio descending
                             select r;

            //Equivalente a: SELECT Descripcion, Precio FROM ListaProductos WHERE precio > 2 ORDER BY precio DESC
            var resultado3a = DataLists.ListaProductos
                .Where(x => x.Precio > 2)
                .OrderByDescending(x => x.Precio)
                .Select(x => new { x.Descripcion, x.Precio })
                .ToList();

            var resultado3b = from r in DataLists.ListaProductos
                             where r.Precio > 2
                             orderby r.Precio descending
                             select new { r.Descripcion, r.Precio };


            foreach (var item in resultado1a)
            {
                Console.WriteLine($"{item.Descripcion}  {item.Precio}");
            }
        }

        static void Busquedas2()
        {
            //Clientes nacidos entre 1980 y 1990

            //La base de datos nos retorna la lista de clientes y luego los contamos
            var clientes1 = DataLists.ListaClientes
                .Where(x => x.FechaNac.Year >= 1980 && x.FechaNac.Year <= 1990)
                .ToList().Count;

            //La base de datos nos retorna el número de clientes
            var clientes2 = DataLists.ListaClientes
                .Where(x => x.FechaNac.Year >= 1980 && x.FechaNac.Year <= 1990)
                .Count();

            //La base de datos nos retorna el número de clientes
            var clientes3 = DataLists.ListaClientes
                .Count(x => x.FechaNac.Year >= 1980 && x.FechaNac.Year <= 1990);

            //La base de datos nos retorna la lista de clientes y luego los contamos
            var clients = (from c in DataLists.ListaClientes
                           where c.FechaNac.Year >= 1980 && c.FechaNac.Year <= 1990
                           select c).ToList().Count;

            //La base de datos nos retorna el número de clientes
            var clients2 = (from c in DataLists.ListaClientes
                            where c.FechaNac.Year >= 1980 && c.FechaNac.Year <= 1990
                            select c).Count();
        }

        static void Busquedas3()
        {
            //El producto de mayor precio

            //Ordenamos por precio descedente y nos quedamos con el primer elemento
            var productoA = DataLists.ListaProductos
                    .OrderByDescending(r => r.Precio)
                    .FirstOrDefault();

            var productoB = (from r in DataLists.ListaProductos
                              orderby r.Precio descending
                              select r).FirstOrDefault();


            //////////////////////////////////////////////////////////////////////////////////////////////////////////


            //Buscamos el precio más alto
            var precioMax1a = DataLists.ListaProductos.Select(r => r.Precio).Max();
            var precioMax1b = DataLists.ListaProductos.Max(r => r.Precio);
            var precioMax1c = (from r in DataLists.ListaProductos select r.Precio).Max();

            //Conociendo el precio más alyo buscamos el producto coincidente
            var producto2a = DataLists.ListaProductos
                .Where(r => r.Precio == precioMax1a)
                .FirstOrDefault();

            var producto2b = from r in DataLists.ListaProductos
                            where r.Precio == precioMax1a
                            select r;


            //////////////////////////////////////////////////////////////////////////////////////////////////////////


            //Incluimos en la búsqueda del producto la del precio máximo
            var producto3a = DataLists.ListaProductos
                .Where(r => r.Precio == DataLists.ListaProductos.Select(r => r.Precio).Max())
                .FirstOrDefault();

            var producto3b = DataLists.ListaProductos
                .Where(r => r.Precio == DataLists.ListaProductos.Max(r => r.Precio))
                .FirstOrDefault();

            var producto3c = (from r in DataLists.ListaProductos
                              where r.Precio == (from s in DataLists.ListaProductos select s.Precio).Max()
                              select r).FirstOrDefault();


            Console.WriteLine($"{productoA.Id}#  {productoA.Descripcion}  {productoA.Precio}");
        }

        static void Busquedas4()
        {
            //Producto que contenga en descripción la palabra cuaderno
            var r1a = DataLists.ListaProductos
               .Where(r => r.Descripcion.Contains("cuaderno"))
               .ToList();

            var r1b = from c in DataLists.ListaProductos
                      where c.Descripcion.Contains("cuaderno")
                      select c;


            //Producto que descripción comience por la palabra cuaderno
            var r2a = DataLists.ListaProductos
                .Where(r => r.Descripcion.StartsWith("cuaderno"))
                .ToList();

            var r2b = from c in DataLists.ListaProductos
                      where c.Descripcion.StartsWith("cuaderno")
                      select c;


            //Producto que descripción finalice por la palabra cuaderno
            var r3a = DataLists.ListaProductos
                .Where(r => r.Descripcion.EndsWith("cuaderno"))
                .ToList();

            var r3b = from c in DataLists.ListaProductos
                      where c.Descripcion.EndsWith("cuaderno")
                      select c;
        }

        static void Agrupaciones()
        {
            //Datos de los clientes con sus pedidos
            //Sin Agrupación
            foreach (var c in DataLists.ListaClientes.ToList())
            {
                int numPedidos = DataLists.ListaPedidos
                    .Where(r => r.IdCliente == c.Id)
                    .Count();

                Console.WriteLine($"{c.Nombre} - {numPedidos} pedidos.");
            }
            Console.WriteLine(Environment.NewLine);


            //Datos de los clientes con sus pedidos
            //Con Agrupación
            var data1 = DataLists.ListaPedidos
                .GroupBy(r => r.IdCliente)
                .ToList();

            //Con Agrupación, obteniendo los datos del cliente
            var data2 = DataLists.ListaPedidos
                .GroupBy(r => r.IdCliente)
                .Select(r => new {
                    r.Key,
                    totalPedidos = r.Count(),
                    pedidos = r,
                    cliente = DataLists.ListaClientes.Where(s => s.Id == r.Key).FirstOrDefault()
                }).ToList();

            foreach (var c in data2)
            {
                Console.WriteLine($"{c.cliente.Nombre} - {c.totalPedidos} pedidos.");
            }
        }
    }

    /// <summary>
    /// Representa el Objeto Cliente
    /// </summary>
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaNac { get; set; }
    }

    /// <summary>
    /// Representa el Objeto Producto
    /// </summary>
    public class Producto
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public float Precio { get; set; }
    }

    /// <summary>
    /// Representa el Objeto Pedido
    /// </summary>
    public class Pedido
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public DateTime FechaPedido { get; set; }
    }

    /// <summary>
    /// Representa el Objeto Linea de Pedido
    /// </summary>
    public class LineaPedido
    {
        public int Id { get; set; }
        public int IdPedido { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
    }

    /// <summary>
    /// Representa una Base de datos en memoria utilizando LIST
    /// </summary>
    public static class DataLists
    {
        private static List<Cliente> _listaClientes = new List<Cliente>() {
            new Cliente { Id = 1,   Nombre = "Carlos Gonzalez Rodriguez",   FechaNac = new DateTime(1980, 10, 10) },
            new Cliente { Id = 2,   Nombre = "Luis Gomez Fernandez",        FechaNac = new DateTime(1961, 4, 20) },
            new Cliente { Id = 3,   Nombre = "Ana Lopez Diaz ",             FechaNac = new DateTime(1947, 1, 15) },
            new Cliente { Id = 4,   Nombre = "Fernando Martinez Perez",     FechaNac = new DateTime(1981, 8, 5) },
            new Cliente { Id = 5,   Nombre = "Lucia Garcia Sanchez",        FechaNac = new DateTime(1973, 11, 3) }
        };

        private static List<Producto> _listaProductos = new List<Producto>()
        {
            new Producto { Id = 1,      Descripcion = "Boligrafo",          Precio = 0.35f },
            new Producto { Id = 2,      Descripcion = "Cuaderno grande",    Precio = 1.5f },
            new Producto { Id = 3,      Descripcion = "Cuaderno pequeño",   Precio = 1 },
            new Producto { Id = 4,      Descripcion = "Folios 500 uds.",    Precio = 3.55f },
            new Producto { Id = 5,      Descripcion = "Grapadora",          Precio = 5.25f },
            new Producto { Id = 6,      Descripcion = "Tijeras",            Precio = 2 },
            new Producto { Id = 7,      Descripcion = "Cinta adhesiva",     Precio = 1.10f },
            new Producto { Id = 8,      Descripcion = "Rotulador",          Precio = 0.75f },
            new Producto { Id = 9,      Descripcion = "Mochila escolar",    Precio = 12.90f },
            new Producto { Id = 10,     Descripcion = "Pegamento barra",    Precio = 1.15f },
            new Producto { Id = 11,     Descripcion = "Lapicero",           Precio = 0.55f },
            new Producto { Id = 12,     Descripcion = "Grapas",             Precio = 0.90f }
        };

        private static List<Pedido> _listaPedidos = new List<Pedido>()
        {
            new Pedido { Id = 1,     IdCliente = 1,  FechaPedido = new DateTime(2013, 10, 1) },
            new Pedido { Id = 2,     IdCliente = 1,  FechaPedido = new DateTime(2013, 10, 8) },
            new Pedido { Id = 3,     IdCliente = 1,  FechaPedido = new DateTime(2013, 10, 12) },
            new Pedido { Id = 4,     IdCliente = 1,  FechaPedido = new DateTime(2013, 11, 5) },
            new Pedido { Id = 5,     IdCliente = 2,  FechaPedido = new DateTime(2013, 10, 4) },
            new Pedido { Id = 6,     IdCliente = 3,  FechaPedido = new DateTime(2013, 7, 9) },
            new Pedido { Id = 7,     IdCliente = 3,  FechaPedido = new DateTime(2013, 10, 1) },
            new Pedido { Id = 8,     IdCliente = 3,  FechaPedido = new DateTime(2013, 11, 8) },
            new Pedido { Id = 9,     IdCliente = 3,  FechaPedido = new DateTime(2013, 11, 22) },
            new Pedido { Id = 10,    IdCliente = 3,  FechaPedido = new DateTime(2013, 11, 29) },
            new Pedido { Id = 11,    IdCliente = 4,  FechaPedido = new DateTime(2013, 10, 19) },
            new Pedido { Id = 12,    IdCliente = 4,  FechaPedido = new DateTime(2013, 11, 11) }
        };

        private static List<LineaPedido> _listaLineasPedido = new List<LineaPedido>()
        {
            new LineaPedido() { Id = 1,  IdPedido = 1,  IdProducto = 1,     Cantidad = 9 },
            new LineaPedido() { Id = 2,  IdPedido = 1,  IdProducto = 3,     Cantidad = 7 },
            new LineaPedido() { Id = 3,  IdPedido = 1,  IdProducto = 5,     Cantidad = 2 },
            new LineaPedido() { Id = 4,  IdPedido = 1,  IdProducto = 7,     Cantidad = 2 },
            new LineaPedido() { Id = 5,  IdPedido = 2,  IdProducto = 9,     Cantidad = 1 },
            new LineaPedido() { Id = 6,  IdPedido = 2,  IdProducto = 11,    Cantidad = 15 },
            new LineaPedido() { Id = 7,  IdPedido = 3,  IdProducto = 12,    Cantidad = 2 },
            new LineaPedido() { Id = 8,  IdPedido = 3,  IdProducto = 1,     Cantidad = 4 },
            new LineaPedido() { Id = 9,  IdPedido = 4,  IdProducto = 2,     Cantidad = 3 },
            new LineaPedido() { Id = 10, IdPedido = 5,  IdProducto = 4,     Cantidad = 2 },
            new LineaPedido() { Id = 11, IdPedido = 6,  IdProducto = 1,     Cantidad = 20 },
            new LineaPedido() { Id = 12, IdPedido = 6,  IdProducto = 2,     Cantidad = 7 },
            new LineaPedido() { Id = 13, IdPedido = 7,  IdProducto = 1,     Cantidad = 4 },
            new LineaPedido() { Id = 14, IdPedido = 7,  IdProducto = 2,     Cantidad = 10 },
            new LineaPedido() { Id = 15, IdPedido = 7,  IdProducto = 11,    Cantidad = 2 },
            new LineaPedido() { Id = 16, IdPedido = 8,  IdProducto = 12,    Cantidad = 2 },
            new LineaPedido() { Id = 17, IdPedido = 8,  IdProducto = 3,     Cantidad = 9 },
            new LineaPedido() { Id = 18, IdPedido = 9,  IdProducto = 5,     Cantidad = 11 },
            new LineaPedido() { Id = 19, IdPedido = 9,  IdProducto = 6,     Cantidad = 9 },
            new LineaPedido() { Id = 20, IdPedido = 9,  IdProducto = 1,     Cantidad = 4 },
            new LineaPedido() { Id = 21, IdPedido = 10, IdProducto = 2,     Cantidad = 7 },
            new LineaPedido() { Id = 22, IdPedido = 10, IdProducto = 3,     Cantidad = 2 },
            new LineaPedido() { Id = 23, IdPedido = 10, IdProducto = 11,    Cantidad = 10 },
            new LineaPedido() { Id = 24, IdPedido = 11, IdProducto = 12,    Cantidad = 2 },
            new LineaPedido() { Id = 25, IdPedido = 12, IdProducto = 1,     Cantidad = 20 }
        };

        // Propiedades de los elementos privados
        public static List<Cliente> ListaClientes { get { return _listaClientes; } }
        public static List<Producto> ListaProductos { get { return _listaProductos; } }
        public static List<Pedido> ListaPedidos { get { return _listaPedidos; } }
        public static List<LineaPedido> ListaLineasPedido { get { return _listaLineasPedido; } }
    }
}