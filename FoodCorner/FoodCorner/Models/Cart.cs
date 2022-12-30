namespace FoodCorner.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public List<Order> Orders { get; set; }
    }
}
