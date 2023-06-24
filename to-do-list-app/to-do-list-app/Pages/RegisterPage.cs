using to_do_list_app.Interfaces.Services;
using to_do_list_app.Services;
using to_do_list_app.ViewModels.Users;

namespace to_do_list_app.Pages
{
    public class RegisterPage
    {
        public static async System.Threading.Tasks.Task RunAsync()
        {
            Console.Clear();
            UserCreateViewModel user = new UserCreateViewModel();

            Console.Write("Full Name:");
            user.Full_Name = Console.ReadLine();

            Console.Write("Email : ");
            user.Email = Console.ReadLine();

            Console.Write("Password : ");
            user.Password = Console.ReadLine();

            IUserService userService = new UserService();
            var result = await userService.RegisterAsync(user);
            if (result.IsSuccessful)
            {
                Console.WriteLine("Muvoffqqiyatli....");
                Thread.Sleep(2000);
                await LoginPage.RunAsync();
            }
            else
            {
                Console.WriteLine(result.message);
                Thread.Sleep(2000);
                await RunAsync();
            }
        }
    }
}
