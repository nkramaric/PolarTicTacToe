using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using PolarTicTacToe.Models;

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
            var curPlayer = Player.Get(facebookID);
            
            if (curPlayer == null)
                Player.Create(facebookID, firstName, lastName);

            return Json(true, JsonRequestBehavior.DenyGet);
        }
    }
}
