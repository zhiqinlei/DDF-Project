using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public Transform target;
    public float speed;
    public float Yspeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
    	transform.position += Vector3.up * Yspeed * Time.deltaTime;
        if(transform.position.y>=2 || transform.position.y <= 0){

             Yspeed = -Yspeed;
        }

        transform.RotateAround(target.position, Vector3.up, speed * Time.deltaTime);
        if (transform.position.x>=2 || transform.position.x<=-2)
            {
                speed = -speed;
            }
    }
}

