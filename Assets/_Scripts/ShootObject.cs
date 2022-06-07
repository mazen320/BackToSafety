using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootObject : MonoBehaviour
{
    public float objectHealth = 30f;

    public void ObjectHitDamage(float dmg)
    {
        objectHealth -= dmg;
        if (objectHealth <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
