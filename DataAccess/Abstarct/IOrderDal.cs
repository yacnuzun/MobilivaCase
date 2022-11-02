using Core.DataAccess;
using Entities.Concrete;
using Entities.Dto_s;

namespace DataAccess.Abstarct
{
    public interface IOrderDal : IEntityRepository<Order>
    {
        void CreateOrder(CreateOrderRequestDto createOrder);
    }
}
