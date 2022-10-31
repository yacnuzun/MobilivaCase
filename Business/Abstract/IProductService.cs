using Core.Utilities.Result;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IProductService
    {
        IApiResponse<List<Product>> GetAll();
        IApiResponse<Product> GetById(int id);
        IApiResponse<List<Product>> GetProduct(Product product);

        IApiResponse<Product> Add(Product product);
        IApiResponse<Product> Update(Product product);
        IApiResponse<Product> Delete(Product product);
    }
}
