using DAE.BoardSystem;
using DAE.Gamesystem;
using DAE.HexSystem;
using DAE.StateSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DAE.GameSystem.GameStates
{
    class GamePlayState : GameStateBase
    {

        private ActionManager<Card, Piece> _actionManager;
        private Board<IHex, Piece> _board;
        private Deck _deck;

        private List<CardData> _handListSave;

        private Piece _playerPiece;

        private GameObject _playerHandObject;
        private bool _firstTimeEnter;
        public GamePlayState(StateMachine<GameStateBase> stateMachine, Board<IHex, Piece> board, ActionManager<Card, Piece> moveManager, PlayerHand playerhand, Deck deck, Piece playerPiece, GameObject playerHandObject) : base(stateMachine)
        {
            _playerPiece = playerPiece;
            _playerHandObject = playerHandObject;


            _deck = deck;
            _actionManager = moveManager;
            _board = board;
            _deck.EqualizeDecks();
            _deck.ShuffleCurrentDeck();
        }


        public override void OnEnter()
        {
            _playerHandObject.SetActive(true);
            //if (_firstTimeEnter == true)
            //{
            //    _deck.PlayerHandList.AddRange(_handListSave);
            //}
            if (_firstTimeEnter == false)
            {
                _deck.DrawCard();
                _deck.DrawCard();
                _deck.DrawCard();
                _deck.DrawCard();
                _deck.DrawCard();
                _deck.InstantiateHandGOs();
                _firstTimeEnter = true;
            }
        }

        public override void OnExit()
        {
            _playerHandObject.SetActive(false); 
        }

        internal override void HighLightNew(Hex position, Card card)
        {
            var validpositions = _actionManager.ValidPisitionsFor(_playerPiece, position, card._cardType);
            var IsolatedPositions = _actionManager.IsolatedValidPisitionsFor(_playerPiece, position, card._cardType);

            if (!validpositions.Contains(position))
            {
                foreach (var hex in validpositions)
                {
                    hex.Activate();
                }
            }

            if (IsolatedPositions.Contains(position))
            {
                foreach (var hex in IsolatedPositions)
                {
                    hex.Activate();
                }
            };
        }

        internal override void UnHighlightOld(Hex position, Card card)
        {
            var validpositions = _actionManager.ValidPisitionsFor(_playerPiece, position, card._cardType);
            var IsolatedPositions = _actionManager.IsolatedValidPisitionsFor(_playerPiece, position, card._cardType);

            foreach (var hex in validpositions)
            {
                hex.Deactivate();
            }

            foreach (var hex in IsolatedPositions)
            {
                position.Deactivate();
            }
        }

        internal override void OnDrop(Hex position, Card card)
        {
            var validpositions = _actionManager.ValidPisitionsFor(_playerPiece, position, card._cardType);
            var IsolatedPositions = _actionManager.IsolatedValidPisitionsFor(_playerPiece, position, card._cardType);

            if (IsolatedPositions.Contains(position))
            {
                _actionManager.Action(_playerPiece, position, card._cardType);

                _deck.ExecuteCard(card);

            }

            foreach (var hex in validpositions)
            {
                hex.Deactivate();
            }

            StateMachine.MoveToState(GameState.GamePlayState2);

        }

    }



}
