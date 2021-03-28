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
    public float FuelExistingTime = 4.0f;
    public float FuelGenerateInterval = 5.0f; // every certain seconds, generate a fuel
    private float tempFuelGenerateInterval;
    public bool AutoGenerateFuel = true; // turn off if want to use other things to trigger generating action
    

    void Start()
    {
        // add a fuel at the beginning 
        GenerateFuel();

        tempFuelGenerateInterval = FuelGenerateInterval;
    }

    void Update()
    {
        if (AutoGenerateFuel)
        {
            tempFuelGenerateInterval -= Time.deltaTime;
            if (tempFuelGenerateInterval <= 0.0f)
            {
                GenerateFuel();
                
                tempFuelGenerateInterval = FuelGenerateInterval;
            }
        }
       
    }

    public void GenerateFuel()
    {
        Vector3 startPos = new Vector3(
            Random.Range(XRangeMin.transform.position.x, XRangeMax.transform.position.x),
            FuelGroup.transform.position.y,
            Random.Range(ZRangeMin.transform.position.z, ZRangeMax.transform.position.z)
        );
        GameObject fuelObj = Instantiate(FuelPrefab, startPos, Quaternion.identity, FuelGroup.transform);
        FuelController fuel = fuelObj.GetComponent<FuelController>();
        fuel.Initialize(FuelExistingTime);
    }
}
