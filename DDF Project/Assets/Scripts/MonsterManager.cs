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

    public float Size;
    public float maxsize = 2.5f;
    public float minsize = 1f;

    public float acceleration;
    public float LevelUpTime;

    [SerializeField] private List<MonsterController> MonsterList;
    private GameManager gameManager;

    private float x = 0;  
    private int dir = 0; 

    void Start()
    {
        gameManager = GameManager.Instance;
        tempShowInterval = ShowInterval;
        MonsterList = new List<MonsterController>();
    }

    void Update()
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
        if (Score.GetScore() == LevelUpTime){
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

            LevelUpTime += LevelUpTime;

        }
    }

    private void Show(float Size)
    {
        x = Random.Range(PositionTop.transform.position.x, PositionButton.transform.position.x);

        if (x < (PositionButton.transform.position.x + PositionTop.transform.position.x)/2 ) // born left
        {
            x = PositionTop.transform.position.x;
            dir = 0;
        }
        else // born right
        {
            x = PositionButton.transform.position.x;
            dir = 180;
        }
        Vector3 monsterStartPos = new Vector3(
            x, //randomly choose a start position
            PositionButton.transform.position.y - (float) 1.25 + Size,
            PositionButton.transform.position.z);
        
        
        GameObject monsterObj = Instantiate(ShadowMonsterPrefab, monsterStartPos, Quaternion.Euler(dir, 90, 0) , MonsterGroup.transform); // randomly choose direction
        MonsterController monster = monsterObj.GetComponent<MonsterController>();

        //randomly change size
        Vector3 randomSize = new Vector3 (0, Size, Size);
        monsterObj.transform.localScale = randomSize;
      

        monster.Initialize(MonsterSpeed);
        MonsterList.Add(monster);
    }

    public float getSize(){
        return Size;
    }
}
