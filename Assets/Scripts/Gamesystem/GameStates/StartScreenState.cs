//using DAE.GameSystem.GameStates;
//using DAE.StateSystem;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Unity;
//using UnityEngine;


//namespace DAE.Gamesystem
//{
//    class StartScreenState : GameStateBase
//    {
//        GameObject _startCanvas;
//        public StartScreenState(StateMachine<GameStateBase> stateMachine, GameObject startCanvas) : base(stateMachine)
//        {
//            _startCanvas = startCanvas;
//        }

//        internal override void StartGame()
//        {
//            StateMachine.MoveToState(GameState.GamePlayState);
//        }

//        public override void OnExit()
//        {
//            _startCanvas.SetActive(false);
//        }




//    }
//}
