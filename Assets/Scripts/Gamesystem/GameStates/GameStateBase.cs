using DAE.Gamesystem;

using DAE.StateSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAE.GameSystem.GameStates
{
    class GameStateBase : IState<GameStateBase>
    {
        public StateMachine<GameStateBase> StateMachine { get; }

        public GameStateBase(StateMachine<GameStateBase> stateMachine)
        {
            StateMachine = stateMachine;
        }

        public virtual void OnEnter()
        {
        }

        public virtual void OnExit()
        {
        }

        internal virtual void HighLightNew(Hex position, Card card)
        {

        }

        internal virtual void UnHighlightOld(Hex position, Card card)
        {

        }

        internal virtual void OnDrop(Hex position, Card card)
        {

        }   


        //internal virtual void StartGame()
        //{

        //}

        //internal virtual void EndGame()
        //{

        //}

        public virtual void PauseGame()
        {

        }

        internal virtual void UnPauseGame()
        {

        }


    }
}
