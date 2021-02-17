using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    public bool DebugMode = true;
    public PlayerController PlayerController;
    public ShadowController ShadowController;
    public BulletManager BulletManager;
    public LightController LightController;
    public HealthBarUIManager HealthBarUIManager;
    public MonsterManager MonsterManager;
    
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
        // DontDestroyOnLoad(gameObject);

        PlayerController = FindObjectOfType<PlayerController>();
        ShadowController = FindObjectOfType<ShadowController>();
        BulletManager = FindObjectOfType<BulletManager>();
        LightController = FindObjectOfType<LightController>();
        MonsterManager = FindObjectOfType<MonsterManager>();

        // ui manager
        HealthBarUIManager = FindObjectOfType<HealthBarUIManager>();
    }
}
