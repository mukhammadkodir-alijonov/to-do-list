using to_do_list_app.Interfaces.Repositories;
using to_do_list_app.Interfaces.Services;
using to_do_list_app.Models;
using to_do_list_app.Repositories;
using to_do_list_app.Security;
using to_do_list_app.ViewModels.Users;

namespace to_do_list_app.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        public UserService()
        {
            _repository = new UserRepository();
        }
        public async Task<(bool IsSuccessful, string message)> LoginAsync(string email, string password)
        {
            var user = await _repository.FindByEmailAsync(email);
            if (user is null) return (IsSuccessful: false, Message: "Email is worng!");
            var hashResult = PasswordHasher.Verify(password, user.Salt, user.PasswordHash);
            if (hashResult) return (IsSuccessful: true, Message: "....");
            else return (IsSuccessful: false, Message: "Password is worng!");
        }

        public async Task<(bool IsSuccessful, string message)> RegisterAsync(UserCreateViewModel viewModel)
        {
            var hashResult = PasswordHasher.Hash(viewModel.Password);
            User user = new User()
            {
                Email = viewModel.Email,
                Full_Name = viewModel.Full_Name,
                PasswordHash = hashResult.PasswordHash,
                Salt = hashResult.Salt
            };
            var repositoryResult = await _repository.CreateAsync(user);
            if (repositoryResult) return (IsSuccessful: true, Message: "....");
            else return (IsSuccessful: false, Message: "User can nor created");

        }
    }
}
