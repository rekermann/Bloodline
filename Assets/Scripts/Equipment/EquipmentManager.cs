using System;
using System.Collections.Generic;
using CardMetaData;
using UnityEngine;

namespace Equipment
{
    public class EquipmentManager : MonoBehaviour
    {
        public List<EquipmentSlot> equipmentSlots;
        public GameObject backpackUi;
        public GameObject draggableInventorySlot;
        public static EquipmentManager instance;
        public GameObject currentDragItem;

        public void Start()
        {
            instance = this;
            SetEquipment(GameManager.Instance.GetSave().equipmentList);
        }

        public void Update()
        {
            if (currentDragItem != null)
            {
                currentDragItem.transform.position = Input.mousePosition;
            }
            
        }

        public void SaveEquipment()
        {
            GameManager.Instance.SavePlayerEquipment(GetEquippedItems());
        }



        public List<EquipmentData> GetEquippedItems()
        {
            List<EquipmentData> list = new List<EquipmentData>();
            foreach (var slot in equipmentSlots)
            {
                if (slot.itemEquipped)
                {
                    list.Add(slot._equipmentData);
                }
                
            }

            return list;
        }

        public void SetEquipment(List<EquipmentData> equipmentList)
        {
            foreach (var item in equipmentList)
            {
                SetEquipmentUi(item);
            }
        }

        public void SetEquipmentUi(EquipmentData item)
        {
            EquipmentSlot itemSlot = GetEquipmentSlot(item.eSlot);
            if(itemSlot != null) itemSlot.SetItem(item);
        }


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
            
            if (item2 == EquipmentData.EquipmentSlot.TwoHand && item1 == EquipmentData.EquipmentSlot.MainHand)
                return true;
               

            return false;
        }

        public void RemoveResourceItem(BaseCardObject card)
        {
            foreach (var item in equipmentSlots)
            {
                if (item.itemSlot == EquipmentData.EquipmentSlot.Backpack) 
                {
                    //[0]Bad
                    if(item.GetEquippedItemData() == null) continue;
                    if (item.GetEquippedItemData().eCards[0] == card)
                    {
                        equipmentSlots.Remove(item);
                        item.Unequip();
                        break;
                    }
                }
            }
            
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
        
        public void MoveItem(EquipmentSlot slot)
        {
            if(currentDragItem != null) return;
            currentDragItem = Instantiate(draggableInventorySlot, gameObject.transform.parent, true);
            currentDragItem.transform.position = Input.mousePosition;
            currentDragItem.GetComponent<DraggableSlot>().SetItem(slot.GetEquippedItemData());
            slot.Unequip();
        }
        
        
    }
}
