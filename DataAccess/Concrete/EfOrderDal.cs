using Core.DataAccess.EntityFramework;
using DataAccess.Abstarct;
using DataAccess.Context;
using Entities.Concrete;
using Entities.Dto_s;

namespace DataAccess.Concrete
{
    public class EfOrderDal : EfEntityRepositoryBase<MyContext, Order>, IOrderDal
    {
        public void CreateOrder(CreateOrderRequestDto createOrder)
        {
            for (int i = 0; i < createOrder.ProductDetail.Count; i++)
            {

            }
            
        }
    }
}
