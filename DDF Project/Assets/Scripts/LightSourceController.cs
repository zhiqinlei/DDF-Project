using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSourceController : MonoBehaviour
{

	//public GameObject obj;
    Light lightPoint;
    public float LightReduceIndex; // describes how fast the light goes off
    public float FuelAddIndex; // describes how many intensity is add when eat a fuel
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        lightPoint = this.GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        LightOff();    
    }

    private void LightOff(){
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
