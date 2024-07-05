using SaleManagement.BusinessObject.Entity;

namespace SaleManagement.Service
{
    public interface IMemberService
    {

        Task<Member?> LoginUser(Member user);
        Task<bool> RegisterUser(Member user);

    }
}
