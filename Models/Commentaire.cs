using System;
using System.Collections.Generic;

namespace Trello_back.Models;

public partial class Commentaire
{
    public int Id { get; set; }

    public string? Contenu { get; set; }

    public DateTime? DateCreation { get; set; }

    public int? IdCarte { get; set; }

    public string? Utilisateur { get; set; }

    public virtual Carte? IdCarteNavigation { get; set; }
}
