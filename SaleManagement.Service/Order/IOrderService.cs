using SaleManagement.BusinessObject.Entity;

namespace SaleManagement.Service
{
    public interface IOrderService
    {
        Task<bool> AddOrder(IEnumerable<OrderDetail> orderDetails, int memberId, int shipping);
        Task<int> GetOrderId(int memberId);
        Task<IEnumerable<Order>> GetOrders();
        Task<IEnumerable<Order>> GetOrdersByMember(int memberId);
    }
}
