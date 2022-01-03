using UnityEngine;

namespace CardMetaData
{
    [CreateAssetMenu(menuName = "Card/Type/Versatile", fileName = "New Versatile Card")]
    public class VersatileCard : BaseCardObject
    {
        public Sprite defaultImage;
        public int combatValue = -1;
        public override CardData GetCardData()
        {
            if (cardArt == null) cardArt = defaultImage;
            return new CardData(cardArt, cardName, CardData.CardType.Versatile, cardEffectText, cardBoostValue, combatValue);
        }

        public override void SetCombatValue(int mainValue)
        {
            combatValue += mainValue;
        }
    }
}
