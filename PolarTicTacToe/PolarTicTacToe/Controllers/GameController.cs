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

            return View(game);
        }

        public ActionResult PlayMove(int id, int player, Tuple<int, int> spot)
        {
            int? winner;
            string message;
            bool isValid = Game.PlayMove(id, player, spot, out winner, out message);

            return View();
        }

        [HttpPost]
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
