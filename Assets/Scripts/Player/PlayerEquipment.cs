using System;
using System.Collections.Generic;
using CardMetaData;
using Equipment;
using UnityEngine;

namespace Player
{
    public class PlayerEquipment : MonoBehaviour
    {
        public List<EquipmentData> equipmentDataList = new List<EquipmentData>();
        
        public void SetEquipment(List<EquipmentData> equipmentList)
        {
            equipmentDataList = equipmentList;
            foreach (var item in equipmentDataList)
            {
                SetEquipmentUi(item);
            }
        }

        public void SetEquipmentUi(EquipmentData item)
        {
            EquipmentSlot itemSlot = UiManager.Instance.equipmentManager.GetEquipmentSlot(item.eSlot);
            if(itemSlot != null) itemSlot.SetItem(item);
        }

        public List<BaseCardObject> GetEquipmentCards()
        {
            List<BaseCardObject> equipmentCards = new List<BaseCardObject>();
            List<EquipmentData.WeaponComboTag> tags = new List<EquipmentData.WeaponComboTag>();
            foreach (var equipment in equipmentDataList)
            {
                if (equipment.eSlot == EquipmentData.EquipmentSlot.EitherHand ||
                    equipment.eSlot == EquipmentData.EquipmentSlot.MainHand
                    || equipment.eSlot == EquipmentData.EquipmentSlot.OffHand)
                {
                    tags.Add(equipment.eWeaponComboTag);
                }
                equipmentCards.AddRange(equipment.eCards);
            }

            if (tags.Count > 1)
            {
                equipmentCards.AddRange(UiManager.Instance.equipmentCombos.CheckCardCombos(tags[0], tags[1]));
            }
            
            return equipmentCards;
        }

        public void GetStats(out int defense, out int damage, out int health, out int move, out int startCards, out int drawPerTurn)
        {
            defense = 0;
            damage = 0;
            health = 0;
            move = 0;
            startCards = 0;
            drawPerTurn = 0;
            

            foreach (var equipment in equipmentDataList)
            {
                if (!equipment) continue;

                defense += equipment.eArmor;
                damage += equipment.eDamage;
                health += equipment.eHealth;
                move += equipment.eMove;
                startCards += equipment.startCardDraw;
                drawPerTurn += equipment.drawPerTurn;

            }
        }
    }
}