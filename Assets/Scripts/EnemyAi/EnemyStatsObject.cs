using System.Collections.Generic;
using CardMetaData;
using UnityEngine;

namespace EnemyAi
{
    [CreateAssetMenu(menuName = "Enemy/Nemesis", fileName = "New Nemesis")]
    public class EnemyStatsObject : ScriptableObject
    {
        public string enemyName;
        public int baseMoveValue;
        public int baseRange;
        public int health;
        public Sprite characterSprite;
        public List<BaseCardObject> enemyAiCards;
        
        public EnemyStats GetEnemyStats()
        {
            return new EnemyStats(enemyName, baseMoveValue, baseRange, health, characterSprite, enemyAiCards);
        }
    }

    public class EnemyStats
    {
        public string enemyName;
        public int baseMoveValue;
        public int baseRange;
        public int health;
        public Sprite characterSprite;
        public List<BaseCardObject> enemyAiCards;

        public EnemyStats(string enemyName, int baseMoveValue, int baseRange, int health, Sprite characterSprite, List<BaseCardObject> enemyAiCards)
        {
            this.enemyName = enemyName;
            this.baseMoveValue = baseMoveValue;
            this.baseRange = baseRange;
            this.health = health;
            this.characterSprite = characterSprite;
            this.enemyAiCards = enemyAiCards;
        }
    }
}
