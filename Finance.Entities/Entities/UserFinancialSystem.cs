using System.ComponentModel.DataAnnotations.Schema;

namespace Finance.Entities.Entities;

[Table("UserFinancialSystem")]
public class UserFinancialSystem 
{
    public int Id { get; set; }
    public string UserEmail { get; set; }
    public bool Admin { get; set; }
    public bool CurrentSystem { get; set; }


    [ForeignKey("FinancialSystem")]
    [Column(Order = 1)]
    public int IdSystem { get; set; }
    public virtual FinancialSystem FinancialSystem { get; set; }
}
