using Npgsql;
using to_do_list_app.Constants;
using to_do_list_app.Interfaces.Repositories;
using to_do_list_app.Models;

namespace to_do_list_app.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly NpgsqlConnection _connection = new NpgsqlConnection(DbConstants.DB_CONNECTION_STRING);
        public async Task<bool> CreateAsync(User entity)
        {
            try
            {
                await _connection.OpenAsync();
                string query = "INSERT INTO public.users(full_name, email, password_hash, salt) " +
                               "VALUES (@full_name, @email, @password_hash, @salt);";
                NpgsqlCommand command = new NpgsqlCommand(query, _connection)
                {
                    Parameters =
                    {
                        new("full_name", entity.Full_Name),
                        new("email", entity.Email),
                        new("password_hash", entity.PasswordHash),
                        new("salt", entity.Salt)
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
                string query = $"delete from users where id = {id};";
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

        public async Task<User?> FindByEmailAsync(string email)
        {
            try
            {
                await _connection.OpenAsync();
                string query = $"select * from users where email = @email;";
                NpgsqlCommand command = new NpgsqlCommand(query, _connection)
                {
                    Parameters =
                    {
                        new("email", email)
                    }
                };
                var reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    return new User()
                    {
                        Id = reader.GetInt32(0),
                        Full_Name = reader.GetString(1),
                        Email = reader.GetString(2),
                        PasswordHash = reader.GetString(3),
                        Salt = reader.GetString(4)
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

        public async Task<User?> FindByIdasync(int id)
        {
            try
            {
                await _connection.OpenAsync();
                string query = $"select * from users where id = {id};";
                NpgsqlCommand command = new NpgsqlCommand(query, _connection);
                var reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    return new User()
                    {
                        Id = reader.GetInt32(0),
                        Full_Name = reader.GetString(1),
                        Email = reader.GetString(2),
                        PasswordHash = reader.GetString(3),
                        Salt = reader.GetString(4)
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

        public async Task<IList<User>> GetAllAsync(int skip, int take)
        {
            try
            {
                var users = new List<User>();
                await _connection.OpenAsync();
                string query = $"select * from users order by full_name offset {skip} limit {take};";
                NpgsqlCommand command = new NpgsqlCommand(query, _connection);
                var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    var user = new User()
                    {
                        Id = reader.GetInt32(0),
                        Full_Name = reader.GetString(1),
                        Email = reader.GetString(2),
                        PasswordHash = reader.GetString(3),
                        Salt = reader.GetString(4)
                    };
                    users.Add(user);
                }
                return users;
            }
            catch
            {
                return new List<User>();
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }

        public async Task<bool> UpdateAsync(int id, User entity)
        {
            try
            {
                await _connection.OpenAsync();
                string query = $"update users set ful_name = @full_name, email = @email, password_hash = @password_hash, salt =@salt where id = {id};";
                NpgsqlCommand command = new NpgsqlCommand(query, _connection)
                {
                    Parameters =
                {
                    new("ful_name", entity.Full_Name),
                    new("email", entity.Email),
                    new("password", entity.PasswordHash),
                    new("salt", entity.Salt)
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
