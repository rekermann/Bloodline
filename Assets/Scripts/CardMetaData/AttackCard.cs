using UnityEngine;

namespace CardMetaData
{
    [CreateAssetMenu(menuName = "Card/Type/Attack", fileName = "New Attack Card")]
    public class AttackCard : BaseCardObject
    {
        public Sprite defaultImage;
        public int combatValue = -1;
        public override CardData GetCardData()
        {
            if (cardArt == null) cardArt = defaultImage;
            return new CardData(cardArt, cardName, CardData.CardType.Attack, cardEffectText, cardBoostValue, combatValue);
        }
    
        public override void SetCombatValue(int mainValue)
        {

            combatValue += mainValue;
        }
    }
}