using UiUtilities;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace Equipment
{
    public class EquipmentSlot : MonoBehaviour, IAltHoverable
    {
        [SerializeField] private TooltipPopup _tooltipPopup;
        public Image image;
        public EquipmentData.EquipmentSlot itemSlot;
        private EquipmentData _equipmentData;
        public bool itemEquipped;
        

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
    }
}
