using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAE.HexSystem
{
    public interface IDeck<TCardData>
    {        
        public List<TCardData> CurrentDeckList {get;}
        public List<TCardData> StartingDecklist { get; }
        public List<TCardData> TemporaryCardsList { get; }
        public List<TCardData> PlayerHandList { get; }
        public void EqualizeDecks();

    }
}
