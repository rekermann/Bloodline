using CardMetaData;
using Tiles;
using UnityEngine;

namespace EnemyAi
{
    public class Enemy : MonoBehaviour
    {
        private string enemyName;
        private int baseMoveValue;
        private int baseRange;
        private int health;
        private int _maxHealth;
        
        public MapTile tileStandingOn;

        [SerializeField] private SpriteRenderer spriteRenderer;

        public EnemyStatsObject enemyStatsObject;

        private BaseCardObject _activeCard;

        public void Start()
        {
            SetupEnemy();
            
            SetActiveCard();
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
        }
        
        public void TakeTurn()
        {
            EnemyMovement();
            PlayerController player = EnemyMovementController.CheckIfPlayerIsInRange(tileStandingOn, baseRange);
            if (player != null)
                EnemyAttack(player);
            else
            {
                CombatManager.Instance.ContinueTurn();
            }
        }


        private void EnemyMovement()
        {
            MapTile destinationTile = EnemyMovementController.MoveTowardsPlayer(tileStandingOn, baseMoveValue);
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
            player.Attacked(_activeCard.GetCardData().cardCombatValue);

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

        public bool TakeDamage(BaseCardObject baseCardObject)
        {
            health -= baseCardObject.GetCardData().cardCombatValue;
            if (health < 0) health = 0;
            GetComponent<InfoWindow>().SetInfoWindowData(enemyName, health, _maxHealth, _activeCard);
            if (health <= 0)
            {
                Die();
                return true;
            }

            return false;
        }

        public bool TakeDamage(int dmg)
        {
            health -= dmg;
            if (health < 0) health = 0;
            GetComponent<InfoWindow>().SetInfoWindowData(enemyName, health, _maxHealth, _activeCard);
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
