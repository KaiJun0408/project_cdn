using BLO;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using BLO.Models;

namespace project_cdn.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            using (DBObject con = new DBObject())
            {
                var data = con.DB.QueryFirstOrDefault<UserListRequest>("Select username, email from user_table");
                var datai = 1;
            }

            return View();
        }
    }

    //-------------------------------------------------Login function-----------------------------------
  
}
