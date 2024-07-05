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
    public class ShopModel : PageModel
    {
        private readonly IProductService _productService;

        public ShopModel(IProductService productService)
        {
            _productService = productService;
        }

        [BindProperty]
        public IEnumerable<Product> Products { get; set; } = new List<Product>();

        [BindProperty]
        public Product Product { get; set; }

        public async Task<IActionResult> OnGet(string searchValue)
        {
            if (!HttpContext.Session.IsAuthenticated())
            {
                return Redirect("/login");
            }

            TempData["search"] = searchValue;

            Products = await _productService.GetProducts(searchValue);

            return Page();
        }

        public async Task OnGetAddToCart(int productId)
        {
            // Get the product by productId
            var product = await _productService.GetProduct(productId);
            HttpContext.Session.AddToCart(product);
            Products = await _productService.GetProducts();

            // 
            var shipping = HttpContext.Session.GetSession<ObjectData>(SessionConstant.Shipping);

            if(shipping == null)
            {
                shipping = Data.Shippings.FirstOrDefault(s => s.Id == 1);
                HttpContext.Session.SetSession(SessionConstant.Shipping, shipping);
            }

        }

        public async Task<IActionResult> OnPostCreateProduct()
        {
            var result = await _productService.CreateProduct(Product);

            if(result)
            {
                TempData["MessageCreateProduct"] = "Create Product Sucessfully!";
                return Redirect("/");
            }

            TempData["MessageCreateProduct"] = "Create Product Fail! Please check product name maybe existed!";

            return Redirect("/");
        }

        public async Task<IActionResult> OnGetDeleteProduct(int productId)
        {
            var result = await _productService.DeleteProduct(productId);


            if (result)
            {
                TempData["MessageCreateProduct"] = "Delete Product Sucessfully!";
                return Redirect("/");
            }

            TempData["MessageCreateProduct"] = "Delete Product Fail!";

            return Redirect("/");
        }

        public async Task<IActionResult> OnPostUpdateProduct(Product product)
        {
            var result = await _productService.UpdateProduct(product);


            if (result)
            {
                TempData["MessageCreateProduct"] = "Update Product Sucessfully!";
                return Redirect("/");
            }

            TempData["MessageCreateProduct"] = "Update Product Fail! Please check product name maybe existed!";

            return Redirect("/");
        }

    }
}
