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
    private Touch touch;
    private float zRot;
    private bool canJump;
    private void Update()
    {
        if (Input.touchCount > 0 && canJump)
        {
            FollowSledOnXAxis();

            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                transform.SetParent(null);
                rb.gravityScale = 1;
                rb.AddForce(jumpHeight, ForceMode2D.Impulse);
            }
            if (touch.phase == TouchPhase.Stationary)
            {
                RotateCharacter();
                
            }
            if (touch.phase == TouchPhase.Ended)
            {
                 canJump = false;
            }
        }
        else
        {
            if (Mathf.Abs(sled.position.y - transform.position.y) < 2f)
            {
                FollowSled();
            }
            else {
                FollowSledOnXAxis();
            }
        }

    }

    private void RotateCharacter()
    {
        zRot += Time.deltaTime * speed;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, -zRot));
    }

    private void FollowSled()
    {
        rb.gravityScale = 0;
        canJump=true;
        transform.SetParent(sled);
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }
    private void FollowSledOnXAxis(){
        Vector2 tempPos=transform.position;
        tempPos.x = sled.position.x;
        transform.position = tempPos;
    }

}
