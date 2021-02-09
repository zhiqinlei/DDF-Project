﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    public bool DebugMode = true;
    public PlayerController PlayerController;
    public ShadowController ShadowController;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        // Dont destroy on reloading the scene
        DontDestroyOnLoad(gameObject);

        PlayerController = FindObjectOfType<PlayerController>();
        ShadowController = FindObjectOfType<ShadowController>();
    }
}
