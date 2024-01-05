using System;
using System.Collections.Generic;

namespace Trello_back.Models;

public partial class Project
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<List> Lists { get; set; } = new List<List>();
}
