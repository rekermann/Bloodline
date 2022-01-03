using System;
using CardMetaData;
using UnityEngine;

namespace PlayerHand
{
    public class CardManager : MonoBehaviour
    {
        public static CardManager Instance;
        public CardDropSpot cardDropSpot;
        [NonSerialized] public bool inPlayZone;
        public HandUi handUi;
        public DiscardPile discardPile;
        public DiscardUi discardUi;


        public void Awake()
        {
            if (Instance != null) {Destroy(gameObject); }
            Instance = this;
        }
    
        public void SetInPlayZone()
        {
            inPlayZone = true;
        }
    
        public void SetOutsidePlayZone()
        {
            inPlayZone = false;
        }

        public void UpdateHandUi()
        {
            handUi.UpdateLayout();
        }
    
    
        public void AddCardToUi(BaseCardObject cardObject)
        {
            handUi.AddCard(cardObject);


            //hand.AddCard(cardObject.GetCardData());

        }

        public void SendToDiscard(BaseCardObject card)
        {
            discardUi.AddCard(card);
            discardPile.AddCardToDiscard(card);
        }

        public void GetCardsInHand()
        {
        
        }
    }
}
