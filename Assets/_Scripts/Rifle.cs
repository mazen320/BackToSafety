using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : MonoBehaviour
{
    [Header("Rifle Initals")]
    public Camera cam;
    public float damage = 10f;
    public float bulletRange = 100f;

    [Header("Rifle Effects")]
    public ParticleSystem muzzleFlash;
    public GameObject ConcreteEffect;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();

        }
    }
    private void Shoot()
    {
        muzzleFlash.Play();
        RaycastHit hitInfo;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, bulletRange))
        {
            Debug.Log(hitInfo.transform.name);
            ShootObject shootObject = hitInfo.transform.GetComponent<ShootObject>();

            if(shootObject != null)
            {
                shootObject.ObjectHitDamage(damage);
                GameObject impactShot = Instantiate(ConcreteEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(impactShot, 1f);
            }
        }
    }
}
