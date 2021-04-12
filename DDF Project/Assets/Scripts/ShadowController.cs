﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class ShadowController : MonoBehaviour
{
    [Required] public PlayerController Player;
    [Required] public GameObject LightSourceObj;
    [Required] public GameObject WallObj;
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;

        if (!LightSourceObj || !WallObj || !Player)
        {
            Debug.LogError("no light source or wall or player");
        }
    }

    void LateUpdate()
    {
        Vector3 shadowPos = GetShadowPosition(LightSourceObj.transform.position, 
            Player.gameObject.transform.position,
            WallObj.transform.position);
        transform.position = shadowPos;
        float targetHeight = GetShadowHeight(LightSourceObj.transform.position, 
            Player.gameObject.transform.position,
            WallObj.transform.position,
            Player.Height);

        float currentHeight = GetComponent<Renderer>().bounds.size.y;
        Vector3 rescale = transform.localScale;
        rescale.y = targetHeight * rescale.y / currentHeight;
        rescale.x = targetHeight * rescale.x / currentHeight;
        transform.localScale = rescale;
    }

    public static Vector3 GetShadowPosition(Vector3 light, Vector3 player, Vector3 screen)
    {
        float lightToPlayer = Mathf.Abs(player.z - light.z);
        float lightToPlayerOnX = light.x - player.x;
        float lightToPlayerOnY = light.y - player.y;
        float lightToScreen = Mathf.Abs(screen.z - light.z);
        
        // if (gameManager.DebugMode)
        // {
        //     Debug.Log("lightToPlayer " + lightToPlayer);
        //     Debug.Log("x "+lightToPlayerOnX);
        //     Debug.Log("y "+lightToPlayerOnY);
        //     Debug.Log("lightToScreen "+lightToScreen);
        // }
        Vector3 result = Vector3.zero;
        float dX =  lightToScreen / lightToPlayer * lightToPlayerOnX;
        result.x = light.x - dX;

        float dY = lightToScreen / lightToPlayer * lightToPlayerOnY;
        result.y = light.y - dY;

        result.z = screen.z + 0.2f;

        return result;
    }

    public static float GetShadowHeight(Vector3 light, Vector3 player, Vector3 screen, float playerHeight)
    {
        float lightToPlayer = Mathf.Abs(player.z - light.z);
        float lightToScreen = Mathf.Abs(screen.z - light.z);
        return lightToScreen / lightToPlayer * playerHeight;
    }

    void OnTriggerEnter(Collider other)
    {   
        Vector3 shadowPos = GetShadowPosition(LightSourceObj.transform.position, 
            Player.gameObject.transform.position,
            WallObj.transform.position);

        if (other.gameObject.tag == "Monster")
        {
            Debug.Log("hit monster");
            // if shadow smaller than ghost size, geteat
            if (  transform.localScale.y <= other.gameObject.transform.localScale.y ){
                
                Player.GetEat();
            }
            else{
            // else eat the ghost
               Player.Eat();
            }
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Bullet")
        {
            Debug.Log("hit bullet");
            Player.ReduceHealth();
            Destroy(other.gameObject);
        }
    }
}
