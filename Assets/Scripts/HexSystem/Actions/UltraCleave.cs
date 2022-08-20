//using DAE.BoardSystem;
//using DAE.HexSystem;
//using DAE.HexSystem.Actions;

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using UnityEngine;

//namespace DAE.HexSystem.Actions
//{
//    class UltraCleave<TCard, TPiece> : ActionBase<TCard, TPiece> where TPiece : IPiece where TCard : ICard
//    {

//         List<IHex> _valid = new List<IHex>();
//        public override void ExecuteAction(Board<IHex, TPiece> board, Grid<IHex> grid, IHex position, TPiece piece, CardType card)
//        {
//            foreach (var hex in IsolatedPositions(board, grid, position, piece, card))
//            {
//                if (hex != null)
//                {

//                    if (board.TryGetPieceAt(hex, out var enemy))
//                    {
//                        board.TryGetPositionOf(piece, out var positionPlayer);
//                        grid.TryGetCoordinateOf(positionPlayer, out var coordinate);
//                        var playerX = coordinate.x;
//                        var playerY = coordinate.y;
//                        board.TryGetPositionOf(enemy, out var positionenemy);
//                        grid.TryGetCoordinateOf(positionenemy, out var coordinateenemy);
//                        var enemyX = coordinateenemy.x;
//                        var enemyY = coordinateenemy.y;

//                        var directionX = enemyX - playerX;
//                        var directionY = enemyY - playerY;

//                        int nexPositionenemyY =0;
//                        int nexPositionenemyX =0;

//                        Vector2 direction = new Vector2(directionX, directionY);
//                        Math.Abs(direction.magnitude);
//                        if (Math.Abs(direction.magnitude) >= 1.5)
//                        {
//                            nexPositionenemyX = (int)Math.Round(direction.x)/2 + enemyX;
//                            nexPositionenemyY = (int)Math.Round(direction.y)/2 + enemyY;
//                        } else if (Math.Abs(direction.magnitude) <= 1.5)
//                        {
//                            nexPositionenemyX = (int)Math.Round(direction.x)*2 + enemyX;
//                            nexPositionenemyY = (int)Math.Round(direction.y)*2 + enemyY;
//                        }

//                        //if (Math.Abs(direction.magnitude) <= 1)
//                        //{
//                        //    nexPositionenemyX = (int)direction.normalized.x *2+ enemyX;
//                        //    nexPositionenemyY = (int)direction.normalized.y *2+ enemyY;
//                        //}

//                        //else if (Math.Abs(direction.magnitude) <= 1)
//                        //{
//                        //    nexPositionenemyX = directionX + 2*enemyX;
//                        //    nexPositionenemyY = directionY + 2*enemyY;
//                        //}


//                        if (grid.TryGetPositionAt(nexPositionenemyX, nexPositionenemyY, out var enemyNextPosition))
//                        {

//                            if (!board.TryGetPieceAt(enemyNextPosition, out var pieceInTheWay))
//                            {
//                                board.Take(enemy);
//                                board.Place(enemy, enemyNextPosition);

//                            }
//                            else
//                            {
//                                board.Take(enemy);
//                                board.Place(enemy, hex);
//                            }
//                        }
//                        else
//                        {
//                            board.Take(enemy);
//                        }
//                    }
//                    hex.Destroy();
//                }
//            }
//        }

//        public override List<IHex> IsolatedPositions(Board<IHex, TPiece> board, Grid<IHex> grid, IHex position, TPiece piece, CardType card)
//        {
//            ActionHelper<TCard, TPiece> actionHelperPartual = new ActionHelper<TCard, TPiece>(board, grid, position, piece, card);
//            actionHelperPartual.Test1(2)
//                        .Test2(2)
//                        .Test3(2)
//                        .Test4(2)
//                        .Test5(2)
//                        .Test6(2);

//            actionHelperPartual.Collect().RemoveAll(item => item == null);


//            return actionHelperPartual.Collect();
//        }

//        public override List<IHex> Validpositions(Board<IHex, TPiece> board, Grid<IHex> grid, IHex position, TPiece piece, CardType card)
//        {
//            ActionHelper<TCard, TPiece> actionHelper = new ActionHelper<TCard, TPiece>(board, grid, position, piece, card);
//            actionHelper.Test1(2)
//                        .Test2(2)
//                        .Test3(2)
//                        .Test4(2)
//                        .Test5(2)
//                        .Test6(2);


//             actionHelper.Collect().RemoveAll(item => item == null);



//            return actionHelper.Collect();


//        }
//    }
//}
