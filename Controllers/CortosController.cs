using bichos.Models.Entities;
using bichos.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bichos.Controllers
{
    public class CortosController : Controller
    {
        public IActionResult Index()
        {
            PixarContext context = new();
            var datos = context.Categoria.Include(x => x.Cortometraje).OrderBy(x => x.Nombre)
                .Select(x => new IndexCortosViewModel()
                {
                    Categoria = x.Nombre??"Sin Nombre",
                    cortos= x.Cortometraje.Select(c=>
                    new CortoModel()
                    {
                    Id= c.Id,
                    Nombre=c.Nombre??"Sin Nombre"
                    })
                }) ;
            return View(datos);
        }
        [Route("/Cortos/{nombre}")] //route sirve para recuperar el endpoint (literal, como ingresar al IActionResult de abajo)
        //bichos/Cortos/cars   = ejemplo
        public IActionResult Detalles(string nombre)
        {
            nombre = nombre.Replace("_", "-");
            PixarContext context = new();
            var corto = context.Cortometraje.Where(x => x.Nombre == nombre)
                .Select(x => new DetallesCortosViewModel()
                {
                    Id = x.Id,
                    Nombre = x.Nombre ?? "Sin Nombre",
                    Descripcion = x.Descripcion ?? "Sin Descripcion"
                }).FirstOrDefault();
            if (corto == null)
            {
                return RedirectToAction("Index");
            }

            return View(corto);
        }
    }
}
