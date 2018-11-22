using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Warzywniak.Models;

namespace Warzywniak.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

		[HttpPost]
		public ActionResult Authorize(Warzywniak.Models.User userModel)
		{
			using (WarzywniakLoginEntities db = new WarzywniakLoginEntities())
			{
				var userDetails = db.Users.Where(x => x.Nick == userModel.Nick
									&& x.Password == userModel.Password).FirstOrDefault();
				if (userDetails == null)
				{
					userModel.LoginErrorMessage = "Wrong username or password.";
					return View("Index", userModel);
				}
				else
				{
					Session["userID"] = userDetails.UserId;
					return RedirectToAction("Index", "Home");
				}
			}
		}
    }
}