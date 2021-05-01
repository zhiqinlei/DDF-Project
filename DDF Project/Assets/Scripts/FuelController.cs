using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class FuelController : MonoBehaviour
{
    public float m_ExistingTime;
    public bool AlwaysExist;
    public FuelShadowController Shadow;
    private GameManager gameManager;
    private GameObject lightSource;
    [ReadOnly] public PlayerController holder;

    void Start()
    {
        gameManager = GameManager.Instance;
        lightSource = gameManager.LightController.LightPosition;
    }

    public void Initialize(float existingTime, FuelShadowController shadow, bool alwaysExist=false)
    {
        m_ExistingTime = existingTime;
        Shadow = shadow;
        AlwaysExist = alwaysExist;
    }

    void Update()
    {
        if (gameManager.GameMode != GameManager.Mode.Tutorial || !AlwaysExist)
        {
            m_ExistingTime -= Time.deltaTime;
            if (m_ExistingTime <= 0.0f)
            {
                Destroy(gameObject);
            }
        }
    }

    void LateUpdate()
    {
        float d = Vector3.Distance(transform.position, lightSource.transform.position);
        if (d < 1.3f)
        {
            Debug.Log("add fuel");
            gameManager.LightController.AddFuel();
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        Debug.Log("destroy fuel");
        if (holder)
        {
            holder.carriedFuelObj = null;
        }
        Destroy(Shadow.gameObject);
        Destroy(gameObject);
    }
}
