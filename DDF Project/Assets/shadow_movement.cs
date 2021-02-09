using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shadow_movement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 4f;

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        
        Vector3 direction = new Vector3(horizontal, 0f, 0f).normalized;
    
        //CharacterController = GetComponent(CharacterController);

        Vector3 temp;

        if (direction.magnitude >= 0.1f)
        {
            controller.Move(direction * speed * Time.deltaTime);
        }
        
        
        if (Input.GetKey ("s")){
            temp = transform.localScale;
            temp.x += 0.01f;
            temp.y += 0.01f;
            transform.localScale = temp;
        }

        if (Input.GetKey ("w")){
            temp = transform.localScale;
            temp.x -= 0.01f;
            temp.y -= 0.01f;
            transform.localScale = temp;
        }
        
    }
}
