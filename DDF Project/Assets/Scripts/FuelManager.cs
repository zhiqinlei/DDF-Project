using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class FuelManager : MonoBehaviour
{
    [Required] public GameObject XRangeMax;
    [Required] public GameObject XRangeMin;
    [Required] public GameObject ZRangeMax;
    [Required] public GameObject ZRangeMin;
    [Required] public GameObject FuelGroup; // fuel object generate position
    [Required] public GameObject FuelPrefab;
    [Required] public GameObject FuelShadowGroup;
    [Required] public GameObject FuelShadowPrefab;
    public float FuelExistingTime;
    public float FuelGenerateInterval; // every certain seconds, generate a fuel
    public float LevelUpTime; // reduce the fuelgenerateInterval while level up
    private float LevelTime = 45f;
    private float tempFuelGenerateInterval;
    public bool AutoGenerateFuel = true; // turn off if want to use other things to trigger generating action
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;
        tempFuelGenerateInterval = FuelGenerateInterval;
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
        if (AutoGenerateFuel)
        {
            tempFuelGenerateInterval -= Time.deltaTime;
            if (tempFuelGenerateInterval <= 0.0f)
            {
                GenerateFuel();
                
                if (Score.GetScore() >= LevelTime && FuelGenerateInterval >= 0f)
                {
                    FuelGenerateInterval -= 1f;
                }
                tempFuelGenerateInterval = FuelGenerateInterval;
                LevelTime += LevelUpTime;
            }
        }
    }

    public GameObject GenerateFuel()
    {
        Vector3 startPos = new Vector3(
            Random.Range(XRangeMin.transform.position.x, XRangeMax.transform.position.x),
            FuelGroup.transform.position.y,
            Random.Range(ZRangeMin.transform.position.z, ZRangeMax.transform.position.z)
        );
        return GenerateFuel(startPos);
    }

    public GameObject GenerateFuel(Vector3 startPos, bool alwaysExist=false)
    {
        GameObject fuelObj = Instantiate(FuelPrefab, startPos, Quaternion.identity, FuelGroup.transform);
        FuelController fuel = fuelObj.GetComponent<FuelController>();
        GameObject fuelShadowObj = Instantiate(FuelShadowPrefab, FuelShadowGroup.transform);
        FuelShadowController shadow = fuelShadowObj.GetComponent<FuelShadowController>();
        fuel.Initialize(FuelExistingTime, shadow, alwaysExist:alwaysExist);
        shadow.Initialize(fuel);
        return fuelObj;
    }
}
