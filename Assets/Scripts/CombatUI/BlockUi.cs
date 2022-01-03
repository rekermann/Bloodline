using System;
using CardMetaData;
using PlayerHand;
using TMPro;
using UnityEngine;

namespace CombatUI
{
    public class BlockUi : MonoBehaviour
    {
        public GameObject AttackCard;
        public GameObject DefenseCard;
        public TextMeshProUGUI attackAmountText;
        public TextMeshProUGUI defenseAmountText;
        public TextMeshProUGUI healthChangeText;

        private int damage;
        private BaseCardObject blockCard;
        public void Start()
        {
            DefenseCard.GetComponent<CardDropSpot>().OnCardChange += SetupUi;
        }

        public void SetActive(BaseCardObject attackCard)
        {
            gameObject.SetActive(true);
            AttackCard.GetComponent<CardPopulate>().SetupCard(attackCard);
            SetupUi(null);
        }

        public void SetInactive()
        {
            gameObject.SetActive(false);
        }

        public void Confirm()
        {
            SetInactive();
            if(blockCard != null)
                CardManager.Instance.SendToDiscard(blockCard);
            DefenseCard.GetComponent<CardDropSpot>().ClearSlots();
            CombatManager.Instance.BlockDone(damage);
        
        }

        public void SetupUi(BaseCardObject card)
        {
            blockCard = card;
            attackAmountText.text = AttackCard.GetComponent<CardPopulate>().GetCardData().cardCombatValue.ToString();
            if (card != null)
            {
                defenseAmountText.text = card.GetCardData().cardCombatValue.ToString();
            }
            else
            {
                defenseAmountText.text = 0.ToString();
            }
        
            healthChangeText.text = CalculateHealthChange(card).ToString();
        }


        public int CalculateHealthChange(BaseCardObject card)
        {
            int healthChange = 0;
            if (card != null)
            {
                healthChange -= AttackCard.GetComponent<CardPopulate>().GetCardData().cardCombatValue -
                                card.GetCardData().cardCombatValue;
            }
            else
            {
                healthChange -= AttackCard.GetComponent<CardPopulate>().GetCardData().cardCombatValue;
            }

        
            if (healthChange > 0) healthChange = 0;
            damage = Math.Abs(healthChange);
        
            return healthChange;
        }
    
    }
}
