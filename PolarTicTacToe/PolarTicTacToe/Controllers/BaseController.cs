using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PolarTicTacToe.Models;

namespace PolarTicTacToe.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (CurrentUser != null)
            {
                ViewBag.User = CurrentUser;
                if (CurrentUser != null)
                {
                    ViewData["user_id"] = CurrentUser.FacebookID;
                }
            }
            else
            {
                RedirectToAction("Index", "Home");
            }
            base.OnActionExecuting(filterContext);
        }

        private Player currentUser = null;

        protected Player CurrentUser
        {
            get
            {
                if (currentUser == null && !String.IsNullOrWhiteSpace(User.Identity.Name))
                {
                    currentUser = Player.GetByFBID(long.Parse(User.Identity.Name));
                }

                return currentUser;
            }
            set
            {
                currentUser = value;
            }
        }

    }
}
