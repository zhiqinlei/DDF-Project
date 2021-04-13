using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class PickUpThrow : MonoBehaviour
{
    public Transform ObjectHolder;
    public float ThrowForce;
    public static bool carryObject;
    private GameObject Item;
    public bool IsThrowable;
    // add light source
    private GameObject LightSource;
    private GameManager gameManager;

    private AudioSource Music;
    [Required] public AudioClip MusicThrow;

    private float cd = 0f;// cd of pick&throw

    // Start is called before the first frame update
    void Start()
    {
        //Item = GameObject.FindGameObjectWithTag("Fuel");
        gameManager = GameManager.Instance;
        LightSource = gameManager.LightController.LightPosition;
        Music = gameManager.Music;
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

            if(Item == null)
            {
                // when Item get destoryed, set carryObject & IsThrowable to false
                cd = 0f;
                carryObject = false;
                IsThrowable = false;
                return;
            }

            // add fuel
            float d = Vector3.Distance(Item.transform.position, LightSource.transform.position);
            if (d < 1.3f)
            {
                Debug.Log("add fuel");
                gameManager.LightController.AddFuel();
                Destroy(Item);
                return;
            }

            
        }

        if(Item == null)
        {
            // when Item get destoryed, set carryObject & IsThrowable to false
            cd = 0f;
            carryObject = false;
            IsThrowable = false;
            return;
        }

        
        
        

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            //RaycastHit hit;
            //Ray directionRay = new Ray(transform.position, transform.forward);
            //if(Physics.Raycast(directionRay, out hit, 0f))

            
            
            
            float dist = Vector3.Distance(ObjectHolder.transform.position, Item.transform.position);
            {
                //if(hit.collider.tag == "Fuel")
                if (dist < 1.5f)
                {
                    cd += 1f;
                    
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
        if(Input.GetKeyDown(KeyCode.LeftShift) && cd >= 2f)
        {
            if (IsThrowable)
            {
                Music.clip = MusicThrow;
                Music.Play();
                ObjectHolder.DetachChildren();
                Item.GetComponent<Rigidbody>().isKinematic = false;
                Item.GetComponent<Rigidbody>().useGravity = true;
                //Item.GetComponent<Rigidbody>().AddRelativeForce(transform.forward * ThrowForce);
                Item.GetComponent<Rigidbody>().AddForce(transform.forward * ThrowForce);

            }

            carryObject = false;
            IsThrowable = false;

            cd = 0f;
        }

        

        
        
    }

    

    
}
