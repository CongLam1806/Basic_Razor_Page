using Microsoft.EntityFrameworkCore;
using SaleManagement.BusinessObject.Entity;
using SaleManagement.Repository.OrderRepo;

namespace SaleManagement.Repository.OrderRepo
{
    public class OrderRepository : IOrderRepository
    {
        private readonly FStoreContext _context;

        public OrderRepository(FStoreContext context)
        {
            _context = context;
        }

        public OrderRepository()
        {
            _context = new FStoreContext();
        }

        public async Task<bool> CreateOrder(Order order)
        {
            await _context.Orders.AddAsync(order);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteOrder(int id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == id);

            if(order == null)
            {
                return false;
            }

            _context.Orders.Remove(order);

            return await _context.SaveChangesAsync() > 0;

        }

        public async Task<IEnumerable<Order>> GetAllOrder()
        {
            return await _context.Orders
                .Include(o => o.Member)
                .Include(o => o.OrderDetails)
                .OrderByDescending(o => o.OrderDate).ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrderByMember(int memberId)
        {
            return await _context.Orders
                .Include(o => o.Member)
                .Include(o => o.OrderDetails)
                .Where(o => o.MemberId == memberId).OrderByDescending(o => o.OrderDate).ToListAsync();
        }

        public async Task<int> GetOrderId(int memberId)
        {
            var order = await _context.Orders.Where(o => o.MemberId == memberId).OrderByDescending(o => o.OrderDate).FirstOrDefaultAsync();

            return order!.OrderId;
        }

        public async Task<bool> UpdateOrder(Order order)
        {
            var existingOrder = await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == order.OrderId);

            if(existingOrder == null)
            {
                return false;
            }

            _context.Orders.Update(order);
            return await _context.SaveChangesAsync() > 0;

        }
    }
}
