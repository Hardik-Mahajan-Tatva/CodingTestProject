using CoddingAssessmentProject.Repositories.Models;

namespace CoddingAssessmentProject.Repositories.Intefaces
{
    public interface IUsersRepository
    {
        /// <summary>
        /// Retrieves a Users  email and password asynchronously.
        /// </summary>
        /// <param name="email">The email of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>A task that returns the User if found, otherwise null.</returns>
        Task<User?> GetUserLoginAsync(string userEmail, string userPassword);

        /// <summary>
        /// Retrieves a User by their email asynchronously.
        /// </summary>
        /// <param name="email">The email of the user to retrieve.</param>
        /// <returns>A task that returns the user if found, otherwise null.</returns>
        Task<User?> GetUserByEmailAsync(string userEmail);
    }
}
