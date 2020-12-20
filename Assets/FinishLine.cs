using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [SerializeField] FollowCamera mainCamera;
    [SerializeField] GameObject newLoc;
    [SerializeField] GameObject confetti;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        mainCamera.target = newLoc.transform;
        confetti.GetComponent<ParticleSystem>().Play();
    }
}
