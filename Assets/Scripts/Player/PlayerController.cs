using System;
using System.Collections;
using System.Collections.Generic;
using CardMetaData;
using Player;
using PlayerHand;
using Tiles;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
     public PlayerMovementController playerMovementController;
     public PlayerCards playerCards;
     public PlayerEquipment playerEquipment;
     public Hand hand;
     private Deck.Deck deck;
     private int playerHealth = 0;
     private int _maxHealth;
     private int playerDamage = 0;
     private int playerDefense = 0;
     private  int playerArmor = 0;
     private int playerMoveValue = 0;
     private int actionPoints;
     private int _maxActionPoints;
     private int boostPoolMax = 0;
     private int startHandAmount = 0;
     private int drawPerTurn = 0;
     private bool fastTurn = true;
     [NonSerialized] public int boostPool;
     

     public delegate void PlayerActions();
     public event PlayerActions OnPlayerDone = () => {};


     private void Start()
     {
          
          SetPlayerStats();

          deck = UiManager.Instance.deck;
          playerCards.SetCards(playerEquipment.GetEquipmentCards());
          deck.SetDeck(playerCards.GetPlayerCardsShuffled());
          deck.OnDeckClicked += DrawCard;
          DrawCard(startHandAmount);
          CombatManager.Instance.UpdatePlayerHealthBar(playerHealth, _maxHealth);
          StartOfTurn();
     }

     private void SetPlayerStats()
     {
          PlayerStatsObject playerStats = GameManager.Instance.playerSaveObject.GetPlayerStats();
          playerHealth = playerStats.playerHealth;
          actionPoints = playerStats.actionPoints;
          playerArmor = playerStats.playerArmor;
          playerDamage = playerStats.playerDamage;
          playerDefense = playerStats.playerDefense;
          boostPoolMax = playerStats.boostPoolMax;
          playerMoveValue = playerStats.playerMoveValue;
          startHandAmount = playerStats.startHandAmount;
          
          playerEquipment.SetEquipment(GameManager.Instance.playerSaveObject.GetEquipment());
          playerEquipment.GetStats(out var eArmor, out var eDamage, out var eHealth, out var eMoveValue, out var eStartCards, out var eDrawPerTurn);
          playerDefense += eArmor;
          playerDamage += eDamage;
          playerHealth += eHealth;
          playerMoveValue += eMoveValue;
          startHandAmount += eStartCards;
          drawPerTurn += eDrawPerTurn;
          
          _maxActionPoints = actionPoints;
          _maxHealth = playerHealth;
     }
     
     public void StartOfTurn()
     {
          DrawCard(drawPerTurn);
          playerArmor /= 2;
          boostPool = 0;
          CombatManager.Instance.SetBoostPoolUi(boostPool);
          CombatManager.Instance.SetPlayerArmorUi(playerArmor);
          actionPoints = _maxActionPoints;
          CombatManager.Instance.PlayerActionUi.GetAction(_maxActionPoints);
     }

     public bool CanPlayCard(int range, int cost)
     {
          if (!UiManager.Instance.CheckIfCanTakeAction()){ return false;}
          if (actionPoints < cost) return false;
          return Pathfinding.CheckEnemiesInRange(range, playerMovementController.tileStandingOn);
          
     }
     
     public bool CanPlayCard(int cost)
     {
          if (!UiManager.Instance.CheckIfCanTakeAction()){ return false;}
          if (actionPoints < cost) return false;
          return true;

     }

     private void DrawCard()
     {
          if(!UiManager.Instance.CheckIfCanTakeAction()) return;
          if(actionPoints <= 0) return;
          playerMovementController.OnMove += UseActionPoint;
          playerMovementController.HighlightMove(playerMoveValue);
          //move
          if (hand.CheckIfFull())
          {
               CardManager.Instance.discardPile.AddCardToDiscard(deck.DrawToDiscard());
               
          }
          else
          {
               hand.AddCard(deck.DrawCard());
          }
          
          
     }

     public void DrawCard(int amount)
     {
          for (int i = amount; i > 0; i--)
          {
               if (hand.CheckIfFull())
               {
                    CardManager.Instance.discardPile.AddCardToDiscard(deck.DrawToDiscard());
               
               }
               else
               {
                    hand.AddCard(deck.DrawCard());
               }
          }

     }


     public void TakeDamage(int amount)
     {
          if (playerArmor > 0)
          {
               playerArmor -= amount;
               amount = playerArmor < 0 ? math.abs(playerArmor) : 0;
               
               CombatManager.Instance.SetPlayerArmorUi(playerArmor);
          }
         
          if (playerHealth - amount >= 0)
          {
               playerHealth -= amount;
          }
          else
          {
               playerHealth = 0;
          }
          CombatManager.Instance.UpdatePlayerHealthBar(playerHealth, _maxHealth);
          CombatManager.Instance.ContinueTurn();
     }

     public void HealDamage(int amount)
     {
          if (playerHealth + amount >= _maxHealth)
          {
               playerHealth = _maxHealth;
          }
          else
          {
               playerHealth += amount;
          }
          
          CombatManager.Instance.UpdatePlayerHealthBar(playerHealth, _maxHealth);
     }
     
     public void UseActionPoint(int cost)
     {

          CombatManager.Instance.PlayerActionUi.LoseAction(cost);
          actionPoints -= cost;
          if (actionPoints <= 0)
          {
               if (fastTurn)
               {
                    if(!hand.CheckIfCanPlayCard()) OnPlayerDone();
               }
                    
          }
     }
     
     public void UseActionPoint()
     {
          playerMovementController.OnMove -= UseActionPoint;
          CombatManager.Instance.PlayerActionUi.LoseAction(1);
          if (--actionPoints <= 0)
          {
               if (fastTurn)
               {
                    if(!hand.CheckIfCanPlayCard()) OnPlayerDone();
               }
                    
          }
     }

     public void Attacked(int damage)
     {
          TakeDamage(damage); 
          CombatManager.Instance.ContinueTurn();
          // if (hand.HasCardType(CardData.CardType.Defense) || hand.HasCardType(CardData.CardType.Versatile))
          // {
          //      CombatManager.Instance.BlockPhase();
          // }
          // else
          // {
          //      TakeDamage(damage); 
          //      CombatManager.Instance.ContinueTurn();
          // }
     }
     

     public bool UseBoost(int boostCost)
     {
          return boostPool >= boostCost;
     }

     public void GainBoost(int boost)
     {
          boostPool += boost;
          if (boostPool > boostPoolMax) boostPool = boostPoolMax;
          CombatManager.Instance.SetBoostPoolUi(boostPool);
     }

     public void GainArmor(int amount)
     {
          playerArmor += amount;
          CombatManager.Instance.SetPlayerArmorUi(playerArmor);
     }
     
     
     public int GetCardValues(BaseCardObject cardObject)
     {
          if (cardObject.GetCardData().cardType == CardData.CardType.Agility)
          {
               return playerMoveValue;
          }

          if (cardObject.GetCardData().cardType == CardData.CardType.Attack)
          {
               return playerDamage;
          }

          if (cardObject.GetCardData().cardType == CardData.CardType.Defense)
          {
               return playerDefense;
          }

          if (cardObject.GetCardData().cardType == CardData.CardType.Versatile)
          {
               return playerDamage + playerDefense;
          }

          return 0;
     }

     public int CheckDamageAfterArmor(int amount)
     {
          if (playerArmor > amount) return 0;
          return Math.Abs(playerArmor - amount);
     }
}
