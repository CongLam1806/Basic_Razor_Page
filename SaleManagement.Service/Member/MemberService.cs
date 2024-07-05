using SaleManagement.BusinessObject.Entity;
using SaleManagement.Repository.MemberRepo;

namespace SaleManagement.Service
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;

        public MemberService(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public async Task<Member?> LoginUser(Member user)
        {
            return await _memberRepository.GetMemberByEmailAndPassword(user.Email, user.Password);
        }

        public async Task<bool> RegisterUser(Member user)
        {
            return await _memberRepository.CreateMember(user);
        }

    }
}
