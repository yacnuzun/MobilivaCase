using Business.Abstract;
using Core.Utilities.Chaching.Redis.Abstract;
using Core.Utilities.MailService;
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
    public class OrderManager : IOrderService
    {
        IOrderDal _orderDal;
        IProductDal _productDal;
        IOrderDetailDal _detailDal;
        public IApiResponse<CreateOrderRequestDto> CreateOrder(CreateOrderRequestDto createOrder)
        {
            var result=ConverttoOrderDetail(createOrder);
            OrderDetailAdd(result);
            return new ApiResponse<CreateOrderRequestDto>(Statuses.Success, "", 0,createOrder);
        }

        private Order OrderAdd(Order order)
        {
            var result=_orderDal.Add(order);
            if (result == true)
            {
                return order;
            }
            else
                return null;
        }
        private void OrderDetailAdd(List<OrderDetail> orderDetailList)
        {
            foreach (var orderDetails in orderDetailList)
            {
                _detailDal.Add(orderDetails);
            }
            
        }
        private bool SendMail(EmailMessage email)
        {
            var result= MailSenderManager.SendMail(email);
            if (result == true)
            {
                Rabbit.SendQue(email);
                return true;
            }
            else
                return false;
        }
        
        private Order ConverttoOrder(CreateOrderRequestDto createOrder)
        {
        Order order= new Order();
            order.CustomerName=createOrder.CustomerName;
            order.CustomerEmail=createOrder.CustomerEmail;
            order.CustomerGSM=createOrder.CustomerGSM;
            order.TotalAmount=TotalAmount(createOrder.ProductDetail);
            return order;
        }

        private int TotalAmount(List<ProductDetail> products)
        {
            int total=0;
            for (int i = 0; i < products.Count; i++)
            {
                total = products[i].UnitPrice;
            }
            return total;
        }

        private List<OrderDetail> ConverttoOrderDetail(CreateOrderRequestDto createOrder)
        { 
        List<OrderDetail> listResult= new List<OrderDetail>();
            OrderDetail result=new OrderDetail();
            for (int i = 0; i < createOrder.ProductDetail.Count; i++)
            {
                result.ProductId=createOrder.ProductDetail[i].ProductId;
                var order=ConverttoOrder(createOrder);
                var rOrder=OrderAdd(order);
                var rResult = _orderDal.Get(o => o.Id == rOrder.Id);
                result.OrderId = rResult.Id;
                result.UnitPrice = createOrder.ProductDetail[i].UnitPrice;
                listResult.Add(result);
            }
            return listResult;
        }
    }
}
