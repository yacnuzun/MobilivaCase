using Business.Abstract;
using Core.Utilities.Chaching.Redis.Abstract;
using DataAccess.Abstarct;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("b")]
        public IActionResult GetAll()
        {
            var result= _productService.GetAll();
            if (result.ErrorCode==(int)StatusCodes.Status200OK)
                return Ok(result);
            else
                return BadRequest();
        }
        [HttpGet("c")]
        public IActionResult GetProducts(string? category)
        {
            var result=_productService.GetProducts(category);
            if (result.ErrorCode == (int)StatusCodes.Status200OK)
                return Ok(result);
            else
                return BadRequest();
        }
    }
}