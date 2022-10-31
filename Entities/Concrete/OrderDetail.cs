using Core.Entities.Entity;

namespace Entities.Concrete
{
    public class OrderDetail :IEntity
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int UnitPrice { get; set; }
         
    }
}
