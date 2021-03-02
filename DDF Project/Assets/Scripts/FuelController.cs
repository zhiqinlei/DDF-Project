using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelController : MonoBehaviour
{
    public float m_ExistingTime;

    public void Initialize(float existingTime)
    {
        m_ExistingTime = existingTime;
    }

    void Update()
    {
        m_ExistingTime -= Time.deltaTime;
        if (m_ExistingTime <= 0.0f)
        {
            Destroy(gameObject);
        }
    }
}
