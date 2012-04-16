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
            bool isValid = false;

            if (game.PendingPlayerID == CurrentUser.ID && game.GameState != GameState.Finished.ToString())
            {
                isValid = Game.PlayMove(id, CurrentUser.ID, spot, out winner, out message);
            }
            
            //refresh
            game = Game.Get(id);

            return Json(new { isValid = isValid, winner = game.WinnerID }, JsonRequestBehavior.DenyGet);
        }

        [HttpGet]
        public ActionResult GetGame(int id)
        {
            Game game = Game.Get(id);

            return Json(game.GetApiGame(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetMoves(int id)
        {
            Game game = Game.Get(id);

            int? winner = game.WinnerID;

            int PendingPlayerID = game.PendingPlayerID;

            long? curAppRequest = game.CurAppRequest;

            return Json(new { moves = game.MoveList, winner = winner, PendingPlayerID = PendingPlayerID, curAppRequest = curAppRequest }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult Get(long FBID1, long FBID2)
        {
            var player1 = Player.GetByFBID(FBID1);
            var player2 = Player.GetByFBID(FBID2);

            long? pendingPlayerFBID = null;
            int? gameID = null;

            if (player1 != null && player2 != null)
            {
                Game curGame = Game.GetActive(player1.ID, player2.ID);
                if (curGame != null)
                {
                    pendingPlayerFBID = Player.GetByID(curGame.PendingPlayerID).FacebookID;
                    gameID = curGame.ID;
                }
            }

            return Json(new { pendingPlayerFBID = pendingPlayerFBID, gameID = gameID }, JsonRequestBehavior.DenyGet);
        }

        [HttpGet]
        public ActionResult GetGames()
        {
            List<Game> games = Game.GetActive(CurrentUser.ID);
            
            return Json(games.ToList(p => p.GetApiGame()), JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult Create(long OpponentFBID, long appRequest)
        {
            // if the opponent doesnt exist create it
            var Opponent = Player.GetByFBID(OpponentFBID);
            if (Opponent == null)
                Opponent = Player.Create(OpponentFBID);

            // if an active game doesnt exist create it
            var curGame = Game.GetActive(CurrentUser.ID, Opponent.ID);
            if (curGame == null)
                curGame = Game.Create(CurrentUser.ID, Opponent.ID);

            curGame.setAppRequest(appRequest);

            return Json(curGame.GetApiGame(), JsonRequestBehavior.AllowGet);
        }
        
        [HttpGet]
        public ActionResult SetAppRequest(int id, long appRequest)
        {
            var curGame = Game.Get(id);

            curGame.setAppRequest(appRequest);

            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}
