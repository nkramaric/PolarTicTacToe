using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PolarTicTacToe.Models;
using PolarTicTacToe.Utils;

namespace PolarTicTacToe.Controllers
{
    public class GameController : BaseController
    {
        //
        // GET: /Game/

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Play(int id)
        {
            Game game = Game.Get(id);

            ViewBag.Game = game;

            return View(game);
        }

        [HttpPost]
        public ActionResult PlayMove(int id, int x, int y)
        {
            int? winner;
            string message;

            Game game = Game.Get(id);

            Tuple<int, int> spot = new Tuple<int, int>(x, y);

            if (game.PendingPlayerID == CurrentUser.ID)
            {
                bool isValid = Game.PlayMove(id, CurrentUser.ID, spot, out winner, out message);
            }
            else
            {
                return Json(false, JsonRequestBehavior.DenyGet);
            }

            return Json(true, JsonRequestBehavior.DenyGet);
        }

        [HttpGet]
        public ActionResult Create()
        {
            PolarTicTacToeDataContext dataContext = new PolarTicTacToeDataContext();

            Game newGame = new Game();
            newGame.ChallengerID = CurrentUser.ID;
            newGame.GameState = GameState.Active.ToString();

            dataContext.Games.InsertOnSubmit(newGame);

            dataContext.SubmitChanges();

            return RedirectToAction("Play", "Game", new { id = newGame.ID });
        }

    }
}
