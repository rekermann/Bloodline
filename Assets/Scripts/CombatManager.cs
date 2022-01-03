using System;
using System.Collections;
using System.Collections.Generic;
using CombatUI;
using EnemyAi;
using TMPro;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public PlayerActionUi PlayerActionUi;
    public PlayerHealthUI PlayerHealthUI;
    public HealthBarUi playerHealthBar;
    public BlockUi BlockUi;
    public TextMeshProUGUI boostPoolUi;
    public TextMeshProUGUI armorUi;
    

    public static CombatManager Instance;
    private PlayerController playerRef;
    public Turn turn = Turn.Player;
    public List<Enemy> enemies = new List<Enemy>();
    private Enemy _activeEnemy;

    private List<Enemy> _enemyQueue = new List<Enemy>();
    public void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;
    }
    public enum Turn
    {
        Player,
        Enemy
    }
    public void PassTurn()
    {
        ToggleTurn();
        switch (turn)
        {
            case Turn.Enemy:
                _activeEnemy = null;
                SetupEnemyQueue();
                if(_enemyQueue.Count > 0) EnemyTurn();
                break;
            case Turn.Player:
                foreach (var enemy in enemies)
                {
                    enemy.SetActiveCard();
                }
                PlayerTurn();
                break;
        }
    }

    public void ContinueTurn()
    {
        if (turn == Turn.Enemy)
        {
            EnemyTurn();
        }
    }

    private void SetupEnemyQueue()
    {
        _enemyQueue.Clear();
        foreach (var enemy in enemies)
        {
            _enemyQueue.Add(enemy);
        }
    }

    private void ToggleTurn()
    {
        if (turn == Turn.Enemy) { turn = Turn.Player; }
        else { turn = Turn.Enemy; }
        
    }

    public void PlayerTurn()
    {
        playerRef.StartOfTurn();
    }
    
    public void EnemyTurn()
    {
        if (_enemyQueue.Count < 1)
        {
            PassTurn();
        }
        else
        {
            _activeEnemy = _enemyQueue[0];
            _enemyQueue.RemoveAt(0);
            _activeEnemy.TakeTurn();
        }
        
    }
    public void AddEnemy(Enemy e)
    {
        enemies.Add(e);
    }
    
    public void RemoveEnemy(Enemy e)
    {
        enemies.Remove(e);
    }
    
    public void SetPlayerReference(GameObject p)
    {
        playerRef = p.GetComponent<PlayerController>();
        playerRef.OnPlayerDone += PassTurn;
    }

    public void BlockDone(int damage)
    {
        playerRef.TakeDamage(damage);
    }

    public void BlockPhase()
    {
        BlockUi.SetActive(_activeEnemy.GetActiveCard());
    }

    public void SetBoostPoolUi(int amount)
    {
        boostPoolUi.text = amount.ToString();
    }
    public void SetPlayerArmorUi(int amount)
    {
        if (amount < 0) amount = 0;
        armorUi.text = amount.ToString();

    }

    public PlayerController GetPlayer()
    {
        return playerRef;
    }
    public void UpdatePlayerHealthBar(int health, int maxHealth)
    {
        playerHealthBar.UpdateHealthBar(health, maxHealth);
    }
}
