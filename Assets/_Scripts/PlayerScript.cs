using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Player Movement")]
    public float playerSpeed = 2f;

    [Header("Gravity & Animator")]
    public CharacterController cC;

    [Header("Jumping & velocity")]
    public float turnTime = 0.1f;   //for smooth rotation
    float turnVelocity;

    void Update()
    {
        playerMovement();
    }

    void playerMovement()
    {
        float horizontal_axis = Input.GetAxisRaw("Horizontal");
        float vertical_axis = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal_axis, 0f, vertical_axis).normalized;

        if(direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnVelocity, turnTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            cC.Move(direction.normalized * playerSpeed * Time.deltaTime);
        }
    }
}
