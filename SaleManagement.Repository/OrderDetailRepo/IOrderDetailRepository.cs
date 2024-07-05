using SaleManagement.BusinessObject.Entity;

namespace SaleManagement.Repository.OrderDetailRepo
{
    public interface IOrderDetailRepository
    {
        Task<IEnumerable<OrderDetail>> GetOrderDetail(int orderId);
        Task<IEnumerable<OrderDetail>> GetOrderDetails();
        Task<IEnumerable<OrderDetail>> GetOrderDetailById(int userId);
        Task<bool> CreateOrderDetails(IEnumerable<OrderDetail> orderDetails, int orderId, int discount);
    }
}
