using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAE.HexSystem
{
    public interface IHex
    {
        //public event EventHandler Activated;
        //public event EventHandler Deactivated;

        public void Activate();
        public void Deactivate();

        public void Destroy();
                     
    }
}
