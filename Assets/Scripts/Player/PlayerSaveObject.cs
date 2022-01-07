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
        public int drawPerTurn;
        
        public EquipmentData playerHeadSlot;
        public EquipmentData playerChestSlot;
        public EquipmentData playerLegsSlot;
        public EquipmentData playerNeckSlot;
        public EquipmentData playerFingerSlot;
        public EquipmentData playerMainHandSlot;
        public EquipmentData playerOffHandSlot;
        public EquipmentData playerPocketSlot;
        public EquipmentData playerFeetSlot;

        public List<EquipmentData> backpackItemList = new List<EquipmentData>();

        
        
        
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

            foreach (var backpackItem in backpackItemList)
            {
                if(backpackItem != null) returnList.Add(backpackItem);
            }

            return returnList;
        }

        public PlayerSaveObj GetPlayerStats()
        {
            return new PlayerSaveObj(playerHealth, playerDamage, playerDefense, playerArmor, playerMoveValue,
                actionPoints, boostPoolMax, startHandAmount, drawPerTurn, GetEquipment());
        }

        public void SavePlayerStats(PlayerSaveObj obj)
        {
            playerHealth = obj.playerHealth;
            playerDamage = obj.playerDamage;
            playerDefense = obj.playerDefense;
            playerArmor = obj.playerArmor;
            playerMoveValue = obj.playerMoveValue;
            actionPoints = obj.actionPoints;
            boostPoolMax = obj.boostPoolMax;
            startHandAmount = obj.startHandAmount;
            drawPerTurn = obj.drawPerTurn;

            playerHeadSlot = null;
            playerChestSlot = null;
            playerLegsSlot = null;
            playerNeckSlot = null;
            playerFingerSlot = null;
            playerMainHandSlot = null;
            playerOffHandSlot = null;
            playerPocketSlot = null;
            playerFeetSlot = null;
            backpackItemList.Clear();
            
            foreach (var equipment in obj.equipmentList)
            {
                if (equipment.eSlot == EquipmentData.EquipmentSlot.Head) playerHeadSlot = equipment;
                if (equipment.eSlot == EquipmentData.EquipmentSlot.Chest) playerChestSlot = equipment;
                if (equipment.eSlot == EquipmentData.EquipmentSlot.Legs) playerLegsSlot = equipment;
                if (equipment.eSlot == EquipmentData.EquipmentSlot.Neck) playerNeckSlot = equipment;
                if (equipment.eSlot == EquipmentData.EquipmentSlot.Finger) playerFingerSlot = equipment;
                if (equipment.eSlot == EquipmentData.EquipmentSlot.Pocket) playerPocketSlot = equipment;
                if (equipment.eSlot == EquipmentData.EquipmentSlot.Feet) playerFeetSlot = equipment;
                if (equipment.eSlot == EquipmentData.EquipmentSlot.Backpack) backpackItemList.Add(equipment);
                
                if (equipment.eSlot == EquipmentData.EquipmentSlot.MainHand || 
                    equipment.eSlot == EquipmentData.EquipmentSlot.EitherHand || 
                    equipment.eSlot == EquipmentData.EquipmentSlot.TwoHand)
                {
                    if(playerMainHandSlot == null)
                        playerMainHandSlot = equipment;
                }
                else if (equipment.eSlot == EquipmentData.EquipmentSlot.MainHand || 
                    equipment.eSlot == EquipmentData.EquipmentSlot.EitherHand)
                {
                    if(playerOffHandSlot == null)
                        playerOffHandSlot = equipment;
                }
            }

            
            
            
        }
    }

    public class PlayerSaveObj
    {
        public int playerHealth;
        public int playerDamage;
        public int playerDefense;
        public int playerArmor;
        public int playerMoveValue;
        public int actionPoints;
        public int boostPoolMax;
        public int startHandAmount;
        public int drawPerTurn;
        public List<EquipmentData> equipmentList;

        public PlayerSaveObj(int playerHealth, int playerDamage,int playerDefense,int playerArmor,
            int playerMoveValue,int actionPoints,int boostPoolMax,int startHandAmount, int drawPerTurn, List<EquipmentData> equipmentList)
        {
            
            this.playerHealth = playerHealth;
            this.playerDamage = playerDamage;
            this.playerDefense = playerDefense;
            this.playerArmor = playerArmor;
            this.playerMoveValue = playerMoveValue;
            this.actionPoints = actionPoints;
            this.boostPoolMax = boostPoolMax;
            this.startHandAmount = startHandAmount;
            this.drawPerTurn = drawPerTurn;
            this.equipmentList = equipmentList;
        }

        public void SetEquipment(List<EquipmentData> equipmentData)
        {
            equipmentList = equipmentData;
        }
    }
}
