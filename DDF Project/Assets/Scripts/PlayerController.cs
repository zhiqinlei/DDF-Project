﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using NaughtyAttributes;


public class PlayerController : MonoBehaviour
{
    //#region Movement Settings
    //[ReadOnly] [SerializeField] private CharacterController characterController;
    //[ReadOnly] [SerializeField] private Rigidbody rb;

    //public float MoveSpeed = 5.0f;
    //public float JumpSpeed = 20.0f;
    //public float RotationSpeed = 240.0f;
    //public float Gravity = 200.0f; // weight
    //private Vector3 moveDirection = Vector3.zero;
    //private Vector3 moveDirection;
    //public float turnSmoothTime = 0.1f;
    //private float turnSmoothVelocity;

    public AudioSource Music;
    public AudioClip MusicJump;
    public AudioClip MusicRestore;
    public AudioClip MusicDamage;

    //#endregion
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

        
    }


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
