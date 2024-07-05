using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SaleManagement.BusinessObject.Constants;
using SaleManagement.BusinessObject.DataTransfer;
using SaleManagement.BusinessObject.Entity;
using SaleManagement.Service;
using SaleManagement.Utilities;
using ShoppingCart.BusinessObject.Constants;

namespace SaleManagement.Pages.Shopping
{
    public class CheckOutModel : PageModel
    {
        private readonly IOrderService _orderService;
        private readonly IOrderDetailService _orderDetailService;

        public CheckOutModel(IOrderService orderService, IOrderDetailService orderDetailService)
        {
            _orderService = orderService;
            _orderDetailService = orderDetailService;
        }


        public void OnGet()
        {

        }

        public void OnPostApplyShipping(int shippingId)
        {
            var shipping = Data.Shippings.FirstOrDefault(s => s.Id == shippingId);

            if (shipping == null)
            {
                return;
            }

            HttpContext.Session.SetSession(SessionConstant.Shipping, shipping);
        }

        public void OnPostApplyVoucher(string code)
        {

            var discount = Data.Discounts.FirstOrDefault(d => d.Name.Equals(code, StringComparison.OrdinalIgnoreCase))?.Value;

            if(discount == null)
            {
                TempData["Message"] = "Invalid Voucher Code";
                return;
            }

            HttpContext.Session.SetSession(SessionConstant.Discount, discount);

        }

        public void OnPostDelete(int productId)
        {
            HttpContext.Session.DeleteCart(productId);
        }

        public void OnPostDownUnit(int productId)
        {
            HttpContext.Session.DescreaseUnit(productId);

        }

        public void OnPostUpUnit(int productId)
        {
            HttpContext.Session.IncreaseUnit(productId);
        }

        public async Task<IActionResult> OnGetCheckOut()
        {
            var carts = HttpContext.Session.GetSession<HashSet<OrderDetail>>(SessionConstant.CartSession);
            var discount = HttpContext.Session.GetSession<int>(SessionConstant.Discount);

            if (carts == null || !carts!.Any())
            {
                return Page();
            }

            var shipping = HttpContext.Session.GetSession<ObjectData>(SessionConstant.Shipping);
            var user = HttpContext.Session.GetCurrentUser<Member>();

            var result = await _orderService.AddOrder(carts!, user.MemberId, shipping!.Value);
           
            if (!result)
            {
                TempData["MessageCheckout"] = "Checkout failed!";
                return Page();
            }

            var orderId = await _orderService.GetOrderId(user.MemberId);
            result = await _orderDetailService.AddOrder(carts!, orderId, discount);

            if (!result)
            {
                TempData["MessageCheckout"] = "Checkout failed!";
                return Page();
            }

            TempData["MessageCheckout"] = "Checkout successfully!";
            return Redirect("/");

        }

    }
}
