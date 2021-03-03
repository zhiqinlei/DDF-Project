using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class LightController : MonoBehaviour
{
    [Required] public GameObject LightPosition;
    [Required] public GameObject LightPositionLeft;
    [Required] public GameObject LightPositionRight;
    public float speed;
    [Required] public Light lightPoint;
    public float LightReduceIndex; // describes how fast the light goes off
    public float FuelAddIndex; // describes how many intensity is add when eat a fuel

    void Update()
    {
        LightOff();
    }

    void FixedUpdate()
    {
    	UpdatePosition();
    }

    private void UpdatePosition()
    {
        LightPosition.transform.position += Vector3.left * speed * Time.deltaTime;
        if (LightPosition.transform.position.x >= LightPositionRight.transform.position.x || 
            LightPosition.transform.position.x <= LightPositionLeft.transform.position.x )
        {
            speed = -speed;
        }
    }

    private void LightOff()
    {
        lightPoint.intensity -= LightReduceIndex*Time.deltaTime;

        if (lightPoint.intensity<=0.0){
            FindObjectOfType<GameManager>().EndGame();
        }
    }

    public void AddFuel()
    {
        lightPoint.intensity += FuelAddIndex;
    }
}

