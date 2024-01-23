using System;
using System.Collections.Generic;

namespace Trello_back.Models;

public partial class List
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? IdProject { get; set; }

    public virtual ICollection<Card> Cards { get; set; } = new List<Card>();

    public virtual Project? IdProjectNavigation { get; set; }
}
