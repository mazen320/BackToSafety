using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    public GameObject aimCam;
    public GameObject aimCanvas;
    public GameObject thirdPersonCanvas;
    public GameObject thirdPersonCam;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire2"))
        {
            thirdPersonCam.SetActive(false);
            thirdPersonCanvas.SetActive(false);
            aimCam.SetActive(true);
            aimCanvas.SetActive(true);
        }
        else
        {
            thirdPersonCam.SetActive(true);
            thirdPersonCanvas.SetActive(true);
            aimCam.SetActive(false);
            aimCanvas.SetActive(false);
        }
    }
    
}
