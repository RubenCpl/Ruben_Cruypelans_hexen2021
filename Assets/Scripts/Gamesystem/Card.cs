using DAE.HexSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DAE.Gamesystem
{
    public class CardEventArgs : EventArgs
    {
        public Card Card { get; }
        public CardEventArgs(Card card) => Card = card;
    }

    public class Card : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, ICard
    {
        [SerializeField] private string _name;
        [SerializeField] private string _description;      
        [SerializeField] private Texture2D _cardTexture;     

        [SerializeField] public GameObject CardImage;      
        [SerializeField] public GameObject TitleText;
        [SerializeField] public GameObject DiscriptionText;
        [SerializeField] public CardType _cardType;

        public CardData _cardData;

        public Transform parentToReturnTo = null;
        public Transform placeholderParent = null;

        GameObject placeholder = null;

        public string Name => _name;
        public CardType CardType { get; set; }
        public Texture2D CardTexture => _cardTexture;
        public string Description => _description;
        public CardData CardData => _cardData;



        public void InitializeCard(CardData data)
        {
            _name = data._name;
            _description = data._description;
            _cardType = data._cardType;
            _cardTexture = data._cardTexture;
            _cardData = data;


            CardImage.GetComponent<RawImage>().texture = CardTexture;
            TitleText.GetComponent<Text>().text = _name;
            DiscriptionText.GetComponent<Text>().text = _description;
        }

        public void Used()
        {
            //send to discardPile.
            Destroy(this.gameObject);
            Destroy(placeholder);
        }
       
        
        public event EventHandler<CardEventArgs> BeginDrag;
        public event EventHandler<CardEventArgs> Dragging;
        public event EventHandler<CardEventArgs> EndDrag;
        

        public void OnBeginDrag(PointerEventData eventData)
        {
            //Debug.Log($"OnBeginDrag {CardType}");

            //gameObject.SetActive(false);

            placeholder = new GameObject();
            placeholder.transform.SetParent(this.transform.parent);
            LayoutElement le = placeholder.AddComponent<LayoutElement>();
            le.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
            le.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
            le.flexibleWidth = 0;
            le.flexibleHeight = 0;

            placeholder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());

            parentToReturnTo = this.transform.parent;
            placeholderParent = parentToReturnTo;
            this.transform.SetParent(this.transform.parent.parent);

            GetComponent<CanvasGroup>().blocksRaycasts = false;

            OnBeginDragging(this, new CardEventArgs(this));
        }

        public void OnDrag(PointerEventData eventData)
        {
            //Debug.Log("OnDrag");

            this.transform.position = eventData.position;

            if (placeholder.transform.parent != placeholderParent)
                placeholder.transform.SetParent(placeholderParent);

            int newSiblingIndex = placeholderParent.childCount;

            for (int i = 0; i < placeholderParent.childCount; i++)
            {
                if (this.transform.position.x < placeholderParent.GetChild(i).position.x)
                {

                    newSiblingIndex = i;

                    if (placeholder.transform.GetSiblingIndex() < newSiblingIndex)
                        newSiblingIndex--;

                    break;
                }
            }

            placeholder.transform.SetSiblingIndex(newSiblingIndex);

            OnDragging(this, new CardEventArgs(this));
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("OnEndDrag");
            //gameObject.SetActive(true);
            this.transform.SetParent(parentToReturnTo);
            this.transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());
            GetComponent<CanvasGroup>().blocksRaycasts = true;

            Destroy(placeholder);
        }

        
        protected virtual void OnBeginDragging(object source, CardEventArgs e)
        {
            var handler = BeginDrag;
            handler?.Invoke(this, e);
        }
        protected virtual void OnDragging(object source, CardEventArgs e)
        {
            var handler = Dragging;
            handler?.Invoke(this, e);
        }
        protected virtual void OnEndDragging(object source, CardEventArgs e)
        {
            var handler = EndDrag;
            handler?.Invoke(this, e);
        }


    }
}

