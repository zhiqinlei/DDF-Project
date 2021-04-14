﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class FuelShadowController : MonoBehaviour
{
    private FuelController Fuel;
    private GameObject LightSourceObj;
    private GameObject WallObj;
    private GameManager gameManager;

    public void Initialize(FuelController fuel)
    {
        Fuel = fuel;
    }

    void Start()
    {
        gameManager = GameManager.Instance;
        LightSourceObj = gameManager.LightController.LightPosition.gameObject;
        WallObj = gameManager.Wall.gameObject;

        if (!LightSourceObj || !WallObj || !Fuel)
        {
            Debug.LogError("no light source or wall or fuel");
        }
    }

    void Update()
    {
        //add fuel attack and defend
    }

    void LateUpdate()
    {
        Vector3 shadowPos = ShadowController.GetShadowPosition(LightSourceObj.transform.position, 
            Fuel.gameObject.transform.position,
            WallObj.transform.position);
        transform.position = shadowPos;
        float targetHeight = ShadowController.GetShadowHeight(LightSourceObj.transform.position, 
            Fuel.gameObject.transform.position,
            WallObj.transform.position,
            Fuel.GetComponent<Renderer>().bounds.size.y);

        float currentHeight = GetComponent<Renderer>().bounds.size.y;
        Vector3 rescale = transform.localScale;
        rescale.y = targetHeight * rescale.y / currentHeight;
        rescale.x = targetHeight * rescale.x / currentHeight;
        
        transform.localScale = rescale;
    }


    void OnTriggerEnter(Collider other)
    {
        // add shadow attack/shield
        if (other.gameObject.tag == "Monster")
        {
            Debug.Log("Fuel attack");
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Bullet")
        {
            Debug.Log("Fuel defend");
            Destroy(other.gameObject);
        }
    }

    
}