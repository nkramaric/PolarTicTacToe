﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PolarTicTacToe
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "About",
                "about",
                new { controller = "Home", action = "About" }
            );

            routes.MapRoute(
                "CreateGame",
                "game/create",
                 new { controller = "Game", action = "Create", id = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                "GameMoves",
                "game/{id}/moves",
                 new { controller = "Game", action = "GetMoves", id = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
               "GetActiveGame",
               "game/get",
                new { controller = "Game", action = "Get" } // Parameter defaults
           );

              routes.MapRoute(
               "SetAppRequest",
               "game/{id}/set-app-request",
                new { controller = "Game", action = "SetAppRequest" } // Parameter defaults
           );

            routes.MapRoute(
               "GetActiveGames",
               "games",
                new { controller = "Game", action = "GetGames" } // Parameter defaults
            );

            routes.MapRoute(
               "GetGame",
               "game/{id}",
                new { controller = "Game", action = "GetGame" } // Parameter defaults
           );

            routes.MapRoute(
                "PlayGame",
                "game/{id}/play",
                 new { controller = "Game", action = "Play", id = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                "PlayMove",
                "game/{id}/playmove",
                 new { controller = "Game", action = "PlayMove", id = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}