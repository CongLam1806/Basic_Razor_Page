using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SaleManagement.BusinessObject.Entity;
using SaleManagement.Service;
using SaleManagement.Utilities;

namespace SaleManagement.Pages.Authen
{
    public class RegisterModel : PageModel
    {
        private readonly IMemberService _memberService;

        public RegisterModel(IMemberService memberService)
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

        public async Task<IActionResult> OnPostRegister()
        {
            IsRegisterForm = true;

            var userResponse = await _memberService.RegisterUser(UserDTO);

            if (userResponse)
            {
                IsRegisterForm = false;
                return Redirect("/");
            }

            return Page();
        }
    }
}
