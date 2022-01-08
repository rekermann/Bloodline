using System;
using CardMetaData;
using Tiles;
using UiUtilities;
using UnityEngine;

namespace EnemyAi
{
    public class Enemy : MonoBehaviour
    {
        private string enemyName;
        private int baseMoveValue;

        public int BaseMoveValue => baseMoveValue;

        private int baseRange;
        private int health;
        private int _maxHealth;
        public int tmpDamage;
        public int tmpMoveValue;
        private ISpecialEnemyLogic specialRules;
        [NonSerialized] public int damagedDealtThisTurn;
        [NonSerialized] public int damagedTakenThisTurn;
        public BarUiScaler healthBarUi;

        public MapTile tileStandingOn;

        [SerializeField] private SpriteRenderer spriteRenderer;

        public EnemyStatsObject enemyStatsObject;

        private BaseCardObject _activeCard;
        public bool immobilized;
        
        

        public void Start()
        {
            SetupEnemy();
            SetActiveCard();
            specialRules?.SpecialRules();
        }

        public void SetupEnemy()
        {
            EnemyStats stats = enemyStatsObject.GetEnemyStats();
            enemyName = stats.enemyName;
            baseMoveValue = stats.baseMoveValue;
            baseRange = stats.baseRange;
            health = stats.health;
            spriteRenderer.sprite = stats.characterSprite;
            GetComponent<EnemyDeck>().SetEnemyCards(stats.enemyAiCards);
            _maxHealth = health;
            CombatManager.Instance.AddEnemy(this);
            specialRules = GetComponent<ISpecialEnemyLogic>();
            
        }
        
        public void TakeTurn()
        {
            specialRules?.StartOfTurn();
            
            PlayerController player = EnemyMovementController.CheckIfPlayerIsInRange(tileStandingOn, baseRange);
            if(!CombatManager.Instance.GetPlayer().invisible && !immobilized) EnemyMovement();
            if (player != null && !player.invisible)
            {
                specialRules?.BeforeDamage();
                EnemyAttack(player);
                specialRules?.AfterDamage();
            }
            else
            {
                CombatManager.Instance.ContinueTurn();
            }
            tmpDamage = 0;
            tmpMoveValue = 0;
            specialRules?.EndOfTurn();
            damagedTakenThisTurn = 0;
            damagedDealtThisTurn = 0;
            immobilized = false;
        }


        private void EnemyMovement()
        {
            MapTile destinationTile = EnemyMovementController.MoveTowardsPlayer(tileStandingOn, baseMoveValue + tmpMoveValue);
           
            if (destinationTile != tileStandingOn) tileStandingOn.unitOnTile = null;
            if (destinationTile != null)
            {
                var obj = gameObject;
                obj.transform.position = destinationTile.transform.position;
                tileStandingOn = destinationTile;
                destinationTile.unitOnTile = obj;
            
            }

        }

        private void EnemyAttack(PlayerController player)
        {
            //puke
            damagedDealtThisTurn = player.CheckDamageAfterArmor(_activeCard.GetCardData().cardCombatValue + tmpDamage);
            player.Attacked(_activeCard.GetCardData().cardCombatValue + tmpDamage);
            

        }

        public BaseCardObject GetActiveCard()
        {
            return _activeCard;
        }

        public void SetActiveCard()
        {
            _activeCard = GetComponent<EnemyDeck>().GetRandomCard();
            GetComponent<InfoWindow>().SetInfoWindowData(enemyName, health, _maxHealth, _activeCard);
        }
        
        public bool TakeDamage(int dmg)
        {
            damagedTakenThisTurn = dmg;
            health -= dmg;
            specialRules?.BeforeDamageTaken();
            if (health < 0) health = 0;
            if(healthBarUi != null) healthBarUi.UpdateBarXScale(health, _maxHealth);
            GetComponent<InfoWindow>().SetInfoWindowData(enemyName, health, _maxHealth, _activeCard);
            specialRules?.AfterDamagedTaken();
            if (health <= 0)
            {
                Die();
                return true;
            }
            return false;
        }

        private void Die()
        {
            CombatManager.Instance.RemoveEnemy(this);
            Destroy(gameObject);
        }
        
        
    }
}
