using Business.Abstract;
using Core.Utilities.Chaching.Redis.Abstract;
using Core.Utilities.RabitMQ;
using Core.Utilities.Result;
using DataAccess.Abstarct;
using Entities.Concrete;
using Entities.Dto_s;
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

        public IApiResponse<List<ProductDto>> GetProducts(string? category)
        {
            if (category==null)
            {
                return GetProductsforNull();
            }
            
            else
                return GetProductforCategory(category);
        }

        public IApiResponse<Product> Update(Product product)
        {
            throw new NotImplementedException();
        }

        private IApiResponse<List<ProductDto>> GetProductforCategory(string category)
        {
            var result = _cacheManager.Get<List<ProductDto>>("products");
            if (result != null)
                return new ApiResponse<List<ProductDto>>(Statuses.Success, "Getirildi.", 200, result);
            else
            {
                var data = _productDal.GetProducts(p=>p.Category==category);
                _cacheManager.Set<List<ProductDto>>("products", data);
                return new ApiResponse<List<ProductDto>>(Statuses.Success, "Getirildi.", 200, data);
            }
        }
        private IApiResponse<List<ProductDto>> GetProductsforNull()
        {
            var result = _cacheManager.Get<List<ProductDto>>("products");
            if (result != null)
                return new ApiResponse<List<ProductDto>>(Statuses.Success, "Getirildi.", 200, result);
            else
            {
                var data = _productDal.GetProducts();
                _cacheManager.Set<List<ProductDto>>("products", data);
                return new ApiResponse<List<ProductDto>>(Statuses.Success, "Getirildi.", 200, data);
            }
        }
    }
}
