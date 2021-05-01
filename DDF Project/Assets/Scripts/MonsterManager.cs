using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using NaughtyAttributes;

public class MonsterManager : MonoBehaviour
{
    [Required] public GameObject MonsterGroup;
    [Required] public GameObject PositionTop;
    [Required] public GameObject PositionButton;
    [Required] public GameObject ShadowMonsterPrefab;
    public float ShowInterval = 0.7f;
    public float MinShowInterval;
    private float tempShowInterval;

    public float MonsterSpeed = 4.0f;
    public float MaxMonsterSpeed;

    private float Size;
    public float maxsize = 2.5f;
    public float minsize = 1f;

    public float acceleration;

    private float LevelTime = 20f;
    public float LevelUpTime;

    [SerializeField] private List<MonsterController> MonsterList;
    private GameManager gameManager;

    private float x = 0;  
    private int dir = 0;
    private int UpDown = 0; // change the up down of monster 

    void Start()
    {
        gameManager = GameManager.Instance;
        tempShowInterval = ShowInterval;
        MonsterList = new List<MonsterController>();
    }

    void Update()
    {
        if (gameManager.GameMode == GameManager.Mode.Normal)
        {
            NormalGameModeLoop();
        }
    }

    private void NormalGameModeLoop()
    {
        tempShowInterval -= Time.deltaTime;
        if (tempShowInterval <= 0)
        {
            // random size
            Size = Random.Range(minsize, minsize + 1f);
            Show(Size);
            tempShowInterval = Random.Range(ShowInterval/2, ShowInterval); //ghost will randomly showup
        }

        // upgrade monster speed and size and reduce interval when level up
        if (Score.GetScore() == LevelTime){
            if (Mathf.Abs(ShowInterval) >= MinShowInterval){
                ShowInterval -= acceleration*ShowInterval;
            }
            // bullet speed accelerate until max speed
            if(Mathf.Abs(MonsterSpeed)<=MaxMonsterSpeed){
                MonsterSpeed += acceleration*MonsterSpeed;
            }

            if(Mathf.Abs(minsize)<=maxsize){
                minsize += acceleration*minsize;
            }

            //sent analyticsResult
            AnalyticsResult analyticsResult = Analytics.CustomEvent(
                "Monster: Upgrade",
                new Dictionary<string, object> {
                    {"Time", Score.GetScore() }
                }
            );
            Debug.Log("analyticsResult: " + analyticsResult);
            Debug.Log("Monster upgrade");

            LevelTime += LevelUpTime;

        }
    }

    public GameObject Show(float size)
    {
        x = Random.Range(PositionTop.transform.position.x, PositionButton.transform.position.x);

        if (x < (PositionButton.transform.position.x + PositionTop.transform.position.x)/2 ) // born right
        {
            x = PositionTop.transform.position.x;
            dir = 0;
            UpDown = 180;
        }
        else // born left
        {
            x = PositionButton.transform.position.x;
            dir = 180;
            UpDown = 0;
        }
        Vector3 monsterStartPos = new Vector3(
            x, //randomly choose a start position
            PositionButton.transform.position.y - (float) 1.25 + size,
            PositionButton.transform.position.z);
        
        return Show(size, monsterStartPos, dir);
    }

    public GameObject Show(float size, Vector3 startPos, float dir)
    {
        //GameObject monsterObj = Instantiate(ShadowMonsterPrefab, startPos, Quaternion.Euler(dir, 90, 0) , MonsterGroup.transform); // randomly choose direction
        GameObject monsterObj = Instantiate(ShadowMonsterPrefab, startPos, Quaternion.Euler(dir, 90, UpDown) , MonsterGroup.transform);
        MonsterController monster = monsterObj.GetComponent<MonsterController>();
        monsterObj.transform.localScale = new Vector3 (0, size, size);
        monster.Initialize(MonsterSpeed);
        MonsterList.Add(monster);
        return monsterObj;
    }

    public float getSize(){
        return Size;
    }
}
