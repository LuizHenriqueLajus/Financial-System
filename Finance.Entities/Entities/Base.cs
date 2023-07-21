using Finance.Entities.Notifications;
using System.ComponentModel.DataAnnotations;

namespace Finance.Entities.Entities;

public class Base : Notifica
{
    [Display(Name = "UserId")]
    public int Id { get; set; }

    [Display(Name = "Name")]
    public string Name { get; set; }
}
