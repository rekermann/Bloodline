using System;
using UnityEngine;

namespace CardMetaData
{
    public class CardData
    {
        public Sprite cardArt;
        public String cardName;
        public CardType cardType;
        public String cardEffectText;
        public int cardBoostValue;
        public int cardCombatValue;

        public enum CardType
        {
            Attack,
            Defense,
            Versatile,
            Tactic,
            Agility,
            Resource,
            Trigger
        }

        public CardData(Sprite cardArt, String cardName, CardType cardType, String cardEffectText, int cardBoostValue, int cardCombatValue = -1)
        {
            this.cardArt = cardArt;
            this.cardName = cardName;
            this.cardType = cardType;
            this.cardEffectText = cardEffectText;
            this.cardBoostValue = cardBoostValue;
            this.cardCombatValue = cardCombatValue;
        }

    }
}
