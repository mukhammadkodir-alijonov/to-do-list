using Npgsql;
using to_do_list_app.Constants;
using to_do_list_app.Interfaces.Repositories;

namespace to_do_list_app.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly NpgsqlConnection _connection = new NpgsqlConnection(DbConstants.DB_CONNECTION_STRING);

        public async Task<bool> CreateAsync(Models.Task entity)
        {
            try
            {
                await _connection.OpenAsync();
                string query = "INSERT INTO public.tasks(title, description, begin_time, end_time, status, owner_id )" +
                               "VALUES (@title, @description, @begin_time, @end_time, @status, @owner_id);";
                NpgsqlCommand command = new NpgsqlCommand(query, _connection)
                {
                    Parameters =
                    {
                        new("title", entity.Title),
                        new("description", entity.Description),
                        new("begin_time", entity.BeginTime),
                        new("end_time", entity.EndTime),
                        new("status", entity.Status),
                        new("owner_id", entity.OwnerId)
                    }
                };
                int result = await command.ExecuteNonQueryAsync();

                if (result == 0) return false;
                else return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                await _connection.OpenAsync();
                string query = $"delete from tasks where id = {id};";
                NpgsqlCommand command = new NpgsqlCommand(query, _connection);
                int result = await command.ExecuteNonQueryAsync();

                if (result == 0) return false;
                else return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }

        public async Task<Models.Task?> FindByIdasync(int id)
        {
            try
            {
                await _connection.OpenAsync();
                string query = $"select * from tasks where id = {id};";
                NpgsqlCommand command = new NpgsqlCommand(query, _connection);
                var reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    return new Models.Task()
                    {
                        Id = reader.GetInt32(0),
                        Title = reader.GetString(1),
                        Description = reader.GetString(2),
                        BeginTime = reader.GetDateTime(3),
                        EndTime = reader.GetDateTime(4),
                        Status = reader.GetString(5),
                        OwnerId = reader.GetInt32(6),
                        CreatedAt = reader.GetDateTime(7)
                    };
                }
                else return null;
            }
            catch
            {
                return null;
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }

        public async Task<IList<Models.Task>> GetAllAsync(int skip, int take)
        {
            try
            {
                var tasks = new List<Models.Task>();
                await _connection.OpenAsync();
                string query = $"select * from tasks order by created_at offset {skip} limit {take};";
                NpgsqlCommand command = new NpgsqlCommand(query, _connection);
                var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    var task = new Models.Task()
                    {
                        Id = reader.GetInt32(0),
                        Title = reader.GetString(1),
                        Description = reader.GetString(2),
                        BeginTime = reader.GetDateTime(3),
                        EndTime = reader.GetDateTime(4),
                        Status = reader.GetString(5),
                        OwnerId = reader.GetInt32(6),
                        CreatedAt = reader.GetDateTime(7)
                    };
                    tasks.Add(task);
                }
                return tasks;
            }
            catch
            {
                return new List<Models.Task>();
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }

        public async Task<bool> UpdateAsync(int id, Models.Task entity)
        {
            try
            {
                await _connection.OpenAsync();
                string query = "update tasks set title = @title, description = @description, begin_time = @begin_time, end_time =@end_time, status=@status, owner_id = @owner_id" +
                               $" where id = {id};";
                NpgsqlCommand command = new NpgsqlCommand(query, _connection)
                {
                    Parameters =
                {
                        new("title", entity.Title),
                        new("description", entity.Description),
                        new("begin_time", entity.BeginTime),
                        new("end_time", entity.EndTime),
                        new("status", entity.Status),
                        new("owner_id", entity.OwnerId)
                }
                };
                int result = await command.ExecuteNonQueryAsync();
                if (result == 0) return false;
                else return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }
    }
}
