using Business.Abstract;
using Core.Utilities.Chaching.Redis.Abstract;
using Core.Utilities.RabitMQ;
using Core.Utilities.Result;
using DataAccess.Abstarct;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        ICacheManager _cacheManager;

        public ProductManager(IProductDal productDal, ICacheManager cacheManager)
        {
            _productDal = productDal;
            _cacheManager = cacheManager;
        }

        public IApiResponse<Product> Add(Product product)
        {
            throw new NotImplementedException();
        }

        public IApiResponse<Product> Delete(Product product)
        {
            throw new NotImplementedException();
        }

        public IApiResponse<List<Product>> GetAll()
        {
            var result = _cacheManager.Get<List<Product>>("products");
            if (result!=null)
                return new ApiResponse<List<Product>>(Statuses.Success, "Getirildi.", 200, result);
            else
            {
                var data = _productDal.GetAll();
                _cacheManager.Set<List<Product>>("products", data);
                return new ApiResponse<List<Product>>(Statuses.Success, "Getirildi.", 200, data);
            }
        }

        public IApiResponse<Product> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IApiResponse<List<Product>> GetProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public IApiResponse<Product> Update(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
