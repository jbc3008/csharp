using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using Formacion.CSharp.Data.Models;
using Newtonsoft.Json;

namespace Formacion.CSharp.ConsoleAppAPIClient
{
    class Program
    {
        static readonly HttpClient http = new HttpClient();
        static readonly string url = "http://api.labs.com.es/v1.0/";

        static void Main(string[] args)
        {
            Ejercicio3DELETE();
        }

        static void HttpClientWithDynamic()
        {
            http.BaseAddress = new Uri(url);

            var response = http.GetAsync("clientes.ashx?all").Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine("Respuesta JSON: {0}", content);

                var clientes = JsonConvert.DeserializeObject<List<dynamic>>(content);

                foreach (var c in clientes)
                {
                    Console.WriteLine($"{c.CustomerID}# {c.CompanyName} - {c.Country}");
                }
            }
            else Console.WriteLine("Error: {0}", response.StatusCode.ToString());
        }

        static void HttpClientWithCustomers()
        {
            http.BaseAddress = new Uri(url);

            var response = http.GetAsync("clientes.ashx?all").Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {                
                var content = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine("Respuesta JSON: {0}", content);

                var clientes = JsonConvert.DeserializeObject<List<Customers>>(content);

                foreach (var c in clientes)
                {
                    Console.WriteLine($"{c.CustomerID}# {c.CompanyName} - {c.Country}");
                }
            }
            else Console.WriteLine("Error: {0}", response.StatusCode.ToString());
        }

        static void HttpClientDemo()
        {
            http.BaseAddress = new Uri(url);
            var clientes = http.GetFromJsonAsync<List<Customers>>("clientes.ashx?all").Result;

            foreach (var c in clientes)
            {
                Console.WriteLine($"{c.CustomerID}# {c.CompanyName} - {c.Country}");
            }
        }

