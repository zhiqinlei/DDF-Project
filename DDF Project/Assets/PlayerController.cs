using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class PlayerController : MonoBehaviour
{
    #region Movement Settings
    [ReadOnly] [SerializeField] private CharacterController characterController;
    [ReadOnly] [SerializeField] private Rigidbody playerRigidbody;
    public float MoveSpeed = 5.0f;
    public float JumpSpeed = 5.0f;
    public float RotationSpeed = 240.0f;
    public float Gravity = 20.0f;
    private Vector3 moveDirection = Vector3.zero;

    #endregion
    public int StartHealth = 4;
    [ReadOnly] [SerializeField] private int health;

    [Required] public GameObject LightSource;
    [Required] public ShadowController ShadowController;
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;

        health = StartHealth;
        characterController = GetComponent<CharacterController>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 move = new Vector3(horizontal, 0f, vertical);
        if (move.magnitude > 1.0f)
        {
            move.Normalize();
        }

        if (characterController.isGrounded)
        {
            moveDirection = transform.forward * move.magnitude * MoveSpeed;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = JumpSpeed;
            }
        }

        if (direction.magnitude >= 0.1f)
        {
            float turnAmount = Mathf.Atan2(direction.x, direction.z);
            transform.Rotate(0, turnAmount * RotationSpeed * Time.deltaTime, 0);
        }
    }
}
