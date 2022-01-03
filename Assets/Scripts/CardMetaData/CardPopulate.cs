using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CardMetaData
{
    public class CardPopulate : MonoBehaviour
    {
        public Sprite[] cardFrames;
        public Image cardFrame;
        public Image cardImage;
        public TextMeshProUGUI cardName;
        public TextMeshProUGUI cardEffectText;
        public TextMeshProUGUI cardBoostValue;
        public TextMeshProUGUI cardCombatValue;
        public BaseCardObject baseCardObject;

        public int combatValue;



        public void SetupCard(BaseCardObject cardObject)
        {
        
            baseCardObject = cardObject;
            SetupCard();
        }
    
        public void SetupCard(int value)
        {
            combatValue += value;
            SetupCard();
        }

        public void SetupZoomedCard(BaseCardObject cardObject)
        {
            //BAD BOY
            combatValue = UiManager.Instance.playerRef.GetComponent<PlayerController>().GetCardValues(cardObject);
            baseCardObject = cardObject;
            SetupCard();
        }
    
    
        private void SetupCard()
        {
            CardData cData = GetCardData();
            cardFrame.sprite = cardFrames[(int) cData.cardType];
            cardImage.sprite = cData.cardArt;
            cardName.text = cData.cardName;

            cardEffectText.text = cData.cardEffectText.Replace("\\n", "\n");
            cardBoostValue.text = cData.cardBoostValue.ToString();
            if (cData.cardCombatValue == -1) { cardCombatValue.text = ""; }
            else { cardCombatValue.text = (combatValue + cData.cardCombatValue).ToString(); }
        
        }


        public CardData GetCardData()
        {
            return baseCardObject.GetCardData();
        }
    }
}
