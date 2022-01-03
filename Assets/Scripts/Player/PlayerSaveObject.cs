using System.Collections.Generic;
using Equipment;
using UnityEngine;

namespace Player
{
    [CreateAssetMenu(menuName = "Player/Character", fileName = "New Character")]
    public class PlayerSaveObject : ScriptableObject
    {
        public int playerHealth;
        public int playerDamage;
        public int playerDefense;
        public int playerArmor;
        public int playerMoveValue;
        public int actionPoints;
        public int boostPoolMax;
        public int startHandAmount;
        
        public EquipmentData playerHeadSlot;
        public EquipmentData playerChestSlot;
        public EquipmentData playerLegsSlot;
        public EquipmentData playerNeckSlot;
        public EquipmentData playerFingerSlot;
        public EquipmentData playerMainHandSlot;
        public EquipmentData playerOffHandSlot;
        public EquipmentData playerPocketSlot;
        public EquipmentData playerFeetSlot;
        
        /*
        public PlayerSaveObject(int playerHealth, int playerDamage, int playerDefense, int playerArmor,
            int playerMoveValue, int actionPoints, int boostPoolMax, int startHandAmount, List<EquipmentData> equipmentList)
        {
            this.playerHealth = playerHealth;
            this.playerDamage = playerDamage;
            this.playerDefense = playerDefense;
            this.playerArmor = playerArmor;
            this.playerMoveValue = playerMoveValue;
            this.actionPoints = actionPoints;
            this.boostPoolMax = boostPoolMax;
            this.startHandAmount = startHandAmount;
            this.equipmentList = equipmentList;
        }
        */

        public List<EquipmentData> GetEquipment()
        {
            List<EquipmentData> returnList = new List<EquipmentData>();
            if(playerHeadSlot != null)
                returnList.Add(playerHeadSlot);
            if(playerChestSlot != null)
                returnList.Add(playerChestSlot);
            if(playerLegsSlot != null)
                returnList.Add(playerLegsSlot);
            if(playerNeckSlot != null)
                returnList.Add(playerNeckSlot);
            if(playerFingerSlot != null)
                returnList.Add(playerFingerSlot);
            if(playerMainHandSlot != null)
                returnList.Add(playerMainHandSlot);
            if(playerOffHandSlot != null)
                returnList.Add(playerOffHandSlot);
            if(playerPocketSlot != null)
                returnList.Add(playerPocketSlot);
            if(playerFeetSlot != null)
                returnList.Add(playerFeetSlot);

            return returnList;
        }

        public PlayerStatsObject GetPlayerStats()
        {
            return new PlayerStatsObject(playerHealth, playerDamage, playerDefense, playerArmor, playerMoveValue,
                actionPoints, boostPoolMax, startHandAmount);
        }
    }

    public class PlayerStatsObject
    {
        public int playerHealth;
        public int playerDamage;
        public int playerDefense;
        public int playerArmor;
        public int playerMoveValue;
        public int actionPoints;
        public int boostPoolMax;
        public int startHandAmount;
        
        public PlayerStatsObject(int playerHealth, int playerDamage,int playerDefense,int playerArmor,
            int playerMoveValue,int actionPoints,int boostPoolMax,int startHandAmount)
        {
            
            this.playerHealth = playerHealth;
            this.playerDamage = playerDamage;
            this.playerDefense = playerDefense;
            this.playerArmor = playerArmor;
            this.playerMoveValue = playerMoveValue;
            this.actionPoints = actionPoints;
            this.boostPoolMax = boostPoolMax;
            this.startHandAmount = startHandAmount;
        }
    }
}
