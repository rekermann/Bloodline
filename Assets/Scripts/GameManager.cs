using System;
using System.Collections;
using System.Collections.Generic;
using EnemyAi;
using Equipment;
using Player;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Camera mainCamera;
    public PlayerSaveObject playerSaveObject;

    public void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
        
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        mainCamera = Camera.main;
    }


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            SceneManager.LoadScene("Ayuna");
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            SceneManager.LoadScene("CombatTestScene");
        }
    }
}
