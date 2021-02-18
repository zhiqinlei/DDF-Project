using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class BulletManager : MonoBehaviour
{
    [Required] public GameObject BulletGroup;
    [Required] public GameObject PositionTop;
    [Required] public GameObject PositionBotton;
    [Required] public GameObject BulletPrefab;
    public float ShootInterval = 0.7f;
    private float tempShootInterval;
    public float BulletSpeed = 4.0f;
    [SerializeField] private List<BulletController> bulletList;
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;
        tempShootInterval = ShootInterval;
        bulletList = new List<BulletController>();
    }

    void Update()
    {
        tempShootInterval -= Time.deltaTime;
        if (tempShootInterval <= 0)
        {
            Shoot();
            tempShootInterval = ShootInterval;
        }
    }

    private void Shoot()
    {
        Vector3 bulletStartPos = new Vector3(
            Random.Range(PositionBotton.transform.position.x, PositionTop.transform.position.x),
            PositionBotton.transform.position.y,
            PositionBotton.transform.position.z);
        
        // rotate on y axis -90 faceing left
        GameObject bulletObj = Instantiate(BulletPrefab, bulletStartPos, Quaternion.Euler(90, -90, 0) , BulletGroup.transform);
        BulletController bullet = bulletObj.GetComponent<BulletController>();
        bullet.Initialize(BulletSpeed);
        bulletList.Add(bullet);
    }
}
