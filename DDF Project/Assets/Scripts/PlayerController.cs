using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using NaughtyAttributes;


public class PlayerController : MonoBehaviour
{
    //public CharacterController characterController;
    //public float MoveSpeed;
    //public float JumpSpeed;
    //public float RotationSpeed;
    //public float GravityValue = -9.81f;
    //private float gravity;
    //private Vector3 moveVector;
    //private Vector3 gravityVector;
    //public float turnSmoothTime;
    //private float turnSmoothVelocity;

    public AudioSource Music;
    //public AudioClip MusicJump;
    public AudioClip MusicRestore;
    public AudioClip MusicDamage;

    [ReadOnly] public float Height;
    public int StartHealth = 4;
    [ReadOnly] [SerializeField] private int health;
    //[SerializeField] private int fuelNumber = 0;
    [Required] public ShadowController ShadowController;
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;

        health = StartHealth;
        gameManager.HealthBar.SetMaxHealth(health);
        //characterController = GetComponent<CharacterController>();
        //rb = GetComponent<Rigidbody>();

        Height = GetComponent<Renderer>().bounds.size.y;
        Music = gameManager.Music;
        
    }

    //void Update()
    //{
    //    float horizontal = Input.GetAxisRaw("Horizontal");
    //    float vertical = Input.GetAxisRaw("Vertical");
    //    moveVector = new Vector3(-horizontal, 0f, -vertical).normalized;
    //}

/*
    void FixedUpdate()
    {
        if (characterController.isGrounded && Input.GetButton("Jump"))
        {
            // moveVector.y = JumpSpeed;
            moveVector.y = JumpSpeed;
            Music.clip = MusicJump;
            Music.Play();
        }

        Vector3 gravityVector = Vector3.up * GravityValue * Time.deltaTime;
        characterController.Move(moveVector * MoveSpeed * Time.deltaTime + gravityVector);
        
        // rotation
        float targetAngle = Mathf.Atan2(moveVector.x, moveVector.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }
*/


//    void OnCollisionEnter(Collision collision)
//    {
//        foreach (ContactPoint contact in collision.contacts)
//        {
//            Debug.DrawRay(contact.point, contact.normal, Color.white, 2.0f);
//        }

//        if (collision.gameObject.tag == "Fuel")
//        {
//            Debug.Log("hit fuel");
//            fuelNumber += 1;
//            gameManager.LightController.AddFuel();
 //           Destroy(collision.gameObject);
  //      }
    //}

    public void ReduceHealth(int value=1)
    {
        health -= value;
        // update health bar
        gameManager.HealthBar.SetHealth(health);

        Music.clip = MusicDamage;
        Music.Play();
        // add game end condition
        if (health <= 0)
        {
            //sent analyticsResult

            AnalyticsResult analyticsResult = Analytics.CustomEvent(
                "Game Over: Bullet hit",
                new Dictionary<string, object> {
                    {"Score", Score.GetScore() }
                }
            );
            Debug.Log("analyticsResult: " + analyticsResult);
            Debug.Log("die by bullet " + Score.GetScore() +"s");
            //game over
            FindObjectOfType<GameManager>().EndGame();
        }
    }

    public void GetEat()
    {
        health -= 100;
        // update health bar
        gameManager.HealthBar.SetHealth(health);

        Music.clip = MusicDamage;
        Music.Play();
        // add game end condition
        if (health <= 0)
        {
            //sent analyticsResult
            AnalyticsResult analyticsResult = Analytics.CustomEvent(
                "Game Over: Ghost eat",
                new Dictionary<string, object> {
                    {"Score", Score.GetScore() }
                }
            );
            Debug.Log("analyticsResult: " + analyticsResult);
            Debug.Log("die by ghost " + Score.GetScore() +"s");
            //game over
            FindObjectOfType<GameManager>().EndGame();
        }
    }

    public void Eat(int value = 1)
    {
        if (health < StartHealth) // if not full health, get 1 more hp
        {
            health += 1;
        }
        
        // update health bar
        gameManager.HealthBar.SetHealth(health);
        Music.clip = MusicRestore;
        Music.Play();
    }
}
