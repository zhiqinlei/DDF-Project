﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using NaughtyAttributes;

public class LightController : MonoBehaviour
{
    [Required] public GameObject LightPosition;
    [Required] public GameObject LightPositionLeft;
    [Required] public GameObject LightPositionRight;
    public float speed;
    [Required] public Light lightPoint;
    public float MaxIntensity; // fixed the max intensity
    public float LightReduceIndex; // describes how fast the light goes off
    public float FuelAddIndex; // describes how many intensity is add when eat a fuel

    public float acceleration;
    public float MaxSpeed; // adjust speed
    public float LevelUpTime;

    void Update()
    {
        LightOff();
    }

    void FixedUpdate() // light source move
    {
        if (Score.GetScore() >= 10){
            //sent analyticsResult
            AnalyticsResult analyticsResult = Analytics.CustomEvent(
                "Light Source: Start Move",
                new Dictionary<string, object> {
                    {"Time", Score.GetScore() }
                }
            );
            Debug.Log("analyticsResult: " + analyticsResult);
            Debug.Log("light source start move ");
            // start move
            UpdatePosition();
        }
    }

    private void UpdatePosition()
    {
        LightPosition.transform.position += Vector3.left * speed * Time.deltaTime;
        if (LightPosition.transform.position.x >= LightPositionRight.transform.position.x || 
            LightPosition.transform.position.x <= LightPositionLeft.transform.position.x )
        {
            speed = -speed;
        }

        if (Score.GetScore() == LevelUpTime){
            if(Mathf.Abs(speed)<=MaxSpeed){
                speed+=acceleration*speed;
            }
            //sent analyticsResult
            AnalyticsResult analyticsResult = Analytics.CustomEvent(
                "Light Source: Accelerate",
                new Dictionary<string, object> {
                    {"Time", Score.GetScore() }
                }
            );
            Debug.Log("analyticsResult: " + analyticsResult);
            Debug.Log("Light Source accelerate");

            LevelUpTime += LevelUpTime;
        }
    }

    private void LightOff()
    {
        lightPoint.intensity -= LightReduceIndex*Time.deltaTime;
        //reduce range when time goes on
        lightPoint.range -= LightReduceIndex*Time.deltaTime*0.3f;

        if (lightPoint.intensity<=0.0){
            //sent analyticsResult
            AnalyticsResult analyticsResult = Analytics.CustomEvent(
                "Game Over: Light off",
                new Dictionary<string, object> {
                    {"Score", Score.GetScore() }
                }
            );
            Debug.Log("analyticsResult: " + analyticsResult);
            Debug.Log("die by darkness " + Score.GetScore() +"s");
            //game over
            FindObjectOfType<GameManager>().EndGame();
        }
    }

    public void AddFuel()
    {
        if (lightPoint.intensity <= MaxIntensity){

        lightPoint.intensity += FuelAddIndex;
        // enlarge range too
        lightPoint.range += FuelAddIndex*0.5f;
        }
    }
}

