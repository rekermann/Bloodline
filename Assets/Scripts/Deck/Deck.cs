using System.Collections.Generic;
using CardMetaData;
using PlayerHand;
using UnityEngine;
using Random = System.Random;

namespace Deck
{
    public class Deck : MonoBehaviour
    {
        private List<BaseCardObject> _cards;
        private static Random rng = new Random();
        public delegate void DeckChange();
        public event DeckChange OnDeckClicked = () => {};
        public BaseCardObject struggle;


        public void Clicked()
        {
            OnDeckClicked();
        }
    
        public BaseCardObject DrawCard()
        {
            BaseCardObject returnCard = null;
            if(_cards.Count == 0)  { returnCard =  struggle;}
            else
            {
                returnCard = _cards[0];
                _cards.RemoveAt(0);
            }
            CardManager.Instance.AddCardToUi(returnCard);
            return returnCard;
        
        }
    
        public static List<BaseCardObject> ShuffleDeck(List<BaseCardObject> list)  
        {  
            int n = list.Count;  
            while (n > 1) {  
                n--;  
                int k = rng.Next(n + 1);  
                (list[k], list[n]) = (list[n], list[k]);
            }
            
            return list;
        }

        public void SetDeck(List<BaseCardObject> cards)
        {
            _cards = cards;
            _cards = ShuffleDeck(_cards);
        }

        public BaseCardObject DrawToDiscard()
        {
            if (_cards.Count == 0) return null;
            CardManager.Instance.SendToDiscard(_cards[0]);
            BaseCardObject returnCard = _cards[0];
            _cards.RemoveAt(0);
            return returnCard;
        }
    }
}
