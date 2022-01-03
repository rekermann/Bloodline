using UnityEngine;

namespace CardMetaData
{
    [CreateAssetMenu(menuName = "Card/Type/Agility", fileName = "New Agility Card")]
    public class AgilityCard : BaseCardObject
    {
        public Sprite defaultImage;
        public int moveValue;
        public override CardData GetCardData()
        {
            if (cardArt == null) cardArt = defaultImage;
            return new CardData(cardArt, cardName, CardData.CardType.Agility, cardEffectText, cardBoostValue, moveValue);
        }
    
        public override void SetCombatValue(int mainValue)
        {

            moveValue += mainValue;
        }
    }
}