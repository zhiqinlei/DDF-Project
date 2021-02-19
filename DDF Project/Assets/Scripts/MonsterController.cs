using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class MonsterController : MonoBehaviour
{
    private float m_Speed;

    public void Initialize(float speed)
    {
        m_Speed = speed;
    }

    void Update()
    {
        Vector3 moveDirection = transform.forward;
        transform.position += moveDirection * m_Speed * Time.deltaTime;
    }
}
