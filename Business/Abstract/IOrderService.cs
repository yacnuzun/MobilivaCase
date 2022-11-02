using Core.Utilities.Result;
using Entities.Dto_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IOrderService
    {
        IApiResponse<CreateOrderRequestDto> CreateOrder(CreateOrderRequestDto createOrder);
    }
}
