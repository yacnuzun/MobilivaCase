using Core.DataAccess;
using DataAccess.Context;
using Entities.Concrete;
using Entities.Dto_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstarct
{
    public interface IProductDal:IEntityRepository<Product>
    {
        public List<ProductDto> GetProducts(Expression<Func<ProductDto, bool>> filter = null);
    }
}
