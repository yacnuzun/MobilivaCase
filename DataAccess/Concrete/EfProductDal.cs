using Core.DataAccess.EntityFramework;
using DataAccess.Abstarct;
using DataAccess.Context;
using Entities.Concrete;
using Entities.Dto_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class EfProductDal:EfEntityRepositoryBase<MyContext,Product>,IProductDal
    {

        public List<ProductDto> GetProducts(Expression<Func<ProductDto, bool>> filter = null)
        {
            using (MyContext context = new MyContext())
            {
                var result = from p in context.Products

                             select new ProductDto
                             {
                                 Id = p.Id,
                                 Description = p.Description,
                                 Category = p.Category,
                                 Unit = p.Unit,
                                 UnitPrice = p.UnitPrice,
                             };
                return filter == null ? result.ToList() : result.Where(filter).ToList();
            }
        }
    }
}
