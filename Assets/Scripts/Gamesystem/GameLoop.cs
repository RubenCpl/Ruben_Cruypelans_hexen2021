using DAE.BoardSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DAE.HexSystem;
using DAE.StateSystem;
using DAE.GameSystem.GameStates;

namespace DAE.Gamesystem
{
    public class GameLoop : MonoBehaviour
    {
        [SerializeField]
        private PositionHelper _positionHelper;
        [SerializeField]
        private GenerateBoard _generateboard;
        [SerializeField]
        private Transform _boardParent;

        [Header("GenerateBoard")]
        public int Rows = 3;
        public int Columns = 3;
        public int Tileradius = 1;
        public GameObject hex;
        public GenerationShapes MapShape = GenerationShapes.Hexagon;

        public Deck _deckview;
        //public Deck _deckview2;

        public PlayerHand _playerhand;
        public PlayerHand _playerhand2;

        //public GameObject player1;
        //public GameObject player2;

        private ActionManager<Card, Piece> _actionManager;

        public Card _currentCard;

        private Grid<IHex> _grid;
        private Board<IHex, Piece> _board;

        public Piece Player;
        //public Piece Player2;

        private StateMachine<GameStateBase> _gameStateMachine;

        //public CanvasGroup StartScreen;
        //public CanvasGroup EndScreen;
        public CanvasGroup PauseScreen;
        void Start()
        {
            _positionHelper.TileRadius = Tileradius;
            _generateboard.GenerateBoardView(Rows, Columns, 1, MapShape, _positionHelper, hex, _boardParent);

            _grid = new Grid<IHex>(Rows, Columns);
            ConnectGrid(_grid);
            _board = new Board<IHex, Piece>();
            ConnectPiece(_grid, _board);


            _actionManager = new ActionManager<Card, Piece>(_board, _grid);


            _gameStateMachine = new StateMachine<GameStateBase>();
            _gameStateMachine.Register(GameState.GamePlayState, new GamePlayState(_gameStateMachine, _board, _actionManager, _playerhand, _deckview, PauseScreen, Player));
            //_gameStateMachine.Register(GameState.GamePlayState2, new GamePlayState2(_gameStateMachine, _board, _actionManager, _playerhand2, _deckview2, PauseScreen, player2, Player2));

            //_gameStateMachine.Register(GameState.GamePauseState, new PauseScreenState(_gameStateMachine, PauseScreen));

            //_gameStateMachine.Register(GameState.GamePlayState, new GamePlayState(_gameStateMachine, _board, _actionManager, _playerhand, _deckview, EndScreen));


            //_gameStateMachine.Register(GameState.StartScreenState, new StartScreenState(_gameStateMachine));
            //_gameStateMachine.Register(GameState.EndScreenState, new EndScreenState(_gameStateMachine));
            //_gameStateMachine.InitialState = GameState.StartScreenState;

            _gameStateMachine.InitialState = GameState.GamePlayState;

            GridListeners();
            BoardListereners();
        }

        private void GridListeners()
        {
            _grid.destroyed += (s, e) =>
            {
                foreach (var hex in e.DestroyList)
                {
                    hex.Destroy();
                }
            };
        }

        private void BoardListereners()
        {
            _board.moved += (s, e) =>
            {
                if (_grid.TryGetCoordinateOf(e.ToPosition, out var toCoordinate))
                {
                    var worldPosition = _positionHelper.ToWorldPosition(_boardParent, toCoordinate.x, toCoordinate.y);

                    e.Piece.MoveTo(worldPosition);
                }
            };

            _board.placed += (s, e) =>
            {

                if (_grid.TryGetCoordinateOf(e.ToPosition, out var toCoordinate))
                {
                    var worldPosition = _positionHelper.ToWorldPosition(_boardParent, toCoordinate.x, toCoordinate.y);


                    e.Piece.Place(worldPosition);
                }

            };

            _board.taken += (s, e) =>
            {
                e.Piece.Taken();

                //if (e.Piece.PieceType == pieceType.player)
                //{
                //    _gameStateMachine.CurrentState.EndGame();
                //}
            };
        }

        private void ConnectGrid(Grid<IHex> grid)
        {
            var hexes = FindObjectsOfType<Hex>();
            foreach (var hex in hexes)
            {

                hex.Dropped += (s, e) => _gameStateMachine.CurrentState.OnDrop(hex, e.Card); //hex roept iets is gedropt op mij, gameloop zegt tegen state machine voer uit
                hex.Entered += (s, e) => _gameStateMachine.CurrentState.HighLightNew(hex, e.Card);
                hex.Exitted += (s, e) => _gameStateMachine.CurrentState.UnHighlightOld(hex, e.Card);


                var gridpos = _positionHelper.ToGridPosition(_boardParent, hex.transform.position);

                grid.Register((int)gridpos.x, (int)gridpos.y, hex);

                hex.gameObject.name = $"tile {(int)gridpos.x},{(int)gridpos.y}";
            }
        }

        private void ConnectPiece(Grid<IHex> grid, Board<IHex, Piece> board)
        {
            var pieces = FindObjectsOfType<Piece>();
            foreach (var piece in pieces)
            {
                var gridpos = _positionHelper.ToGridPosition(_boardParent, piece.transform.position);
                if (grid.TryGetPositionAt((int)gridpos.x, (int)gridpos.y, out var position))
                {
                    board.Place(piece, position);
                }
            }
        }

        //public void StartGame()
        //{
        //    _gameStateMachine.CurrentState.StartGame();
        //    StartScreen.alpha = 0;
        //    StartScreen.blocksRaycasts = false;
        //    StartScreen.interactable = false;

        //}


        //public void UnPauseGame()
        //{
        //    _gameStateMachine.CurrentState.UnPauseGame();
        //    //PauseScreen.alpha = 0;
        //    //PauseScreen.blocksRaycasts = false;
        //    //PauseScreen.interactable = false;
        //}

        //public void PauseGame()
        //{
        //    _gameStateMachine.CurrentState.PauseGame();
        //}

    }

}