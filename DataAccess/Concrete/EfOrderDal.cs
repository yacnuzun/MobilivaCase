using Core.DataAccess.EntityFramework;
using DataAccess.Context;
using Entities.Concrete;

namespace DataAccess.Concrete
{
    public class EfOrderDal : EfEntityRepositoryBase<MyContext, Order>
    {
    }
}
