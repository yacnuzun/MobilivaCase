using Core.Entities;
using Core.Entities.Dto_s;

namespace Entities.Concrete
{
    public class CreateOrderRequestDto:IDto
    {
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerGSM { get; set; }
        public List<ProductDetail> ProductDetail { get; set; }
    }
    public class ProductDetail : IDto
    {
        public int ProductId { get; set; }
        public int UnitPrice { get; set; }
        public int Amount { get; set; }
    }
}
