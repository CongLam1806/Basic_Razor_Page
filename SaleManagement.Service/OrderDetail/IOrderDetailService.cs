using SaleManagement.BusinessObject.Entity;

namespace SaleManagement.Service
{
    public interface IOrderDetailService
    {
        Task<bool> AddOrder(IEnumerable<OrderDetail> orderDetails, int orderId, int discount);
    }
}
