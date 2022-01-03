using UnityEngine;

namespace CardMetaData
{
    [CreateAssetMenu(menuName = "Card/Type/Defense", fileName = "New Defense Card")]
    public class DefenseCard : BaseCardObject
    {
        public Sprite defaultImage;
        public int combatValue = -1;
    
        public override CardData GetCardData()
        {
            if (cardArt == null) cardArt = defaultImage;
            return new CardData(cardArt, cardName, CardData.CardType.Defense, cardEffectText, cardBoostValue, combatValue);
        }
    
        public override void SetCombatValue(int mainValue)
        {
            combatValue += mainValue;

        }
    }
}