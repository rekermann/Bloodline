using System;
using System.Collections.Generic;
using CardMetaData;
using UnityEngine;

namespace PlayerHand
{
    public class Hand : MonoBehaviour
    {
        private List<BaseCardObject> _cards = new List<BaseCardObject>();
        private int _handLimit = 7;
        private int _cardsInhand;
    
        private List<GameObject> cardsRefInHand = new List<GameObject>();

        public void Start()
        {
            CardManager.Instance.handUi.GetComponent<CardDropSpot>().OnCardChange += AddCard;
        }


        public void AddCard(BaseCardObject card)
        {   
        
            if (card == null)
            {
                return;
            }
            _cardsInhand++;
            _cards.Add(card);
        }

        public void RemoveCard(BaseCardObject card)
        {
        
            if(card == null) return;
            _cardsInhand--;
            _cards.Remove(card);
        }

        public bool CheckIfFull()
        {
            return _cardsInhand >= _handLimit;
        }

        public bool HasCardType(CardData.CardType cardType)
        {
            foreach (var card in _cards)
            {
                if (card.GetCardData().cardType == cardType) return true;
            }

            return false;
        }

        public List<BaseCardObject> GetHand()
        {
            return _cards;
        }


        public bool CheckIfCanPlayCard()
        {
            foreach (var card in _cards)
            {
                if (card.cardCost == 0) return true;
            }

            return false;
        }
    }
}
