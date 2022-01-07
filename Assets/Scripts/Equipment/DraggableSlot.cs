using UnityEngine;
using UnityEngine.UI;

namespace Equipment
{
    public class DraggableSlot : MonoBehaviour
    {
        private EquipmentData _equipmentData;
        public Image image;


        public void SetItem(EquipmentData item)
        {
            _equipmentData = item;
            image.sprite = item.eSprite;
            image.color = new Color
            {
                r = 255,
                g = 255,
                b = 255,
                a = 255,
            };
        }

        public EquipmentData GetItem()
        {
            
            return _equipmentData;
        }

    }
}
