using Finance.Entities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finance.Entities.Entities;

[Table("Expense")]
public class Expense : Base
{
    public decimal Value { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }

    public EnumExpenseType ExpenseType { get; set; }

    public DateTime DataRegistration { get; set; }

    public DateTime DataAlteration { get; set; }

    public DateTime DataPayment { get; set; }

    public DateTime DataDue { get; set; }

    public bool Paid { get; set; }

    public bool DeferredExpenditure { get; set; } //pagamentoatrasado

    [ForeignKey("Category")]
    [Column(Order = 1)]
    public int IdCategory { get; set; }
    //public virtual Category Category { get; set; } //DEPOIS DE RODAR A MIGRATION COMENTAR ESTA LINHA
}
