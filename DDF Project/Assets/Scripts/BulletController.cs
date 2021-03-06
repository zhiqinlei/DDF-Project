﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class BulletController : MonoBehaviour
{
    private float m_Speed;
    public float m_ExistingTime;

    public void Initialize(float speed, float existingTime)
    {
        m_Speed = speed;
        m_ExistingTime = existingTime;
    }

    void Update()
    {
        Vector3 moveDirection = transform.forward;
        transform.position += moveDirection * m_Speed * Time.deltaTime;

        m_ExistingTime -= Time.deltaTime;
        if (m_ExistingTime <= 0.0f)
        {
            Destroy(gameObject);
        }
    }
}
