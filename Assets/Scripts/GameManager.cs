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
        mainCamera = Camera.main;
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("asd");
            SceneManager.LoadScene("Ayuna");
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("asd");
            SceneManager.LoadScene("CombatTestScene");
        }
    }
}
