using DAE.BoardSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAE.HexSystem.Actions
{
    class ConfigurableAction<TCard, TPiece> : ActionBase<TCard, TPiece> where TPiece : IPiece where TCard : ICard
    {
        public delegate List<IHex> PositionCollector(Board<IHex, TPiece> board, Grid<IHex> grid, IHex position, TPiece piece, CardType card);
        

        private PositionCollector _positionCollector;   
                       

        public override List<IHex> Validpositions(Board<IHex, TPiece> board, Grid<IHex> grid, IHex position, TPiece piece, CardType card)
        => _positionCollector(board, grid, position, piece, card);

        public override List<IHex> IsolatedPositions(Board<IHex, TPiece> board, Grid<IHex> grid, IHex position, TPiece piece, CardType card)
        => _positionCollector(board, grid, position, piece, card);
    }
}
