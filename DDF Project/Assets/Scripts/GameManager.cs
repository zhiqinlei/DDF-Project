using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    public bool DebugMode = true;
    public PlayerController PlayerController;
    public ShadowController ShadowController;
    public MonsterController MonsterController;
    public BulletManager BulletManager;
    public LightController LightController;
    //public HealthBarUIManager HealthBarUIManager;
    public MonsterManager MonsterManager;
    public FuelManager FuelManager;
    public BlockManager BlockManager;
    public HealthBar HealthBar;
    public Score Score; 

    public AudioSource Music;
    public AudioClip MusicEnd;
    
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
        MonsterController = FindObjectOfType<MonsterController>();
        BulletManager = FindObjectOfType<BulletManager>();
        LightController = FindObjectOfType<LightController>();
        MonsterManager = FindObjectOfType<MonsterManager>();
        FuelManager = FindObjectOfType<FuelManager>();
        BlockManager = FindObjectOfType<BlockManager>();

        // ui manager
        //HealthBarUIManager = FindObjectOfType<HealthBarUIManager>();
        HealthBar = FindObjectOfType<HealthBar>();
        // socre manager
        Score = FindObjectOfType<Score>();
    }

    bool gameHasEnded = false;

    public float restartDelay = 1f;

    public void EndGame ()
    {
        if (gameHasEnded == false)
        {   
            gameHasEnded = true;
            Debug.Log("Game Over");
            Invoke("Restart", restartDelay);
            Music.clip = MusicEnd;
            Music.Play();
        }
    }

    void Restart ()
    {
        SceneManager.LoadScene("EndMenu");
    }
}
