
using CoddingAssessmentProject.Repositories.Intefaces;
using CoddingAssessmentProject.Repositories.Models;
using Microsoft.EntityFrameworkCore;

namespace CoddingAssessmentProject.Repositories.Implmentations
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ApplicationDbContext _context;

        public UsersRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserLoginAsync(string userEmail, string userPassword)
        {
            User? user = await _context.Users
                .FirstOrDefaultAsync(u =>
                    u.UserEmail.ToLower() == userEmail.ToLower() &&
                    u.UserPassword == userPassword);
            return user;
        }
        public async Task<User?> GetUserByEmailAsync(string userEmail)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.UserEmail == userEmail);
        }
    }
}
