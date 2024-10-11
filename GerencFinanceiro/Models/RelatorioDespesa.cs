using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GerencFinanceiro.Models
{
    public class RelatorioDespesa
    {
        [Key]
        public int ItemId { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Despesa")]
        public string ItemNome { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "numeric(10,2)")]  
        public decimal Valor { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime Date { get; set; } = DateTime.Now;

        [Required]
        [StringLength(100)]
        public string Categoria { get; set; }
    }
}
