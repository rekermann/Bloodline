using System.Collections.Generic;
using CardMetaData;
using UnityEngine;

namespace EnemyAi
{
    public class EnemyDeck : MonoBehaviour
    {
        private List<BaseCardObject> _cards;
    

        public BaseCardObject GetRandomCard()
        {
            return _cards[Random.Range(0, _cards.Count)];
        }

        public void SetEnemyCards(List<BaseCardObject> cards)
        {
            _cards = cards;
        }
    }
}
