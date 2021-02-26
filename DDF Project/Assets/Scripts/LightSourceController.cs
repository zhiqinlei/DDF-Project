using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSourceController : MonoBehaviour
{

	//public GameObject obj;
    Light lightPoint;
    public float index;
    // Start is called before the first frame update
    void Start()
    {
        lightPoint = this.GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
       LightOff();    
    }

    private void LightOff(){
        lightPoint.intensity -= index*Time.deltaTime;

        if(lightPoint.intensity<=0.0){
            FindObjectOfType<GameManager>().EndGame();
        }
    }
}
