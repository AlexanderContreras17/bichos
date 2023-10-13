using bichos.Models.Entities;
using bichos.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bichos.Controllers
{
	public class PeliculasController : Controller
	{
		public IActionResult Index()
		{
			PixarContext context = new();
			var datos = context.Pelicula.OrderBy(x => x.Nombre).Select(x => new IndexPeliculasViewModel
			{
				Id = x.Id,
				Nombre = x.Nombre??""
			}); 
			return View(datos);
		}
		[Route("/Peliculas/{Id}")]

		public IActionResult Detalles(string Id) 
		{
			Id = Id.Replace("-", " ");
			PixarContext context = new();

			var datos = context.Pelicula.Include(x=>x.Apariciones).ThenInclude(x=>x.IdPersonajeNavigation).FirstOrDefault(x => x.Nombre == Id);
			if (datos == null)
			{
				return RedirectToAction("Index");
			}
			else
			{
			DetallesPeliculasViewModel vm = new()

				{
					Id = datos.Id,
					Descripcion = datos.Descripción ?? "No Disponible",
					Nombres = datos.Nombre ?? "Sin Nombre",
					Fecha = datos.FechaEstreno.HasValue ? datos.FechaEstreno.Value.ToDateTime(TimeOnly.MinValue) : DateTime.MinValue,
					//el FechaEstreno.HasValue es igual a un FechaEstreno !=null
					NombreOriginal = datos.NombreOriginal ?? "No Disponible",
					Personajes=datos.Apariciones.Select(x=> new PersonajeModel { Id = x.IdPersonaje, Nombre = x.IdPeliculaNavigation.Nombre??"Sin Nombre"})
				
				};
			return View(vm);
			}
		}
	}
}
