using UnityEngine;

namespace CardMetaData
{
    [CreateAssetMenu(menuName = "Card/Type/Resource", fileName = "New Resource Card")]
    public class ResourceCard : BaseCardObject
    {
        public Sprite defaultImage;
        public int mainValue;
        public override CardData GetCardData()
        {
            if (cardArt == null) cardArt = defaultImage;
            return new CardData(cardArt, cardName, CardData.CardType.Resource, cardEffectText, cardBoostValue, mainValue);
        }
    }
}