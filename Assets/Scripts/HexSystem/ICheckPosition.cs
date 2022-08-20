using DAE.BoardSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAE.HexSystem
{
    interface ICheckPosition<TCard, TPiece> where TPiece : IPiece where TCard : ICard

    {
        
        bool CanExecute(Board<IHex, TPiece> board, Grid<IHex> grid, IHex position, TPiece piece, CardType card);

        void ExecuteAction(Board<IHex, TPiece> board, Grid<IHex> grid, IHex position, TPiece piece, CardType card);       

        List<IHex> Validpositions(Board<IHex, TPiece> board, Grid<IHex> grid, IHex position, TPiece piece, CardType card);

        List<IHex> IsolatedPositions(Board<IHex, TPiece> board, Grid<IHex> grid, IHex position, TPiece piece, CardType card);

    }
}
