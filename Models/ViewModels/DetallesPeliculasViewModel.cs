namespace bichos.Models.ViewModels
{
    public class DetallesPeliculasViewModel
    {
        public int Id { get; set; }
        public string Nombres { get; set; } = null!;
        public string NombreOriginal {  get; set; }= null!;
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; } = null!;
        public IEnumerable<PersonajeModel> Personajes { get; set; } = null!;
    }

    public class PersonajeModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
    }
}
