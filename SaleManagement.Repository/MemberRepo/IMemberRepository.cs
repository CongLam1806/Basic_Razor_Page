using SaleManagement.BusinessObject.Entity;

namespace SaleManagement.Repository.MemberRepo
{
    public interface IMemberRepository
    {
        Task<Member?> GetMemberById(int id);
        Task<int> CheckExist(string email, string password);
        Task<bool> CreateMember(Member member);
        Task<bool> UpdateMember(Member member);
        Task<bool> DeleteMember(int id);
        Task<Member?> GetMemberByEmailAndPassword(string email, string password);

    }
}
