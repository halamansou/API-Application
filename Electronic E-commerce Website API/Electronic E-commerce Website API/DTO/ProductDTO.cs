namespace Electronic_E_commerce_Website_API.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }

        public int Quantity { get; set; }
        public decimal Rate { get; set; }

    }
}
