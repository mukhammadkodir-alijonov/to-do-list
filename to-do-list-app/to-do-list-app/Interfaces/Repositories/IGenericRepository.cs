namespace to_do_list_app.Interfaces.Repositories
{
    public interface IGenericRepository<T>
    {
        public Task<bool> CreateAsync(T entity);
        public Task<bool> DeleteAsync(int id);
        public Task<bool> UpdateAsync(int id, T entity);
        public Task<IList<T>> GetAllAsync(int skip, int take);
        public Task<T?> FindByIdasync(int id);
    }
}
