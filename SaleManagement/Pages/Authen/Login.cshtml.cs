using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SaleManagement.BusinessObject.Entity;
using SaleManagement.Service;
using SaleManagement.Utilities;
using ShoppingCart.Utilities;

namespace SaleManagement.Pages.Authen
{
    public class LoginModel : PageModel
    {
        private readonly IMemberService _memberService;

        public LoginModel(IMemberService memberService)
        {
            _memberService = memberService;
        }

        [BindProperty]
        public Member UserDTO { get; set; } = new Member();

        public bool IsRegisterForm { get; set; } = false;

        public IActionResult OnGet()
        {
            if (HttpContext.Session.IsAuthenticated())
            {
                return Redirect("/");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostLogin()
        {
            IsRegisterForm = false;

            var userResponse = await _memberService.LoginUser(UserDTO);

            if (userResponse != null)
            {
                HttpContext.Session.SignIn(userResponse);

                return Redirect("/");
            }
            else if (SystemHelper.IsAdmin(UserDTO.Email, UserDTO.Password))
            {
                HttpContext.Session.SignIn(UserDTO, true);

                return Redirect("/");
            }

            return Page();

        }

        public async Task<IActionResult> OnPostRegister()
        {
            IsRegisterForm = true;

            var userResponse = await _memberService.RegisterUser(UserDTO);

            if (userResponse)
            {
                IsRegisterForm = false;
                return Page();
            }

            return Page();
        }

        public IActionResult OnGetLogout()
        {
            HttpContext.Session.SignOut();

            return Page();
        }
    }
}
