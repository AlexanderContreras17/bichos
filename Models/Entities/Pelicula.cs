using System;
using System.Collections.Generic;

namespace bichos.Models.Entities;

public partial class Pelicula
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? NombreOriginal { get; set; }

    public string? Descripción { get; set; }

    public DateOnly? FechaEstreno { get; set; }

    public virtual ICollection<Apariciones> Apariciones { get; set; } = new List<Apariciones>();
}
