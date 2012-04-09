using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using PolarTicTacToe.Models;
using System.Web.Security;

namespace PolarTicTacToe.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(long facebookID, string firstName, string lastName)
        {
            Player curPlayer = Player.Get(facebookID);

            if (curPlayer == null)
            {
                Player.Create(facebookID, firstName, lastName);
                curPlayer = Player.Get(facebookID);
            }

            FormsAuthentication.SetAuthCookie(facebookID.ToString(), false /* createPersistentCookie */);

            return Json(true, JsonRequestBehavior.DenyGet);
        }
    }
}
