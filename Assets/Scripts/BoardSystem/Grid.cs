using System;
using DAE.Commons;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAE.BoardSystem
{
    public class DestoyedEventArgs<THex> : EventArgs
    {
        public List<THex> DestroyList { get; }        

        public DestoyedEventArgs(List<THex> destroyList)
        {
            DestroyList = destroyList;       
        }
    }

    public class Grid<THex>
    {
        public int rows { get; }
        public int columns { get; }

        public Grid(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;
        }

        private BidirectionalDictionary<(int x, int y), THex> _positions = new BidirectionalDictionary<(int x, int y), THex>();

        public bool TryGetPositionAt(int x, int y, out THex position) => _positions.TryGetValue((x, y), out position);

        public bool TryGetCoordinateOf(THex position, out (int x, int y) coordinate)
            => _positions.TryGetKey(position, out coordinate);

        public void Register(int x, int y, THex position)
        {
            _positions.Add((x, y), position);
        }

        public bool Destroy(List<THex> hexList)
        {
            foreach (var hex in hexList)
            {
                if (!_positions.Remove(hex))
                {
                    return false;
                }                

            }

            OnDestroyed(new DestoyedEventArgs<THex>(hexList));
            return true;
        }





        public event EventHandler<DestoyedEventArgs<THex>> destroyed;



        protected virtual void OnDestroyed(DestoyedEventArgs<THex> eventargs)
        {
            var handlers = destroyed;
            handlers?.Invoke(this, eventargs);
        }
    }
}
