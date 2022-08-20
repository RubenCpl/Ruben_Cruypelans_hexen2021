using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DAE.HexSystem;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace DAE.Gamesystem
{
    public class PositionEventArgs : EventArgs
    {
        public IHex Position { get; }
        public Card Card { get; }


        public PositionEventArgs(IHex position, Card card)
        {
            Position = position;
            Card = card;
        }
    }


    public class Hex : MonoBehaviour, IHex , IDropHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public event EventHandler<PositionEventArgs> Dropped;
        public event EventHandler<PositionEventArgs> Entered;
        public event EventHandler<PositionEventArgs> Exitted;

        [SerializeField] private UnityEvent OnActivate;
        [SerializeField] private UnityEvent Ondeactivate;

       public void Destroy()
        {
            gameObject.SetActive(false);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (eventData.pointerDrag == null)
                return;      

            Card d = eventData.pointerDrag.GetComponent<Card>();
        
            OnEntered(new PositionEventArgs(this, d));

           
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (eventData.pointerDrag == null)
                return;    

            Card d = eventData.pointerDrag.GetComponent<Card>();      

            OnExited(new PositionEventArgs(this, d));
          
        }

        public void OnDrop(PointerEventData eventData)
        {
            Debug.Log(eventData.pointerDrag.name + " was dropped on " + gameObject.name);
            Card d = eventData.pointerDrag.GetComponent<Card>();

           OnDropped(new PositionEventArgs(this, d));

        }

        protected virtual void OnDropped(PositionEventArgs eventargs)
        {
            var handler = Dropped;
            handler?.Invoke(this, eventargs);
        }

        protected virtual void OnEntered(PositionEventArgs eventargs)
        {
            var handler = Entered;
            handler?.Invoke(this, eventargs);
        }

        protected virtual void OnExited(PositionEventArgs eventargs)
        {
            var handler = Exitted;
            handler?.Invoke(this, eventargs);
        }
            
        public void Activate()
        {
            OnActivate.Invoke();
        }

        public void Deactivate()
        {
            Ondeactivate.Invoke();
        }

    }
}
