using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using to_do_list_app.Interfaces.Repositories;
using to_do_list_app.Models;

namespace to_do_list_app.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        public Task<bool> CreateAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<User?> FindByIdasync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<User>> GetAllAsync(int skip, int take)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(int id, User entity)
        {
            throw new NotImplementedException();
        }
    }
}
