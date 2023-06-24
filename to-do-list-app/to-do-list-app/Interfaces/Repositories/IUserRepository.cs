using to_do_list_app.Models;

namespace to_do_list_app.Interfaces.Repositories;
public interface IUserRepository : IGenericRepository<User>
{
    public Task<User?> FindByEmailAsync(string email); 
}
