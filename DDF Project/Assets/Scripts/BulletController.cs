using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class BulletController : MonoBehaviour
{
    public float mSpeed;
    public float ExistingTime;
    public float tempSpeed;

    public void Initialize(float speed, float existingTime)
    {
        mSpeed = speed;
        ExistingTime = existingTime;
    }

    void Update()
    {
        Vector3 moveDirection = transform.forward;
        transform.position += moveDirection * mSpeed * Time.deltaTime;

        ExistingTime -= Time.deltaTime;
        if (ExistingTime <= 0.0f)
        {
            Destroy(gameObject);
        }
    }

    public void PauseMovement()
    {
        tempSpeed = mSpeed;
        mSpeed = 0;
    }

    public void ResumeMovement()
    {
        mSpeed = tempSpeed;
    }
}
