using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using CardMetaData;
using UnityEngine;

namespace Equipment
{
    [CreateAssetMenu(menuName = "Equipment", fileName = "New Equipment")]
    public class EquipmentData : ScriptableObject
    {
        public Sprite eSprite;
        public enum EquipmentSlot
        {
            Head,
            Chest,
            Legs,
            Feet,
            Finger,
            Pocket,
            OffHand,
            MainHand,
            TwoHand,
            EitherHand,
            Neck,
            Backpack
        }

        public enum EquipmentRarity
        {
            Ordinary,
            Unusual,
            Rare,
            Epic,
            Unique,
            Legendary
        
        }
        




        public enum WeaponComboTag
        {
            None,
            Unarmed,
            Sword,
            Blunt,
            Shield,
        }
    
    

        [SerializeField] private string eName;

        [SerializeField] public string eText;



    
        public EquipmentRarity eRarity;
        public EquipmentSlot eSlot;
        public int eArmor;
        public int eDamage;
        public int eMove;
        public int eHealth;
        public int startCardDraw;
        public int drawPerTurn;

        public List<string> specialEffects;
        public WeaponComboTag eWeaponComboTag;
        public List<BaseCardObject> eCards = new List<BaseCardObject>();


        
        public string NameColorRarity
        {
            get
            {
                string hexColor = ColorUtility.ToHtmlStringRGB(GetRarityColor(this.eRarity));
                return $"<size=50><color=#{hexColor}>{EName}</color></size>";
            }
        }

                
        public string EText => eText;
        
        public string EName => eName;

        /*
         * 
    
        public EquipmentRarity eRarity;
        public EquipmentSlot eSlot;
        public int eArmor;
        public int eDamage;
        public int eMove;
        public int eHealth;

         */
        
        public string GetTooltipInfoText()
        {
            StringBuilder builder = new StringBuilder();

            
            builder.Append(NameColorRarity).AppendLine();
            builder.Append("<color=green>");
            if (eArmor != 0)
            {
                builder.Append("Armor: " + eArmor.ToString("+0;-#")).AppendLine();
            }
            if (eDamage != 0)
            {
                builder.Append("Damage: " + eDamage.ToString("+0;-#")).AppendLine();
            }
            if (eMove != 0)
            {
                builder.Append("Move: " + eMove.ToString("+0;-#")).AppendLine();
            }
            if (eHealth != 0)
            {
                builder.Append("Health: " + eHealth.ToString("+0;-#")).AppendLine();
            }
            if (startCardDraw != 0)
            {
                builder.Append("Start Cards: " + startCardDraw.ToString("+0;-#")).AppendLine();
            }
            if (drawPerTurn != 0)
            {
                builder.Append("Draw per turn: " + drawPerTurn.ToString("+0;-#")).AppendLine();
            }
            
            
            builder.Append("</color>");
            if (eCards.Count > 0)
            {
                builder.Append("<color=orange>").AppendLine();
                int count=1;
                BaseCardObject prevCard = null;
                foreach (var card in eCards)
                {
                    if (prevCard == card)
                    {
                        count++;
                    }
                    else if(prevCard != null)
                    {
                        builder.Append(count.ToString("+0;-#") + " " + prevCard.cardName).AppendLine();
                        count = 1;
                    }

                    prevCard = card;
                }
                if(prevCard != null)
                    builder.Append(count.ToString("+0;-#") + " " + prevCard.cardName).AppendLine();
            
                builder.Append("</color>");
            }
            
            builder.Append(EText);
            
            return builder.ToString();
        }
        
        public Color GetRarityColor(EquipmentRarity rarity)
        {
            switch (rarity)
            {
                case EquipmentRarity.Ordinary:
                    return Color.white;
                case EquipmentRarity.Unusual:
                    return Color.blue;
                case EquipmentRarity.Rare:
                    return Color.cyan;
                case EquipmentRarity.Epic:
                    return Color.yellow;
                case EquipmentRarity.Unique:
                    return Color.magenta;
                case EquipmentRarity.Legendary:
                    return Color.red;

            }

            return Color.white;
        }
    }
}
