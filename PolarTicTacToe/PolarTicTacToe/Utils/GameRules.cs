using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PolarTicTacToe.Models;

namespace PolarTicTacToe.Utils
{
    public class GameRules
    {
        internal bool IsFinished(Models.Game game, out int? winner)
        {
            ///TODO: implement this
            winner = null;

            if (game.MoveList.Count < 7)
            {
                return false;
            }

            if (CheckHorizontal(game) || CheckVertical(game) || CheckSpiral(game))
            {
                winner = game.MoveList.Last().UserID;
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CheckSpiral(Game game)
        {
            var lastMove = game.MoveList.Last();
            int? playerID = lastMove.UserID;
            int y = lastMove.position.Y;
            int x = lastMove.position.X;
            int distanceToOuterSpiral = (3 - y);

            Coordinate outerPosition1 = new Coordinate(x + distanceToOuterSpiral, 3);
            Coordinate outerPosition2 = new Coordinate(x - distanceToOuterSpiral, 3);

            var outer1 = (game[outerPosition1] == playerID && game[outerPosition1 - new Coordinate(1, 1)] == playerID && game[outerPosition1 - new Coordinate(2, 2)] == playerID && game[outerPosition1 - new Coordinate(3, 3)] == playerID);
            var outer2 = (game[outerPosition2] == playerID && game[outerPosition2 + new Coordinate(1, -1)] == playerID && game[outerPosition2 + new Coordinate(2, -2)] == playerID && game[outerPosition2 + new Coordinate(3, -3)] == playerID);

            //checking both spirals
            return outer1
                || outer2;
        }

        private bool CheckVertical(Game game)
        {
            var lastMove = game.MoveList.Last();
            int? playerID = lastMove.UserID;
            int y = lastMove.position.Y;
            int x = lastMove.position.X;

            return game[x, 0] == playerID && game[x, 1] == playerID && game[x,2] == playerID && game[x, 3] == playerID;
        }

        private bool CheckHorizontal(Models.Game game)
        {
            var lastMove = game.MoveList.Last();
            int? player = game.MoveList.Last().UserID;
            int row = 0;

            for (int i = lastMove.position.X - 4; i < lastMove.position.X + 4; i++)
            {
                Coordinate curPosition = new Coordinate(i, lastMove.position.Y);

                int? userID = game[curPosition.X, curPosition.Y];

                if (userID == player)
                {
                    row++;
                    if (row == 4)
                    {
                        return true;
                    }
                }
                else
                {
                    row = 0;
                }
            }

            return false;
        }

        internal bool HasPlayed(Models.Game game, Tuple<int, int> spot)
        {
            return game.MoveList.FirstOrDefault(p => p.position.X == spot.Item1 && p.position.Y == spot.Item2) != null;
        }
    }
}