namespace FoodCorner.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public int UserID { get; set; }
        public int FoodID { get; set; }
        public Food OrderedItem { get; set; }
        public User OrderedBy { get; set; }
    }
}
