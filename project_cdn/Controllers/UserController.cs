using BLO;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using BLO.Models;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Text.Json;
using Newtonsoft.Json;

namespace project_cdn.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Listing(List<string> filter)
        {
            //List<> results = JsonConvert.DeserializeObject<>(ids);
            using (DBObject con = new DBObject())
            {
                var data = con.DB.Query<UserListRequest>("Select id, username, email, phone, skillsets, hobby from user_table where status = 1");
                var querydata = new List<UserListRequest>();
                var newList = new List<UserListRequest>();
                for (int i = 0; i < filter.Count; i++)
                {
                    if (filter[i] != null)
                    {
                        switch (i)
                        {
                            case 0:
                                querydata = data.Where(x => x.username.Contains(filter[0])).ToList();
                                break;
                            case 1:
                                querydata = data.Where(x => x.email.Contains(filter[1])).ToList();
                                break;
                            case 2:
                                querydata = data.Where(x => x.phone.Contains(filter[2])).ToList();
                                break;
                            case 3:
                                querydata = data.Where(x => x.skillsets.Contains(filter[3])).ToList();
                                break;
                            case 4:
                                querydata = data.Where(x => x.hobby.Contains(filter[4])).ToList();
                                break;
                        }
                        newList.AddRange(querydata);
                    }
                }
                newList = newList.DistinctBy(x => x.id).ToList();

                return Json((newList.Count == 0 ? data : newList));
            }
        }

        public ActionResult Edit(long id)
        {
            using (DBObject con = new DBObject())
            {
                var data = con.DB.Query<UserListRequest>("Select id, username, email, phone, skillsets, hobby, status from user_table where status = 1 AND id = @ids", new { ids = id, }).FirstOrDefault();

                UserListRequest model = new UserListRequest();

                model.id = data?.id;
                model.username = data?.username;
                model.email = data?.email;
                model.phone = data?.phone;
                model.skillsets = data?.skillsets;
                model.hobby = data?.hobby;

                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(long id, UserListRequest model)
        {
            using (DBObject con = new DBObject())
            {
                //ProfileRequest data = con.DB.QueryFirstOrDefault<ProfileRequest>("UPDATE user SET UserName  = '" + profileDetails.UserName + "',Email  = '" + profileDetails.Email + "',Phone  = '" + profileDetails.Phone + "',Password  = '" + profileDetails.Password + "' WHERE id = '" + profileDetails.Id + "';");
                var data = con.DB.QueryFirstOrDefault<UserListRequest>("UPDATE user_table SET email  = @mail, phone  = @phone, skillsets  = @skillsets, hobby = @hobby WHERE id = @id",
                new
                {
                    mail = model.email,
                    phone = model.phone,
                    skillsets = model.skillsets,
                    hobby = model.hobby,
                    id = model.id
                });
            }

            ReturnResponse response = new ReturnResponse()
            {
                status = true,
                msg = "User Updated!",
            };

            return Json(response);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserListRequest model)
        {
            using (DBObject con = new DBObject())
            {
                var checkDuplicate = con.DB.Query<UserListRequest>("Select id, username, email, phone, skillsets, hobby, status from user_table where status = 1 AND username = @user", new { user = model.username, }).FirstOrDefault();
                if (checkDuplicate == null)
                {
                    //RegisterRequest data = con.DB.QueryFirstOrDefault<RegisterRequest>("INSERT INTO user (FirstName, LastName, UserName, Phone, Email, Password, RecStatus, ActivationCode) Values ('" + registerDetails.FirstName + "' , '" + registerDetails.LastName + "' , '" + registerDetails.UserName + "', '" + registerDetails.Phone + "', '" + registerDetails.Email + "', '" + registerDetails.Password + "' , '" + registerDetails.RecStatus + "', '" + registerDetails.ActivationCode + "');");
                    var data = con.DB.QueryFirstOrDefault<UserListRequest>("INSERT INTO user_table (username, email, phone, skillsets, hobby, status) Values ( @username , @mail , @phone, @skillsets, @hobby, @status);",
                        new
                        {
                            username = model.username,
                            mail = model.email,
                            phone = model.phone,
                            skillsets = model.skillsets,
                            hobby = model.hobby,
                            status = 1,
                        });
                    ReturnResponse response = new ReturnResponse()
                    {
                        status = true,
                        msg = "User Created!",
                    };

                    return Json(response);
                }
                else
                {
                    ReturnResponse response = new ReturnResponse()
                    {
                        status = false,
                        msg = "User Existed!",
                    };

                    return Json(response);
                }


            }
        }

        [HttpPost]
        public ActionResult DeleteUser(long id)
        {
            using (DBObject con = new DBObject())
            {
                var data = con.DB.QueryFirstOrDefault<UserListRequest>("UPDATE user_table SET status = 0 WHERE id = @ids;", new { ids = id, });
            }

            ReturnResponse response = new ReturnResponse()
            {
                status = true,
                msg = "User Deleted!"
            };

            return Json(response);
        }

    }
}
