using UnityEngine;

namespace CardMetaData
{
    [CreateAssetMenu(menuName = "Card/Type/Tactic", fileName = "New Tactic Card")]
    public class TacticCard : BaseCardObject
    {
        public Sprite defaultImage;
        public int damage;
        public override CardData GetCardData()
        {
            if (cardArt == null) cardArt = defaultImage;
            return new CardData(cardArt, cardName, CardData.CardType.Tactic, cardEffectText, cardBoostValue, damage);
        }
    }
}
