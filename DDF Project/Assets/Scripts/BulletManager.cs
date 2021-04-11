using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using NaughtyAttributes;

public class BulletManager : MonoBehaviour
{
    [Required] public GameObject BulletGroup;
    [Required] public GameObject PositionTop;
    [Required] public GameObject PositionBotton;
    [Required] public GameObject BulletPrefab;
    public float ShootInterval = 0.7f;
    public float MinShootInterval = 0.3f;
    private float tempShootInterval;

    public float BulletSpeed = 4.0f;
    public float MaxBulletSpeed = 7.0f;

    public float acceleration;
    public float LevelUpTime;

    public float BulletExistingTime = 6.0f; // bullte will be destroied after certain seconds
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;
        tempShootInterval = ShootInterval;
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
        tempShootInterval -= Time.deltaTime;
        if (tempShootInterval <= 0)
        {
            Shoot();
            tempShootInterval = ShootInterval;
        }
        // reduce bullet shoot interval is Level UP until reach the min interval
        if (Score.GetScore() == LevelUpTime){
            if (Mathf.Abs(ShootInterval) >= MinShootInterval){
                ShootInterval -= acceleration*ShootInterval;
            }
            // bullet speed accelerate until max speed
            if(Mathf.Abs(BulletSpeed)<=MaxBulletSpeed){
                BulletSpeed += acceleration*BulletSpeed;
            }
            //sent analyticsResult
            AnalyticsResult analyticsResult = Analytics.CustomEvent(
                "Bullet: Accelerate",
                new Dictionary<string, object> {
                    {"Time", Score.GetScore() }
                }
            );
            Debug.Log("analyticsResult: " + analyticsResult);
            Debug.Log("Bullet accelerate");

            LevelUpTime += LevelUpTime;

        }
    }

    public GameObject Shoot()
    {
        Vector3 bulletStartPos = new Vector3(
            Random.Range(PositionBotton.transform.position.x, PositionTop.transform.position.x),
            PositionBotton.transform.position.y,
            PositionBotton.transform.position.z);
        
        // rotate on y axis -90 faceing left
        GameObject bulletObj = Instantiate(BulletPrefab, bulletStartPos, Quaternion.Euler(90, -90, 0) , BulletGroup.transform);
        BulletController bullet = bulletObj.GetComponent<BulletController>();
        bullet.Initialize(BulletSpeed, BulletExistingTime);
        return bulletObj;
    }
}
