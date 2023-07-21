using System.ComponentModel.DataAnnotations.Schema;

namespace Finance.Entities.Entities;

[Table("Category")]
public class Category : Base
{
    [ForeignKey("FinancialSystem")]
    [Column(Order = 1)]
    public int IdSystem { get; set; }
    //public virtual FinancialSystem FinancialSystem { get; set; }
}
