using System;
using System.Collections.Generic;

namespace Trello_back.Models;

public partial class Card
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? IdList { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual List? IdListNavigation { get; set; }
}
