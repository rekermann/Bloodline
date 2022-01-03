using System.Collections;
using System.Collections.Generic;
using CardMetaData;
using EnemyAi;
using TMPro;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.UI;

public class InformationWindowUi : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI healthText;
    public CardPopulate activeCard;
    public Transform healtBar;


    public void SetupInformationWindow(InfoWindowData data)
    {
        nameText.text = data.name;
        healthText.text = data.health + "/" + data.maxHealth;
        healtBar.localScale = new Vector3((float) data.health / data.maxHealth, 1, 1);
        activeCard.SetupCard(data.card);
    }

}
