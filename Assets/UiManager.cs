using System;
using System.Collections;
using System.Collections.Generic;
using EnemyAi;
using Equipment;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance;
    
    public GameObject playerPrefab;
    public Deck.Deck deck;
    public MouseController mouseController;
    public GameObject informationWindowUi;
    public EquipmentCombos equipmentCombos;
    public EquipmentManager equipmentManager;
    
    private bool _canTakeAction = true;
    private bool _playerControl = true;
    [NonSerialized] public GameObject playerRef;
    
    public void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;
        
    }

    public void DisableControls()
    {
        _playerControl = false;
    }

    public void EnableControls()
    {
        _playerControl = true;
    }
    public bool CheckControls()
    {
        return _playerControl;
    }
    public bool CheckIfCanTakeAction()
    {
        return _canTakeAction;
    }

    public void DisableAction()
    {
        _canTakeAction = false;
    }

    public void EnableAction()
    {
        _canTakeAction = true;
    }
    
    public void SetPlayerRefrence(GameObject player)
    {
        playerRef = player;
    }

    public void EnableInformationWindow()
    {
        informationWindowUi.SetActive(true);
    }


    public void EnableInformationWindow(InfoWindowData data)
    {
        
        informationWindowUi.SetActive(true);
        informationWindowUi.GetComponent<InformationWindowUi>().SetupInformationWindow(data);
    }

    public void DisableInformationWindow()
    {
        informationWindowUi.SetActive(false);
    }
}
