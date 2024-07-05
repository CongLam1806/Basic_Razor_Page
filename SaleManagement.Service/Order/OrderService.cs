using SaleManagement.BusinessObject.Entity;
using SaleManagement.Repository.OrderRepo;

namespace SaleManagement.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<bool> AddOrder(IEnumerable<OrderDetail> orderDetails, int memberId, int shipping)
        {
            var order = new Order
            {
                MemberId = memberId,
                OrderDate = DateTime.Now,
                Freight = shipping,
                RequiredDate = DateTime.Now,
                ShippedDate = DateTime.Now + TimeSpan.FromDays(5)
            };

            return await _orderRepository.CreateOrder(order);


            

        }

        public async Task<int> GetOrderId(int memberId)
        {
            return await _orderRepository.GetOrderId(memberId);
        }

        public async Task<IEnumerable<Order>> GetOrders()
        {
            return await _orderRepository.GetAllOrder();
        }

        public async Task<IEnumerable<Order>> GetOrdersByMember(int memberId)
        {
            return await _orderRepository.GetOrderByMember(memberId);
        }

    }
}
