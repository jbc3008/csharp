using Formacion.CSharp.MicroServicioNorthwind.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Formacion.CSharp.MicroServicioNorthwind.api
{
    /// <summary>
    /// Descripción breve de Empleados
    /// </summary>
    public class Empleados : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                //Parámetro id, que determina el id del empleado
                int id = Convert.ToInt32(context.Request.Params["id"]);

                var db = new ModelNorthwind();
                db.Configuration.LazyLoadingEnabled = false;

                var empleado = db.Employees
                    .Where(r => r.EmployeeID == id)
                    .FirstOrDefault();

                if (empleado == null)
                {
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("Empleado no encontrado");
                    context.Response.StatusCode = 200;
                }
                else
                {
                    context.Response.ContentType = "application/json";
                    context.Response.Write(JsonConvert.SerializeObject(empleado));
                    context.Response.StatusCode = 200;
                }
            }
            catch (Exception e)
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write(e.Message);
                context.Response.StatusCode = 500;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}