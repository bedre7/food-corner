using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace FoodCorner.Models
{
    public class OrderItem
    {
        public int orderID { get; set; }
        public string imageUrl { get; set; }
        public string orderName { get; set; }
        public int price { get; set; }
    }
}
