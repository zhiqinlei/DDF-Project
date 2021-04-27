using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using NaughtyAttributes;

public class TutorialManager : MonoBehaviour
{
    public enum State {
        Movement,
        AvoidBlock,
        AvoidBullet,
        AvoidShadowMonster,
        PickFuel,
        ThrowFuel,
        End
    };
    [ReadOnly] public State TutorialState;
    public State EnterState;
    [Required] public GameObject MovementText;
    [Required] public GameObject AvoidBulletText;
    [Required] public GameObject AvoidBulletText2;
    [Required] public GameObject AvoidBlockText;
    [Required] public GameObject AvoidShadowText;
    [Required] public GameObject PickFuelText;
    [Required] public GameObject ThrowFuelText;
    [Required] public GameObject ShadowText;
    public float Delay = 1f;
    private float tempDelay;
    private GameManager gameManager;
    private GameObject bulletObj;
    private GameObject monsterObj;
    private GameObject fuelObj;


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
                if (Input.GetButtonDown("Submit"))
                {
                    SetTutorialState(State.AvoidBullet);
                }
                break;
            case State.AvoidBullet:
                if (bulletObj)
                {
                    AvoidBulletText.transform.position = Camera.main.WorldToScreenPoint(bulletObj.transform.position);
                }
                if (Input.GetButtonDown("Submit"))
                {
                    if (bulletObj)
                    {
                        bulletObj.GetComponent<BulletController>().ResumeMovement();
                        AvoidBulletText2.gameObject.SetActive(false);
                        StartCoroutine(DestroyBulletAfterTime(5));
                    }
                    SetTutorialState(State.AvoidShadowMonster);
                }
                break;
            case State.AvoidShadowMonster:
                if (monsterObj)
                {
                    AvoidShadowText.gameObject.transform.position = Camera.main.WorldToScreenPoint(monsterObj.transform.position);
                }
                
                if (Input.GetButtonDown("Submit"))
                {
                    if (monsterObj)
                    {
                        monsterObj.GetComponent<MonsterController>().ResumeMovement();
                        AvoidShadowText.SetActive(false);
                    }
                    SetTutorialState(State.PickFuel);
                }
                break;
            case State.PickFuel:
                if (fuelObj)
                {
                    PickFuelText.gameObject.transform.position = Camera.main.WorldToScreenPoint(fuelObj.transform.position);
                }
                if (Input.GetButtonDown("Submit"))
                {
                    SetTutorialState(State.ThrowFuel);
                }
                break;
            case State.ThrowFuel:
                if (Input.GetButtonDown("Submit"))
                {
                    SetTutorialState(State.End);
                }
                break;
            case State.End:
                if (Input.GetButtonDown("Submit"))
                {
                    SceneManager.LoadScene("Game");
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
                AvoidBulletText2.SetActive(false);
                break;
            case State.AvoidShadowMonster:
                AvoidShadowText.SetActive(false);
                break;
        }


        TutorialState = state;

        // enter state
        switch (TutorialState)
        {
            case State.Movement:
                MovementText.SetActive(true);
                break;
            case State.AvoidBullet:
                Debug.Log("Enter avoid bullet");
                AvoidBulletText.SetActive(true);
                AvoidBulletText2.SetActive(false);

                // generate bullet in the middle
                BulletManager bulletManager = gameManager.BulletManager;
                Vector3 bulletStartPos = new Vector3(
                    (bulletManager.PositionBotton.transform.position.x + bulletManager.PositionTop.transform.position.x)/2.0f,
                    bulletManager.PositionBotton.transform.position.y,
                    bulletManager.PositionBotton.transform.position.z);
                bulletObj = bulletManager.Shoot(bulletStartPos);
                BulletController bullet = bulletObj.GetComponent<BulletController>();
                bullet.Initialize(1f, 100);

                // pause bullet after some time
                StartCoroutine(PauseBulletAfterTime(5));
                break;
            case State.AvoidShadowMonster:
                Debug.Log("Enter avoid shadow");
                AvoidShadowText.SetActive(true);

                // generate shadow monster on the right
                monsterObj = gameManager.MonsterManager.Show(
                    1.0f,
                    new Vector3(gameManager.MonsterManager.PositionTop.transform.position.x,
                        gameManager.MonsterManager.PositionButton.transform.position.y - (float) 1.25 + 1,
                        gameManager.MonsterManager.PositionButton.transform.position.z),
                    0);

                // pause monster after some time
                StartCoroutine(PauseMonsterAfterTime(5));
                break;
            case State.PickFuel:
                Debug.Log("Enter pick up fuel");
                PickFuelText.SetActive(true);

                FuelManager fuelManager = gameManager.FuelManager;
                // generate fuel in the middle
                Vector3 startPos = new Vector3(
                    (fuelManager.XRangeMin.transform.position.x + fuelManager.XRangeMax.transform.position.x)/2,
                    fuelManager.FuelGroup.transform.position.y,
                    (fuelManager.ZRangeMin.transform.position.z + fuelManager.ZRangeMax.transform.position.z)/2
                );
                fuelObj = fuelManager.GenerateFuel(startPos, alwaysExist:true);
                break;
        }
    }

    IEnumerator PauseBulletAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        AvoidBulletText2.SetActive(true);
        bulletObj.GetComponent<BulletController>().PauseMovement();
    }

    IEnumerator DestroyBulletAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        Destroy(bulletObj);
    }

    IEnumerator PauseMonsterAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        if (monsterObj)
        {
            monsterObj.GetComponent<MonsterController>().PauseMovement();
        }
    }

    IEnumerator DestroyMonsterAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        if (monsterObj)
        {
            Destroy(monsterObj);
        }
    }
}
