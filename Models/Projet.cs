using System;
using System.Collections.Generic;

namespace Trello_back.Models;

public partial class Projet
{
    public int Id { get; set; }

    public string? Nom { get; set; }

    public string? Description { get; set; }

    public DateTime? DateCreation { get; set; }

    public virtual ICollection<Liste> Listes { get; set; } = new List<Liste>();
}
