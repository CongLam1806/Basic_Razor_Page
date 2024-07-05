using Microsoft.EntityFrameworkCore;
using SaleManagement.BusinessObject.Entity;
using SaleManagement.Repository.MemberRepo;

namespace SaleManagement.Repository
{
    public class MemberRepository : IMemberRepository
    {

        private readonly FStoreContext _context;

        public MemberRepository(FStoreContext context)
        {
            _context = context;
        }

        public MemberRepository()
        {
            _context = new FStoreContext();
        }

        public async Task<Member?> GetMemberByEmailAndPassword(string email, string password)
        {
            return await _context.Members.FirstOrDefaultAsync(m => m.Email.Equals(email) && m.Password.Equals(password));
        }

        public async Task<Member?> GetMemberById(int id)
        {
            return await _context.Members.FirstOrDefaultAsync(m => m.MemberId == id);
        }

        public async Task<int> CheckExist(string email, string password)
        {
            var member = await _context.Members.FirstOrDefaultAsync(m => m.Email == email && m.Password == password);

            return member != null ? member.MemberId : -1;
        }

        public async Task<bool> CreateMember(Member member)
        {
            if(await _context.Members.AnyAsync(m => m.Email.Equals(member.Email)))
            {
                return false;
            }

            await _context.Members.AddAsync(member);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateMember(Member member)
        {
            var existingMember = await _context.Members.FirstOrDefaultAsync(m => m.MemberId == member.MemberId);

            if (existingMember == null)
            {
                return false;
            }

            _context.Members.Update(member);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteMember(int id)
        {
            var member = await _context.Members.FirstOrDefaultAsync(m => m.MemberId == id);
            if (member == null)
            {
                return false;
            }

            _context.Members.Remove(member);
            return await _context.SaveChangesAsync() > 0;
        }

    }
}
