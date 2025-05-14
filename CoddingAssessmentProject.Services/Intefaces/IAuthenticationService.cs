using CoddingAssessmentProject.Repositories.ViewModels;

namespace CoddingAssessmentProject.Services.Intefaces
{
    public interface IAuthenticationService
    {
        /// <summary>
        /// Authenticates a user using their email and password.
        /// </summary>
        /// <param name="email">The email of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>A task that returns the authenticated user if found, otherwise null.</returns>
        Task<UserLoginViewModel?> AuthenticateUserUsingEmailPassword(string userEmail, string userPassword);

        /// <summary>
        /// Checks if a user exists by their email.
        /// </summary>
        /// <param name="email">The email of the user to check.</param>
        /// <returns>A task that returns true if the user exists, otherwise false.</returns>
        Task<bool> CheckIfUserExists(string userEmail);
    }
}
