using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

public class TutorialManager : MonoBehaviour
{
    public enum State {
        Movement,
        AvoidBlock,
        AvoidBullet,
        AvoidShadow,
        PickFuel,
        ThrowFuel
    };
    [ReadOnly] public State TutorialState;
    public State EnterState;
    [Required] public GameObject MovementText;
    [Required] public GameObject AvoidBulletText;
    [Required] public GameObject AvoidBlockText;
    [Required] public GameObject AvoidShadowText;
    [Required] public GameObject PickFuelText;
    [Required] public GameObject ThrowFuelText;
    [Required] public GameObject ShadowText;
    public float Delay = 1f;
    private float tempDelay;
    private GameManager gameManager;
    private bool useHorizontal = false;
    private bool useVertical = false;
    private GameObject bulletObj;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        tempDelay = Delay;
        SetTutorialState(EnterState);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 shadowPosition = gameManager.ShadowController.transform.position;
        ShadowText.transform.position = Camera.main.WorldToScreenPoint(shadowPosition);
        
        switch (TutorialState)
        {
            case State.Movement:
                Vector3 playerPosition = gameManager.PlayerController.transform.position;
                MovementText.transform.position = Camera.main.WorldToScreenPoint(playerPosition);
                if (Input.GetAxisRaw("Horizontal") != 0)
                {
                    useHorizontal = true;
                }
                if (Input.GetAxisRaw("Vertical") != 0)
                {
                    useVertical = true;
                }
                if (useHorizontal && useVertical)
                {
                    tempDelay -= Time.deltaTime;
                    if (tempDelay < 0.0f)
                    {
                        SetTutorialState(State.AvoidBullet);
                    }
                }
                break;
            case State.AvoidBullet:
                if (bulletObj)
                {
                    AvoidBulletText.transform.position = Camera.main.WorldToScreenPoint(bulletObj.transform.position);
                }
                tempDelay -= Time.deltaTime;
                if (tempDelay < 0f)
                {
                    SetTutorialState(State.AvoidShadow);
                }
                break;
        }

        
    }

    public void SetTutorialState(TutorialManager.State state)
    {
        // exit state
        switch (TutorialState)
        {
            case State.Movement:
                MovementText.SetActive(false);
                break;
            case State.AvoidBullet:
                AvoidBulletText.SetActive(false);
                break;
        }


        TutorialState = state;

        // enter state
        switch (TutorialState)
        {
            case State.Movement:
                MovementText.gameObject.SetActive(true);
                tempDelay = 2f;
                break;
            case State.AvoidBullet:
                AvoidBulletText.gameObject.SetActive(true);
                bulletObj = gameManager.BulletManager.Shoot();
                BulletController bullet = bulletObj.GetComponent<BulletController>();
                bullet.Initialize(0.6f, 5);
                tempDelay = 5f;
                break;
            
        }
    }

    private void StartMovementTutorial()
    {
    }

    private void StartAvoidBulletTutorial()
    {

    }

    private void StartFuelTutorial()
    {

    }

    private void StartMonsterTutorial()
    {

    }
}
