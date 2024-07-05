using SaleManagement.BusinessObject.Entity;
using SaleManagement.Repository.OrderDetailRepo;


namespace SaleManagement.Service
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IOrderDetailRepository _orderDetailRepository;

        public OrderDetailService(IOrderDetailRepository orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }

        public async Task<bool> AddOrder(IEnumerable<OrderDetail> orderDetails, int orderId, int discount)
        {
            return await _orderDetailRepository.CreateOrderDetails(orderDetails, orderId, discount);
        }
    }
}
