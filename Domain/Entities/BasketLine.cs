namespace Domain.Entities
{
    public class BasketLine
    {
        public Product Product { get; set; }

        public int Quantity { get; set; }

        public string Size { get; set; }
    }
}
