namespace api.DTOs
{
    public class AddToCartDto
    {
        public int productId { get; set; }
        public int quantity { get; set; }
        public int userId { get; set; }
    }
}
