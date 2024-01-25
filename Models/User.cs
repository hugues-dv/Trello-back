using System;
using System.Collections.Generic;

namespace Trello_back.Models;

public partial class User
{
    public string Username { get; set; } = null!;

    public string? Password { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
}
