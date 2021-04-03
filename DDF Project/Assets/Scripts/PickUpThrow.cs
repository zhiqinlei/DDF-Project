using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpThrow : MonoBehaviour
{
    public Transform ObjectHolder;
    public float ThrowForce;
    public bool carryObject;
    private GameObject Item;
    public bool IsThrowable;
    // add light source
    public Transform LightSource;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        //Item = GameObject.FindGameObjectWithTag("Fuel");
        gameManager = GameManager.Instance;
    }

    

    // Update is called once per frame
    void Update()
    {
        //Item = GameObject.FindGameObjectWithTag("Fuel");
        // change to items

        GameObject[] Items = GameObject.FindGameObjectsWithTag("Fuel");
        foreach (GameObject item in Items)
        {
            if (carryObject!= true)
            {
                Item = item;
            }
            
        }

        if(Item == null)
        {
            return;
        }
        
        

        if(Input.GetKeyDown(KeyCode.E))
        {
            //RaycastHit hit;
            //Ray directionRay = new Ray(transform.position, transform.forward);
            //if(Physics.Raycast(directionRay, out hit, 0f))
            
            float dist = Vector3.Distance(ObjectHolder.transform.position, Item.transform.position);
            {
                //if(hit.collider.tag == "Fuel")
                if (dist < 1.7f)
                {
                    carryObject = true;
                    IsThrowable = true;
                    if(carryObject == true)
                    {
                        //Item = hit.collider.gameObject;
                        Item.transform.SetParent(ObjectHolder);
                        Item.gameObject.transform.position = ObjectHolder.position;
                        Item.GetComponent<Rigidbody>().isKinematic = true;
                        Item.GetComponent<Rigidbody>().useGravity = false;
                    }
                }
            }
            
        }
//        if (Input.GetMouseButton(0))
//        {
//            carryObject = false;
//            IsThrowable = false;
//        }
        if (carryObject == false)
        {
            ObjectHolder.DetachChildren();
            Item.GetComponent<Rigidbody>().isKinematic = false;
            Item.GetComponent<Rigidbody>().useGravity = true;


            
        }
//        if (Input.GetMouseButton(1))
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if (IsThrowable)
            {
                ObjectHolder.DetachChildren();
                Item.GetComponent<Rigidbody>().isKinematic = false;
                Item.GetComponent<Rigidbody>().useGravity = true;
                //Item.GetComponent<Rigidbody>().AddRelativeForce(transform.forward * ThrowForce);
                Item.GetComponent<Rigidbody>().AddForce(transform.forward * ThrowForce);
            }

            carryObject = false;
            IsThrowable = false;
        }

        // add fuel
        float d = Vector3.Distance(Item.transform.position, LightSource.transform.position);
        if (d < 1f)
        {
            Debug.Log("add fuel");
            gameManager.LightController.AddFuel();
            Destroy(Item);
        }

        
        
    }

    

    
}
