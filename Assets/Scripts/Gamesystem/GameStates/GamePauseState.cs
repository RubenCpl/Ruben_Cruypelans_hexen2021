using DAE.GameSystem.GameStates;
using DAE.StateSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DAE.Gamesystem
{
    class PauseScreenState : GameStateBase
    {

        private CanvasGroup _pauseScreen;
        public PauseScreenState(StateMachine<GameStateBase> stateMachine, CanvasGroup pauseScreen) : base(stateMachine)
        {
            _pauseScreen = pauseScreen;
        }
        public override void OnEnter()
        {
            PauseGame();
        }
        
        public override void PauseGame()
        {
            _pauseScreen.gameObject.SetActive(true);
        }
        internal override void UnPauseGame()
        {
            _pauseScreen.gameObject.SetActive(false);
            StateMachine.MoveToState(GameState.GamePlayState);


        }
    }
}
