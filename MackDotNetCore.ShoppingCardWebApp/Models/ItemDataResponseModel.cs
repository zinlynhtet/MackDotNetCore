namespace MackDotNetCore.ShoppingCardWebApp.Models
{
    public class ItemDataResponseModel
    {
        public int? ItemId { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public List<ItemDataModel> Data { get; set; }
    }
}
