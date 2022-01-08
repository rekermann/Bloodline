using System;
using System.Collections;
using System.Collections.Generic;
using EnemyAi;
using Equipment;
using Player;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Camera mainCamera;
    [SerializeField] private PlayerSaveObject playerSaveObject;
    private PlayerSaveObj _playerSaveObj;

    public void Awake()
    {
        if (GameManager.Instance == null)
        {

            Instance = this;
            SceneManager.sceneLoaded += OnSceneLoaded;
            DontDestroyOnLoad(gameObject);
            _playerSaveObj = playerSaveObject.GetPlayerStats();
        }
        else
        {
            Destroy(gameObject);
        }


    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        mainCamera = Camera.main;
    }

    public void SavePlayer()
    {
        
    }

    public void SavePlayerEquipment(List<EquipmentData> equipmentData)
    {
        _playerSaveObj.SetEquipment(equipmentData);

    }
    
    public void SaveToObject()
    {
        EquipmentManager.instance.SaveEquipment();
        playerSaveObject.SavePlayerStats(_playerSaveObj);
        EditorUtility.SetDirty(playerSaveObject);
        AssetDatabase.SaveAssets();
        
    }

    
    public PlayerSaveObj GetSave()
    {
        return _playerSaveObj;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            EquipmentManager.instance.SaveEquipment();
            SceneManager.LoadScene("Ayuna");
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            EquipmentManager.instance.SaveEquipment();
            SceneManager.LoadScene("CombatTestScene");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            EquipmentManager.instance.SaveEquipment();
            SceneManager.LoadScene("StartScreen");
        }
    }
}
