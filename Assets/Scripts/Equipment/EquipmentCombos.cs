using System.Collections.Generic;
using CardMetaData;
using UnityEngine;

namespace Equipment
{
    public class EquipmentCombos : MonoBehaviour
    {
        public List<BaseCardObject> unarmedComboCards;


        public List<BaseCardObject> CheckCardCombos(EquipmentData.WeaponComboTag tag1, EquipmentData.WeaponComboTag tag2)
        {
            if (tag1 == EquipmentData.WeaponComboTag.Unarmed && tag2 == EquipmentData.WeaponComboTag.Unarmed)
            {
                return unarmedComboCards;
            }

            return null;
        }
    }
}
