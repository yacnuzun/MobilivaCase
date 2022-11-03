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

        public OrderManager(IOrderDal orderDal, IProductDal productDal, IOrderDetailDal detailDal)
        {
            _orderDal = orderDal;
            _productDal = productDal;
            _detailDal = detailDal;
        }

        public IApiResponse<CreateOrderRequestDto> CreateOrder(CreateOrderRequestDto createOrder)
        {
            var result=ConverttoOrderDetail(createOrder);
            OrderDetailAdd(result);
            var emailMessage=ConvertMail(createOrder);
            SendMail(emailMessage);
            return new ApiResponse<CreateOrderRequestDto>(Statuses.Success, "", 0,createOrder);
        }

        private EmailMessage ConvertMail(CreateOrderRequestDto createOrderRequest)
        {
            EmailMessage email = new EmailMessage();
            email.Subject = "Sipariş Detayı";
            email.Body = "Siparişiniz Alınmıştır.";
            email.Contacts = createOrderRequest.CustomerEmail;
            return email;
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
        private void SendMail(EmailMessage email)
        {            
                Rabbit.SendQue(email);

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
