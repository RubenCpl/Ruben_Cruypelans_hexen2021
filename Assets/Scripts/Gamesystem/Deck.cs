using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DAE.HexSystem;
using System.Linq;

using System;
using Random = UnityEngine.Random;

namespace DAE.Gamesystem
{
    

    public class Deck : MonoBehaviour, IDeck<CardData>
    {
        [SerializeField]
        private List<CardData> _currentDeckList;

        [SerializeField]
        private List<CardData> _startingDeckList;
        
        private List<CardData> _temporaryCardList;

        [SerializeField]
        private List<CardData> _playerhandList1;

        [SerializeField]
        private List<CardData> _playerhandList2;

        [SerializeField]
        private List<CardData> _discardList;

        [SerializeField]
        private GameObject CardBase;

        public GameObject HandView1;
        public GameObject HandView2;


        private List<CardData> templist = new List<CardData>();
        private List<CardData> tempDrawnlist = new List<CardData>();


        //shuffle shit, generate new deck etc
        //public int DeckSize => _decksize;
        public List<CardData> CurrentDeckList => _currentDeckList;
        public List<CardData> StartingDecklist => _startingDeckList;      

        public List<CardData> TemporaryCardsList => _temporaryCardList;

        public List<CardData> PlayerHandList => _playerhandList1;

        public List<CardData> DiscardList => _discardList;



        public void DrawCard()
        {
            List<CardData> playerhandList = new List<CardData>();
            if (HandView1.activeInHierarchy)
            {
                playerhandList = _playerhandList1;
            }
            else if (HandView2.activeInHierarchy)
            {
                playerhandList = _playerhandList2;
            }
            playerhandList.Add(CurrentDeckList[0]);
            if (CurrentDeckList.Count > 0)
            {
                CurrentDeckList.RemoveAt(0);

            }
        }

        public void EqualizeDecks()        
        {         
            CurrentDeckList.AddRange(StartingDecklist);
        }
        public void ShuffleCurrentDeck()
        {
            _currentDeckList.Shuffle();
        }

        //for prototype im working on
        public void  ShuffleStartingDeck()
        {
            _startingDeckList.Shuffle();
        }

        public void ExecuteCard(Card cardo)
        {
            List<CardData> playerhandList = new List<CardData>();
            if (HandView1.activeInHierarchy)
            {
                playerhandList = _playerhandList1;
            }
            else if (HandView2.activeInHierarchy)
            {
                playerhandList = _playerhandList2;
            }

            playerhandList.Remove(cardo.CardData);
            _discardList.Add(cardo.CardData);
            if (CurrentDeckList.Count > 0)
            {
                playerhandList.Add(CurrentDeckList[0]);
                CurrentDeckList.RemoveAt(0);


            }

            cardo.Used();
            ClearHandGO();
            InstantiateHandGOs();

        }

        public void ClearHandGO()
        {
            int childs = HandView1.transform.childCount;
            for (int i = childs -1 ; i >= 0; i--)
            {
                if (HandView1.activeInHierarchy)
                {
                    Destroy(HandView1.transform.GetChild(i).gameObject);
                }  else if (HandView2.activeInHierarchy)
                {
                    Destroy(HandView2.transform.GetChild(i).gameObject);

                }

            }
                    
        }

        public void InstantiateHandGOs()
        {
           List<CardData> playerhandList = new List<CardData>();
            if (HandView1.activeInHierarchy)
            {
                playerhandList = _playerhandList1;
            } else if (HandView2.activeInHierarchy)
            {
                playerhandList = _playerhandList2;
            }

            foreach (var handCard in playerhandList)
            {
                if (HandView1.activeInHierarchy)
                {
                    var cardobject = Instantiate(CardBase, HandView1.transform);
                    cardobject.GetComponent<Card>().InitializeCard(handCard);
                } else if (HandView2.activeInHierarchy)
                {
                    var cardobject = Instantiate(CardBase, HandView2.transform);
                    cardobject.GetComponent<Card>().InitializeCard(handCard); 
                }
            }

        }

      

    }


}


