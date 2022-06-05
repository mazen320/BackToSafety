using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPunch : MonoBehaviour
{
    public Camera cam;
    public float dmg = 10f;
    public float punchRange = 3f;

    public void Punch()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, punchRange))
        {
            Debug.Log(hitInfo.transform.name);
            ShootObject shootObject = hitInfo.transform.GetComponent<ShootObject>();

            if (shootObject != null)
            {
                shootObject.ObjectHitDamage(dmg);
            }
        }
    }
}
