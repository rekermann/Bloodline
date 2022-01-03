using System;
using UnityEngine;

namespace CardMetaData
{
    public abstract class BaseCardObject : ScriptableObject, ICardData
    {
        public enum AttackTypeEnum
        {
            Single,
            Aoe,
            SlashAoe,
            None
        }

        public enum OriginEnum
        {
            EnemyTarget,
            FreeTarget,
            Self
        }
        
        public Sprite cardArt;
        public String cardName;
        public String cardEffectText;
        public int cardBoostValue;
        public AttackTypeEnum attackType;
        public OriginEnum origin;
        public int cardCost = 1;
        public int range;
        public int aoeRange;
        public string beforeMainEffect;
        public string[] boostEffect;
        public string onKillEffect;
        public string afterMainEffect;
    
    
        public virtual CardData GetCardData()
        {
            return null;
        }

        public virtual void SetCombatValue(int mainValue)
        {
            
        }
    }
}
