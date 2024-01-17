using System;
using System.Collections.Generic;

namespace Trello_back.Models;

public partial class Comment
{
    public int Id { get; set; }

    public string? Content { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? IdCard { get; set; }

    public int? IdUser { get; set; }

    public virtual Card? IdCardNavigation { get; set; }

    public virtual User? IdUserNavigation { get; set; }
}
