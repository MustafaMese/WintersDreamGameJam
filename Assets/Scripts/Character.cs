using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterState
{
    OnSled,
    OnSledOnlyJust,
    OnFlyingRise,
    OnFlyingFall
}
public class Character : MonoBehaviour
{
    [SerializeField] Vector2 jumpHeight;
    [SerializeField] float speed;
    [SerializeField] Transform sled;
    [SerializeField] Rigidbody2D rb;

    private CharacterState characterState;

    private float zRot;
    private bool lastPositionControl;

    // Touching variables
    private Touch touch;

    // Starting variable
    [HideInInspector] public bool isTouched;

    [SerializeField] GameObject perfectParticle;
    [SerializeField] GameObject moderateParticle;
    [SerializeField] GameObject failParticle;

    [SerializeField] Vector3 lastPosition;

    private void Start()
    {
        isTouched = false;
        characterState = CharacterState.OnSled;
        lastPosition = transform.position;
        StateControl();

    }

    private void Update()
    {

        if (characterState == CharacterState.OnSled || characterState == CharacterState.OnSledOnlyJust)
            FollowSled();
        else
            FollowSledOnXAxis();

        if (!isTouched) return;

        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                if(characterState == CharacterState.OnSled)
                {
                    Jump();
                    characterState = CharacterState.OnFlyingRise;
                    lastPositionControl = false;
                }
            }
            if (touch.phase == TouchPhase.Stationary)
            {
                if (characterState == CharacterState.OnFlyingFall || characterState == CharacterState.OnFlyingRise) 
                    RotateCharacter();
            }
            if (touch.phase == TouchPhase.Ended)
            {

            }
        }

        
    }

    private void FixedUpdate()
    {
        if (!isTouched) return;

        
        StateControl();
        CalculateForceAccordingToAngle();
        lastPosition = transform.position;
    }

    private void VerticalSpeedControl()
    {
        float y = rb.velocity.y;
        print(y);
        if (y < -15)
            y = -15;

        rb.velocity = new Vector2(rb.velocity.x, y);
    }

    private void StateControl()
    {
        switch (characterState)
        {
            case CharacterState.OnSled:
                break;
            case CharacterState.OnSledOnlyJust:
                characterState = CharacterState.OnSled;
                break;
            case CharacterState.OnFlyingRise:
                FollowSledOnXAxis();

                if (lastPositionControl && lastPosition.y > transform.position.y)
                    characterState = CharacterState.OnFlyingFall;

                lastPositionControl = true;
                break;
            case CharacterState.OnFlyingFall:

                if (Mathf.Abs(sled.position.y - transform.position.y) < 1)
                {
                    WinAndLoseCondition();
                    FollowSled();
                    characterState = CharacterState.OnSledOnlyJust;
                }
                else
                    FollowSledOnXAxis();

                break;
            default:
                break;
        }
    }


    private void Jump()
    {
        rb.velocity = Vector3.zero;
        rb.gravityScale = 1;
        rb.AddForce(jumpHeight, ForceMode2D.Impulse);
    }

    private void RotateCharacter()
    {
        zRot += Time.deltaTime * speed;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, -zRot));
    }

    private void FollowSled()
    {
        rb.gravityScale = 0;
        transform.position = sled.position + Vector3.up * 0.5f;
        transform.rotation = sled.rotation;
    }

    private void WinAndLoseCondition()
    {
        var zValue = transform.eulerAngles.z;

        if (zValue > 130 && zValue < 205)
        {
            StartCoroutine(ActivateParticle(failParticle));
        }
        else if ((zValue > 320 && zValue < 359) || (zValue > 0 && zValue < 30))
        {
            StartCoroutine(ActivateParticle(perfectParticle));
        }
        else
            StartCoroutine(ActivateParticle(moderateParticle));

    }

    private IEnumerator ActivateParticle(GameObject particle)
    {
        particle.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        particle.SetActive(false);
    }

    private void FollowSledOnXAxis()
    {
        Vector2 tempPos = transform.position;
        tempPos.x = sled.position.x;
        transform.position = tempPos;
    }
    private void CalculateForceAccordingToAngle()
    {
        float zAngle = sled.eulerAngles.z;
        if(zAngle < 330 && zAngle > 300){
            jumpHeight.y =2;
        }
        else
        {
            jumpHeight.y = 10;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }
}
