using System;
using System.Collections.Generic;

namespace bichos.Models.Entities;

public partial class Categoria
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Cortometraje> Cortometraje { get; set; } = new List<Cortometraje>();
}
