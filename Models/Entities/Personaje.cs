using System;
using System.Collections.Generic;

namespace bichos.Models.Entities;

public partial class Personaje
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Apariciones> Apariciones { get; set; } = new List<Apariciones>();
}
