using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class FuelController : MonoBehaviour
{
    public float m_ExistingTime;
    public FuelShadowController Shadow;

    public void Initialize(float existingTime, FuelShadowController shadow)
    {
        m_ExistingTime = existingTime;
        Shadow = shadow;
    }

    void Update()
    {
        m_ExistingTime -= Time.deltaTime;
        if (m_ExistingTime <= 0.0f)
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        Debug.Log("destroy fuel");
        Destroy(Shadow.gameObject);
        Destroy(gameObject);
    }
}
