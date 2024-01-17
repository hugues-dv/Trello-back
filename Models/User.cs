using System;
using System.Collections.Generic;

namespace Trello_back.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
}
