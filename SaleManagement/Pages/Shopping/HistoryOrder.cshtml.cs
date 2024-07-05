using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SaleManagement.BusinessObject.Entity;
using SaleManagement.Service;
using SaleManagement.Utilities;

namespace SaleManagement.Pages.Shopping
{
    public class HistoryOrderModel : PageModel
    {
        private readonly IOrderService _orderService;

        [BindProperty]
        public IEnumerable<Order> Orders { get; set; } = new List<Order>();


        public HistoryOrderModel(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnGetHistoryOrderAdmin()
        {
            Orders = await _orderService.GetOrders();

            return Page();
        }

        public async Task<IActionResult> OnGetHistoryOrderUser()
        {
            var user = HttpContext.Session.GetCurrentUser<Member>();

            Orders = await _orderService.GetOrdersByMember(user.MemberId);

            return Page();
        }

    }
}
