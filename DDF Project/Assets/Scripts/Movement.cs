using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using NaughtyAttributes;

public class Movement : MonoBehaviour
{
    public CharacterController characterController;

    

    public float MoveSpeed;
    public float JumpForce;
    public float Gravity; // weight
    private float JumpCD = 0f;

    public float RotationSpeed;
    private Vector3 moveDirection;
    public float turnSmoothTime;
    private float turnSmoothVelocity;

    public AudioSource Music;
    public AudioClip MusicJump;
    public GameManager gameManager;
    
    void Start()
    {
        Music = gameManager.Music;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 moveDirection = new Vector3(-horizontal, 0f, -vertical).normalized;
        /*
        if(characterController.isGrounded && Input.GetButton("Jump"))
        {
            rb.AddForce(new Vector3(0,5,0), ForceMode.Impulse);
        }
        */

        

        if (characterController.isGrounded && Input.GetButton("Jump") && JumpCD <= 0f)
        {
            JumpCD = 550f;
            moveDirection.y = JumpForce;
            Music.clip = MusicJump;
            Music.Play();
        }
        if (moveDirection.y >= 0)
        {
            moveDirection.y -= Gravity * Time.deltaTime;
        }
        
        JumpCD -= 1f;

        

        // rotation
        float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        characterController.Move(moveDirection * MoveSpeed * Time.deltaTime);
    }
}
