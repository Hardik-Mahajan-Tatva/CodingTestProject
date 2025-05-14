using Microsoft.Extensions.Configuration;
using CoddingAssessmentProject.Repositories.Models;
using CoddingAssessmentProject.Services.Intefaces;
using CoddingAssessmentProject.Repositories.Intefaces;
using CoddingAssessmentProject.Repositories.ViewModels;

namespace CoddingAssessmentProject.Services.Implmentations
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IConfiguration _configuration;

        public AuthenticationService(IUsersRepository usersRepository, IConfiguration configuration)
        {
            _usersRepository = usersRepository;
            _configuration = configuration;
        }
        public async Task<UserLoginViewModel?> AuthenticateUserUsingEmailPassword(string email, string password)
        {
            User? user = await _usersRepository.GetUserLoginAsync(email, password);

            if (user == null)
                return null;
            UserLoginViewModel userLoginViewModel = new()
            {
                UserEmail = user.UserEmail,
                UserPassword = user.UserPassword,
                UserRole = user.UserRole,
            };

            return userLoginViewModel;
        }
        public async Task<bool> CheckIfUserExists(string email)
        {
            if (await _usersRepository.GetUserByEmailAsync(email) != null)
                return true;
            else
                return false;
        }
    }


}

