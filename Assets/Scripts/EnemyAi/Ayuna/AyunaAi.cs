using System;
using System.Diagnostics;
using TMPro;
using UiUtilities;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace EnemyAi.Ayuna
{
    public class AyunaAi : MonoBehaviour, ISpecialEnemyLogic
    {
        public Enemy enemy;
        public BarUiScaler bloodGauge;
        private readonly int hungerMax = 100;
        private int hungerAddPerTurn = 0;
        private int hunger;
        public TextMeshProUGUI damageValue;
        public TextMeshProUGUI movementValue;
        public TextMeshProUGUI hungerPerTurn;



        public void SpecialRules()
        {
            SetCurrentUiValues();
        }

        public void StartOfTurn()
        {
            BloodLevelModifer();
        }

        public void BeforeDamage()
        {
        }

        public void AfterDamage()
        {
            hunger -= enemy.damagedDealtThisTurn;
            if (hunger < 0) hunger = 0;
            bloodGauge.UpdateBarYScale(hunger,hungerMax);
            SetCurrentUiValues();
        }

        public void EndOfTurn()
        {
            hunger += hungerAddPerTurn;
            if (hunger > hungerMax) hunger = hungerMax;
            if (enemy.damagedDealtThisTurn <= 0) hungerAddPerTurn += 10;
            else hungerAddPerTurn += 5;
            bloodGauge.UpdateBarYScale(hunger,hungerMax);
            SetCurrentUiValues();
        }

        public void BeforeDamageTaken()
        {
        }

        public void AfterDamagedTaken()
        {
            hunger -= enemy.damagedTakenThisTurn;
            if (hunger < 0) hunger = 0;
            bloodGauge.UpdateBarYScale(hunger,hungerMax);
            SetCurrentUiValues();
        }


        public void BloodLevelModifer()
        {
            if (hunger >= 10)
            {
                enemy.tmpDamage += 5;
            }
            if (hunger >= 20)
            {
                enemy.tmpDamage += 5;
            }
            if (hunger >= 40)
            {
                enemy.tmpDamage += 5;
                enemy.tmpMoveValue += 1;
            }
            if (hunger >= 60)
            {
                enemy.tmpMoveValue += 1;
                enemy.tmpDamage += 5;
            }
            if (hunger == 100)
            {
                enemy.tmpMoveValue += 2;
                enemy.tmpDamage += 10;
            }
        }


        public void SetCurrentUiValues()
        {
            int uiDamage = enemy.GetActiveCard().GetCardData().cardCombatValue;
            int uiMovement = enemy.BaseMoveValue;
            if (hunger >= 10)
            {
                uiDamage += 5;
            }
            if (hunger >= 20)
            {
                uiDamage += 5;
            }
            if (hunger >= 40)
            {
                uiDamage += 5;
                uiMovement += 1;
            }
            if (hunger >= 60)
            {
                uiMovement += 1;
                uiDamage += 5;
            }
            if (hunger == 100)
            {
                uiMovement += 2;
                uiDamage += 10;
            }
            
            damageValue.text = uiDamage.ToString();
            movementValue.text = uiMovement.ToString();
            hungerPerTurn.text = hungerAddPerTurn.ToString("+0;-#");
        }
    }
}