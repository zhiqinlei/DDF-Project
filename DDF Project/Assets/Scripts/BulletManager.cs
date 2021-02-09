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
    public float ShootInterval = 1.0f;
    private float tempShootInterval;
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
        Vector3 bulletStartPos = new Vector3(PositionBotton.transform.position.x,
            Random.Range(PositionBotton.transform.position.y, PositionTop.transform.position.y),
            PositionBotton.transform.position.z);
        GameObject bulletObj = Instantiate(BulletPrefab, bulletStartPos, Quaternion.Euler(0, 0, 0) , BulletGroup.transform);
        BulletController bullet = bulletObj.GetComponent<BulletController>();
        bulletList.Add(bullet);
    }
}
