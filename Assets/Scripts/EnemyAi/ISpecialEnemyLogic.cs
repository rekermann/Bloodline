using System.Collections;
using System.Collections.Generic;
using EnemyAi;
using UnityEngine;

public interface ISpecialEnemyLogic
{
    /*
    case Enemy.EnemyCurrentState.StartOfTurn:
        StartOfTurn()
        break;
    case Enemy.EnemyCurrentState.BeforeDamage:
        BeforeDamage()
        break;
    case Enemy.EnemyCurrentState.AfterDamage:
        AfterDamage()
        break;
    case Enemy.EnemyCurrentState.EndOfTurn:
        EndOfTurn()
        break;
    case Enemy.EnemyCurrentState.BeforeDamageTaken:
        BeforeDamageTaken()
        break;
    case Enemy.EnemyCurrentState.AfterDamagedTaken:
        AfterDamagedTaken()
        break;
    */

    public void SpecialRules();

    public void StartOfTurn();
    public void BeforeDamage();
    public void AfterDamage();
    public void EndOfTurn();
    public void BeforeDamageTaken();
    public void AfterDamagedTaken();
    
}
