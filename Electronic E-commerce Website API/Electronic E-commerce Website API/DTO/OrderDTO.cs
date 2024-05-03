using Electronic_E_commerce_Website_API.Models;

namespace Electronic_E_commerce_Website_API.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Status { get; set; }

        public decimal TotalAmount { get; set; }

        public DateTime OrderDate { get; set; }


    }
}

