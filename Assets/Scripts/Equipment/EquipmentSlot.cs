using System;
using UiUtilities;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utilities;

namespace Equipment
{
    public class EquipmentSlot : MonoBehaviour, IAltHoverable, IPointerClickHandler
    {
        [SerializeField] private TooltipPopup _tooltipPopup;
        public Image image;
        public EquipmentData.EquipmentSlot itemSlot;
        public EquipmentData _equipmentData;
        public bool itemEquipped;

        public void Start()
        {
            if (_equipmentData)
            {
                SetItem(_equipmentData);
            }
        }

        public void SetItem(EquipmentData item)
        {
            image.sprite = item.eSprite;
            image.color = new Color
            {
                r = 255,
                g = 255,
                b = 255,
                a = 255,
            };
            itemEquipped = true;
            _equipmentData = item;
        }

        public void AltHovered()
        {
            
            _tooltipPopup.DisplayInfo(_equipmentData);
        }

        public void AltUnHovered()
        {
            _tooltipPopup.HideInfo();
        }

        public EquipmentData GetEquippedItemData()
        {
            return _equipmentData;
        }

        public void Unequip()
        {
            image.sprite = null;
            image.color = new Color
            {
                r = 255,
                g = 255,
                b = 255,
                a = 0,
            };
            itemEquipped = false;
            _equipmentData = null;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            GetSetSwapDragItem();
        }

        public void GetSetSwapDragItem()
        {
            if (EquipmentManager.instance.currentDragItem != null)
            {
                EquipmentData newItem =
                    EquipmentManager.instance.currentDragItem.GetComponent<DraggableSlot>().GetItem();
                if (!CheckIfItemFit(newItem)) return;
                Destroy(EquipmentManager.instance.currentDragItem);
                EquipmentManager.instance.currentDragItem = null;
                if(itemEquipped) EquipmentManager.instance.MoveItem(this);
                SetItem(newItem);
                
            }
            else
            {
                if(!itemEquipped) return;
                EquipmentManager.instance.MoveItem(this);
            }
        }

        public bool CheckIfItemFit(EquipmentData item)
        {
            if (item.eSlot == itemSlot || itemSlot == EquipmentData.EquipmentSlot.Backpack) { return true; }

            if ((item.eSlot == EquipmentData.EquipmentSlot.EitherHand ||
                 item.eSlot == EquipmentData.EquipmentSlot.TwoHand) &&
                (itemSlot == EquipmentData.EquipmentSlot.MainHand ||
                itemSlot == EquipmentData.EquipmentSlot.OffHand)) return true;
            return false;
        }


    }
}
