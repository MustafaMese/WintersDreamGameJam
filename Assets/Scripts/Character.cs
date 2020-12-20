using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterState
{
    OnSled,
    OnFlying
}
public class Character : MonoBehaviour
{
    [SerializeField] Vector2 jumpHeight;
    [SerializeField] float speed;
    [SerializeField] Transform sled;
    [SerializeField] Rigidbody2D rb;

    private CharacterState characterState;
    
    private float zRot;
    private bool canJump;
    private bool canRotate;

    // Touching variables
    private Touch touch;
    private bool isStationary;
    private bool isBegan;

    // Starting variable
    public bool isTouched;

    private void Start()
    {
        isTouched = false;
    }

    private void Update()
    {
        if (!isTouched) return;

        CalculateForceAccordingToAngle();
        if (Input.touchCount > 0 && canJump)
        {
            FollowSledOnXAxis();
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                isBegan = true;
            }
            if (touch.phase == TouchPhase.Stationary)
            {
                isStationary = true;
            }
            if (touch.phase == TouchPhase.Ended)
            {
                canJump = false;
                isStationary = false;
                canRotate = false;
            }
        }
        else
        {
            if (Mathf.Abs(sled.position.y - transform.position.y) < 1)
            {
                FollowSled();
            }
            else
            {
                FollowSledOnXAxis();
            }
        }

    }

    private void FixedUpdate()
    {
        if (isStationary)
        {
            if (isBegan)
            {
                rb.velocity = Vector3.zero;
                rb.gravityScale = 1;
                rb.AddForce(jumpHeight, ForceMode2D.Impulse);
                isBegan = false;
                canRotate = true;
                print("1");
            }
            RotateCharacter();
        }
    }

    private void RotateCharacter()
    {
        if (!canRotate) return;

        zRot += Time.deltaTime * speed;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, -zRot));
    }

    private void FollowSled()
    {
        rb.gravityScale = 0;
        canJump = true;
        canRotate = false;
        transform.position = sled.position + Vector3.up * 0.5f;
        transform.rotation = sled.rotation;
    }
    private void FollowSledOnXAxis()
    {
        Vector2 tempPos = transform.position;
        tempPos.x = sled.position.x;
        transform.position = tempPos;
    }
    private void CalculateForceAccordingToAngle(){
        float zAngle = sled.eulerAngles.z;
        if(zAngle < 330 && zAngle > 300){
            jumpHeight.y =2;
        }
        else{
            jumpHeight.y = 10;
        }
    }
}
