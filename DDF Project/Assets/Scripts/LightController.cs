using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public float speed;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
    	transform.position += Vector3.left * speed * Time.deltaTime;
        if(transform.position.x>=4 || transform.position.x <=-5 ){

             speed = -speed;
        }
    }

}

