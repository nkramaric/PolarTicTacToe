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

            if (game.PendingPlayerID == CurrentUser.ID && game.GameState != GameState.Finished.ToString())
            {
                bool isValid = Game.PlayMove(id, CurrentUser.ID, spot, out winner, out message);
            }
            else
            {
                return Json(false, JsonRequestBehavior.DenyGet);
            }

            return Json(true, JsonRequestBehavior.DenyGet);
        }
        
        [HttpPost]
        public ActionResult Get(long FBID1, long FBID2)
        {
            var player1 = Player.GetByFBID(FBID1);
            var player2 = Player.GetByFBID(FBID2);

            long? pendingPlayerFBID = null;

            if (player1 != null && player2 != null)
            {
                Game curGame = Game.GetActive(player1.ID, player2.ID);
                if (curGame != null)
                {
                    pendingPlayerFBID = Player.GetByID(curGame.PendingPlayerID).FacebookID;
                }
            }
            
            return Json(pendingPlayerFBID, JsonRequestBehavior.DenyGet);
        }


        [HttpGet]
        public ActionResult Create(long OpponentFBID)
        {
            // if the opponent doesnt exist create it
            var Opponent = Player.GetByFBID(OpponentFBID);
            if (Opponent == null)
                Opponent = Player.Create(OpponentFBID);

            // if an active game doesnt exist create it
            var curGame = Game.GetActive(CurrentUser.ID, Opponent.ID);
            if (curGame == null)
                curGame = Game.Create(CurrentUser.ID, Opponent.ID);

            return RedirectToAction("Play", "Game", new { id = curGame.ID });
        }

    }
}
