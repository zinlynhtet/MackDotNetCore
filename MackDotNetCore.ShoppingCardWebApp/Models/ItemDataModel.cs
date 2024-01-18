using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MackDotNetCore.ShoppingCardWebApp.Models
{
    [Table("Tbl_item")]
    public class ItemDataModel
    {
        [Key]
        [Column("ItemId")]
        public int? ItemId { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }
    }
}
