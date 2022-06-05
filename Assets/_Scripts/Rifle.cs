using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : MonoBehaviour
{
    [Header("Rifle Initals")]
    public Camera cam;
    public float damage = 10f;
    public float bulletRange = 100f;
    public float fireCharge = 15f;
    private float nextTimeToShoot = 0;
    public Animator anim;
    public PlayerScript player;
    public Transform hand;

    [Header("Rifle shooting and ammunitition")]
    private int maxAmmo = 32;
    public int mag = 10;
    private int currentAmmo;
    public float reloadTime = 1.3f;
    private bool isReloading = false;

    [Header("Rifle Effects")]
    public ParticleSystem muzzleFlash;
    public GameObject ConcreteEffect;


    private void Awake()
    {
        transform.SetParent(hand);
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        if (isReloading)
            return;

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButton("Fire1") && Time.time >=nextTimeToShoot)
        {
            anim.SetBool("Fire", true);
            anim.SetBool("Idle", false);
            nextTimeToShoot = Time.time + 1f / fireCharge;
            Shoot();
        }
        else if(Input.GetButton("Fire1") && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
            anim.SetBool("FireWalk", true);
            anim.SetBool("Idle", false);
        }
        else if(Input.GetButton("Fire2") && Input.GetButton("Fire1"))
            {
            anim.SetBool("Idle", false);
            anim.SetBool("IdleAim", true);
            anim.SetBool("FireWalk", false);
        }
        else
        {
            anim.SetBool("Idle", true);
            anim.SetBool("FireWalk", false);
            anim.SetBool("Fire", false);
        }
    }
    private void Shoot()
    {
        //check the mag
        if(mag == 0)
        {
            //show ammo in text
            return;
        }
        currentAmmo--;

        if(currentAmmo == 0)
        {
            mag--;
        }
        //update ui when implemented from here

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
    IEnumerator Reload()
    {
        player.playerSpeed = 0f;
        player.playerSprint = 0f;
        isReloading = true;
        Debug.Log("Reloading");

        anim.SetBool("Reloading", true);
        //anim
        //sound
        yield return new WaitForSeconds(reloadTime);
        anim.SetBool("Reloading", false);
        //anim
        currentAmmo = maxAmmo;
        player.playerSpeed = 1.9f;
        player.playerSprint = 3;
        isReloading = false;
    }
}
