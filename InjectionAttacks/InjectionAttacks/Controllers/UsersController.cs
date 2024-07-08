using InjectionAttacks.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Text.Encodings.Web;

namespace InjectionAttacks.Controllers
{
    public class UsersController : Controller
    {
        private readonly JavaScriptEncoder javaScriptEncoder;

        public UsersController(JavaScriptEncoder javaScriptEncoder)
        {
            this.javaScriptEncoder = javaScriptEncoder;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(UserLoginModel userLogin)
        {
            var result = string.Empty;
            if (ModelState.IsValid)
            {
                SqlConnection sqlConnection = new SqlConnection("Data Source=(localdb)\\Mssqllocaldb;Initial Catalog=secureDb;Integrated Security=True");
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Users WHERE UserName = @name AND Password=@pass",sqlConnection);

                sqlCommand.Parameters.AddWithValue("@name", userLogin.UserName);
                sqlCommand.Parameters.AddWithValue("@pass", userLogin.Password);

                sqlConnection.Open();
                var reader = sqlCommand.ExecuteReader();
                if (reader.Read())
                {
                    result = "Giriş Başarılı";
                    ViewBag.LoginState = result;
                    return View();
                }
                sqlConnection.Close();
            }

            result = "Giriş başarısız";
            ViewBag.LoginState = result;

            return View();
        }



        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(UserRegisterModel registerModel)
        {
            registerModel.UserInfo = javaScriptEncoder.Encode(registerModel.UserInfo);
            return View(registerModel);
        }

    }


}