        static void HttpClientWithHeaders()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "http://api.labs.com.es/v1.0/clientes.ashx?all");

            request.Headers.Clear();
            request.Headers.Add("ContentType", "application/json");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Add("User-Agent", "ConsoleApp for Nortwind");
            request.Headers.Add("Authorization", "key of access");

            var response = http.SendAsync(request).Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine("Respuesta JSON: {0}", content);

                var clientes = JsonConvert.DeserializeObject<List<Customers>>(content);

                foreach (var c in clientes)
                {
                    Console.WriteLine($"{c.CustomerID}# {c.CompanyName} - {c.Country}");
                }
            }
            else Console.WriteLine("Error: {0}", response.StatusCode.ToString());
        }

        static void HttpClientWithHeaders2()
        {
            http.BaseAddress = new Uri(url);

            http.DefaultRequestHeaders.Clear();
            http.DefaultRequestHeaders.Add("ContentType", "application/json");
            http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            http.DefaultRequestHeaders.Add("User-Agent", "ConsoleApp for Nortwind");            
            http.DefaultRequestHeaders.Add("Authorization", "key of access");

            var response = http.GetAsync("clientes.ashx?all").Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine("Respuesta JSON: {0}", content);

                var clientes = JsonConvert.DeserializeObject<List<Customers>>(content);

                foreach (var c in clientes)
                {
                    Console.WriteLine($"{c.CustomerID}# {c.CompanyName} - {c.Country}");
                }
            }
            else Console.WriteLine("Error: {0}", response.StatusCode.ToString());
        }

        static void HttpClientPost()
        {
            http.BaseAddress = new Uri("http://postman-echo.com/");

            var region = new Region() { RegionID = 10, RegionDescription = "Comunidad de Madrid" };

            var regionJSON = JsonConvert.SerializeObject(region);
            Console.WriteLine($"Región en JSON: {regionJSON}");

            var content = new StringContent(regionJSON, System.Text.Encoding.UTF8, "application/json");

            var response = http.PostAsync("post", content).Result;
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine($"Respuesta: {responseContent}");
            }
            else Console.WriteLine("Error: {0}", response.StatusCode.ToString());
        }

        static void Ejercicio1()
        {
            http.BaseAddress = new Uri("http://ip-api.com/json/");

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Dirección IP: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            var ip = Console.ReadLine();

            var response = http.GetAsync(ip).Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var data = JsonConvert.DeserializeObject<dynamic>(response.Content.ReadAsStringAsync().Result);
                Console.WriteLine("");
                
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Proveedor:   ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(data.isp);

                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Propietario: ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(data["as"]);

                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Región:      ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(data.regionName);
                Console.WriteLine("             {0} ({1})", data.city, data.country);
            }
            else Console.WriteLine("Error: {0}", response.StatusCode.ToString());

        }

        static void Ejercicio2()
        {
            http.BaseAddress = new Uri("https://localhost:44334/api/v1.0/");

            Console.Clear();
            Console.Write("ID Empleado: ");
            var id = Console.ReadLine();

            var response = http.GetAsync($"empleados.ashx?id={id}").Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var contentType = response.Content.Headers.ContentType.MediaType;
                var content = response.Content.ReadAsStringAsync().Result;

                if (contentType == "text/plain")
                {
                    Console.WriteLine(content);
                }
                else if (contentType == "application/json")
                {
                    var empleado = JsonConvert.DeserializeObject<Employees>(content);
                    Console.WriteLine($"{empleado.FirstName} {empleado.LastName}");
                    Console.WriteLine($"{empleado.Title}");
                    Console.WriteLine($"{empleado.City} {empleado.Country}");
                }
            }
            else Console.WriteLine("Error: {0}", response.StatusCode.ToString());
        }

        static void Ejercicio3GET()
        {
            HttpResponseMessage response;
            http.BaseAddress = new Uri("https://localhost:44366/api/");

            //Mostar productos por id o listado introduciendo all
            Console.Clear();
            Console.Write("ID Producto: ");
            var id = Console.ReadLine();

            switch (id.ToLower())
            {
                case "all":
                    response = http.GetAsync("products").Result;
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var productos = JsonConvert.DeserializeObject<List<Products>>(response.Content.ReadAsStringAsync().Result);

                        Console.Clear();
                        foreach (var p in productos) Console.WriteLine($"{p.ProductID}# {p.ProductName} {p.UnitPrice}");
                    }
                    else Console.WriteLine($"Error: {response.StatusCode.ToString()}");                                      
                    break;
                default:
                    response = http.GetAsync($"products/{id}").Result;
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var producto = JsonConvert.DeserializeObject<Products>(response.Content.ReadAsStringAsync().Result);

                        Console.Clear();
                        Console.WriteLine($"{producto.ProductID}# {producto.ProductName} {producto.UnitPrice}");
                    }
                    else Console.WriteLine($"Error: {response.StatusCode.ToString()}");
                    break;
            }

        }

        static void Ejercicio3POST()
        {
            Products producto = new Products();
            HttpResponseMessage response;
            http.BaseAddress = new Uri("https://localhost:44366/api/");

            //Insertar un nuevo Producto
            Console.Clear();
            Console.Write("Nombre: ");
            producto.ProductName = Console.ReadLine();
            Console.Write("Precio: ");
            producto.UnitPrice = Convert.ToDecimal(Console.ReadLine());
            Console.Write("Stock: ");
            producto.UnitsInStock = Convert.ToInt16(Console.ReadLine());
            producto.SupplierID = 1;
            producto.CategoryID = 1;
            producto.QuantityPerUnit = "1";
            producto.UnitsOnOrder = 0;
            producto.Discontinued = false;
            producto.ReorderLevel = 0;

            var content = new StringContent(JsonConvert.SerializeObject(producto), System.Text.Encoding.UTF8, "application/json");

            response = http.PostAsync("products", content).Result;
            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                var data = JsonConvert.DeserializeObject<Products>(response.Content.ReadAsStringAsync().Result);
                Console.WriteLine($"Producto creado correctamente con identificador {data.ProductID}.");
            }
            else Console.WriteLine("Error: {0}", response.StatusCode.ToString());
        }

        static void Ejercicio3PUT()
        {
            HttpResponseMessage response;
            http.BaseAddress = new Uri("https://localhost:44366/api/");

            //Modificar un nuevo Producto
            var producto = http.GetFromJsonAsync<Products>("products/79").Result;
            producto.ProductName = "Nata para cocinar ligera";
            producto.UnitPrice = 2.95M;

            var content = new StringContent(JsonConvert.SerializeObject(producto), System.Text.Encoding.UTF8, "application/json");

            response = http.PutAsync("products/79", content).Result;
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {                
                Console.WriteLine($"Producto modificado correctamente.");
            }
            else Console.WriteLine("Error: {0}", response.StatusCode.ToString());
        }

        static void Ejercicio3DELETE()
        {
            HttpResponseMessage response;
            http.BaseAddress = new Uri("https://localhost:44366/api/");
            
            Console.Clear();
            Console.Write("ID Producto: ");
            var id = Console.ReadLine();

            var producto = http.GetFromJsonAsync<Products>($"products/{id}").Result;
            
            Console.WriteLine($"¿Desea eliminar el producto {producto.ProductName}? Si/No");
            if (Console.ReadLine().ToLower() == "si")
            {
                response = http.DeleteAsync($"products/{id}").Result;
                if(response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Console.WriteLine($"Producto eliminado correctamente.");
                }
                else Console.WriteLine("Error: {0}", response.StatusCode.ToString());
            }
        }
    }
}
