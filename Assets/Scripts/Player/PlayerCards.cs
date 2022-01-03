using System.Collections.Generic;
using CardMetaData;
using UnityEngine;

namespace Player
{
     public class PlayerCards : MonoBehaviour
     {
          [SerializeField] private List<BaseCardObject> _cards;

          public List<BaseCardObject> GetPlayerCards()
          {
               return _cards;
          }
          public List<BaseCardObject> GetPlayerCardsShuffled()
          {
               return Deck.Deck.ShuffleDeck(_cards);
          }
          public void SetCards(List<BaseCardObject> cards)
          {
               _cards = cards;
          }

     }
}
