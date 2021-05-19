using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Formacion.CSharp.WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(int id)
        {
            //Traspasamos información a la vista utilizada ViewBag
            ViewBag.numero = id;
            ViewBag.mensaje = $"Tabla de Multiplicar del {id}";


            //Trapasamos información como modelo de datos
            return View(id);
        }

        public IActionResult Demo()
        {
            return View();
        }
    }
}
