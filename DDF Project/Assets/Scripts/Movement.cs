using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using NaughtyAttributes;

public class Movement : MonoBehaviour
{
    public CharacterController characterController;

    public float MoveSpeed;
    public float JumpSpeed;
    public float RotationSpeed;
    public float Gravity; // weight

    private Vector3 moveDirection;
    public float turnSmoothTime;
    private float turnSmoothVelocity;

    public AudioSource Music;
    public AudioClip MusicJump;
    private GameManager gameManager;
    
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

        if (characterController.isGrounded && Input.GetButton("Jump"))
        {
            moveDirection.y = JumpSpeed;
            Music.clip = MusicJump;
            Music.Play();
        }
        moveDirection.y -= Gravity * Time.deltaTime;

        // rotation
        float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        characterController.Move(moveDirection * MoveSpeed * Time.deltaTime);
    }
}
