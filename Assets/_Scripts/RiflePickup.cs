using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiflePickup : MonoBehaviour
{
    [Header("Rifle")]
    public GameObject PlayerRifle;  //the one on the player already
    public GameObject PickupRifle;  //the one on the wall,or ground thats going to be picked up
    public PlayerPunch playerPunch;

    [Header("Rifle Assigning")]
    public PlayerScript player;
    private float pickupRadius = 3f;    //radius of how far can the player be away from the gun to pick it up.
    public Animator anim;
    private float nextTimetoPunch = 0f;
    public float punchCharge = 15f;

    private void Awake()
    {
        PlayerRifle.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetButton("Fire1") && Time.time >= nextTimetoPunch)
        {
            anim.SetBool("Punch", true);
            anim.SetBool("Idle", false);

            nextTimetoPunch = Time.time + 1f / punchCharge;
            playerPunch.Punch();
        }
        else
        {
            anim.SetBool("Idle", true);
            anim.SetBool("Punch", false);

        }

        if (Vector3.Distance(transform.position, player.transform.position) < pickupRadius)
        {
            if (Input.GetKeyDown("f"))
            {
                PlayerRifle.SetActive(true);
                PickupRifle.SetActive(false);
            }
        }
    }
}
