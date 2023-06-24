namespace to_do_list_app.Models;
public class User
{
    public int Id { get; set; }
    public string Full_Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Salt { get; set; } = string.Empty;
}
