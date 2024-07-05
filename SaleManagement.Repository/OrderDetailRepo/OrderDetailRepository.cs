using Microsoft.EntityFrameworkCore;
using SaleManagement.BusinessObject.Entity;

namespace SaleManagement.Repository.OrderDetailRepo
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly FStoreContext _context;

        public OrderDetailRepository(FStoreContext context)
        {
            _context = context;
        }

        public OrderDetailRepository()
        {
            _context = new FStoreContext();
        }

        public async Task<IEnumerable<OrderDetail>> GetOrderDetail(int orderId)
        {
            return await _context.OrderDetails.Where(o => o.OrderId == orderId).ToListAsync();
        }

        public async Task<IEnumerable<OrderDetail>> GetOrderDetailById(int userId)
        {
            return await _context.OrderDetails
                .Include(o => o.Order)
                .Where(o => o.Order.MemberId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<OrderDetail>> GetOrderDetails()
        {
            return await _context.OrderDetails.ToListAsync();
        }

        public async Task<bool> CreateOrderDetails(IEnumerable<OrderDetail> orderDetails, int orderId, int discount)
        {
            var newList = new List<OrderDetail>();
            foreach (var item in orderDetails)
            {
                var orderDetail = new OrderDetail
                {
                    OrderId = orderId,
                    ProductId = item.Product.ProductId,
                    Quantity = item.Quantity,
                    Discount = discount,
                    UnitPrice = item.Product.UnitPrice
                };

                newList.Add(orderDetail);
            }

            await _context.OrderDetails.AddRangeAsync(newList);
            return await _context.SaveChangesAsync() > 0;
        }


    }
}
