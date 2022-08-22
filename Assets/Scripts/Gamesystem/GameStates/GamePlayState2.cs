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
    class GamePlayState2 : GameStateBase
    {

        private ActionManager<Card, Piece> _actionManager;
        private Board<IHex, Piece> _board;
        private Deck _deck;


        private Piece _playerPiece;

        public GamePlayState2(StateMachine<GameStateBase> stateMachine, Board<IHex, Piece> board, ActionManager<Card, Piece> moveManager, PlayerHand playerhand, Deck deck, Piece playerPiece) : base(stateMachine)
        {
            _playerPiece = playerPiece;

            _deck = deck;
            _actionManager = moveManager;
            _board = board;
            _deck.EqualizeDecks();
            _deck.ShuffleCurrentDeck();

            _deck.InstantiateHandGOs();


        }

        public override void OnEnter()
        {
            _deck.DrawCard();
            _deck.DrawCard();
            _deck.DrawCard();
            _deck.DrawCard();
            _deck.DrawCard();

            _deck.InstantiateHandGOs();

        }

        public override void OnExit()
        {
            _deck.CurrentDeckList.InsertRange(0, _deck.PlayerHandList);
            _deck.PlayerHandList.Clear();
            _deck.ClearHandGO();
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

            StateMachine.MoveToState(GameState.GamePlayState);

        }

    }



}