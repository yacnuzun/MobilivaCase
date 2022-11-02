using Core.Entities.Dto_s;

namespace Entities.Dto_s
{
    public class OrderDetailDto : IDto
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string Description { get; set; }
        public int UnitPrice { get; set; }
    }
}
