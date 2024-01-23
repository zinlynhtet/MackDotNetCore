using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MackDotNetCore.ShoppingCardWebApp.Models
{
    [Table("Tbl_item")]
    public class ItemDataModel
    {
        [Key]
        [Column("ItemId")]
        public int ItemId { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
    }

    public class AddToCartRequestModel
    {
        public int ItemId { get; set; } 
    }

    public class AddToCardListModel
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
