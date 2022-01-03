using System.Collections.Generic;
using UnityEngine;

namespace Equipment
{
    public class EquipmentManager : MonoBehaviour
    {
        public List<EquipmentSlot> equipmentSlots;
        public GameObject backpackUi;

        public EquipmentSlot GetEquipmentSlot(EquipmentData.EquipmentSlot eSlot)
        {
            foreach (var item in equipmentSlots)
            {
                if (!item.itemEquipped && IsLegalSlot(item.itemSlot, eSlot)) 
                {
                    
                    return item;
                }
            }
            
            return null;
        }

        private bool IsLegalSlot(EquipmentData.EquipmentSlot item1 ,EquipmentData.EquipmentSlot item2)
        {
            
            if (item1 == item2) return true;

            if (item2 == EquipmentData.EquipmentSlot.EitherHand && (item1 == EquipmentData.EquipmentSlot.MainHand ||
                                                                    item1 == EquipmentData.EquipmentSlot.OffHand))
            {
                return true;
            }
               

            return false;
        }

        public void ToggleEquipmentUi()
        {
            gameObject.SetActive(!gameObject.activeSelf);
            backpackUi.SetActive(false);
        }

        public void ToggleBackpackUi()
        {
            backpackUi.SetActive(!backpackUi.activeSelf);
        }
        
    }
}
