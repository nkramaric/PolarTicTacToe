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
    public class HomeController : BaseController
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
            Player curPlayer = Player.GetByFBID(facebookID);

            if (curPlayer == null)
            {
                curPlayer = Player.Create(facebookID, firstName, lastName);
            }
            else
            {
                Player.Edit(facebookID, firstName, lastName);
            }

            FormsAuthentication.SetAuthCookie(facebookID.ToString(), false /* createPersistentCookie */);

            return Json(true, JsonRequestBehavior.DenyGet);
        }
    }
}
