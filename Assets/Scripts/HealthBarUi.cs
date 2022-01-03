using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUi : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public Transform healthBar;


    public void UpdateHealthBar(int health, int maxHealth) 
    {
        healthText.text = health + "/" + maxHealth;
        healthBar.localScale = new Vector3((float) health / maxHealth, 1, 1);
    }
}
