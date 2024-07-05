using SaleManagement.BusinessObject.Entity;

namespace SaleManagement.Repository.OrderRepo
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetOrderByMember(int memberId);
        Task<IEnumerable<Order>> GetAllOrder();
        Task<bool> CreateOrder(Order order);
        Task<bool> UpdateOrder(Order order);
        Task<bool> DeleteOrder(int id);
        Task<int> GetOrderId(int memberId);
    }
}
