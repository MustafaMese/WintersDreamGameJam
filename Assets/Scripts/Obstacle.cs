using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Obstacle : MonoBehaviour
{
    [SerializeField] GameObject particul;
    [SerializeField] SpriteRenderer sRenderer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CameraShaker.Instance.ShakeOnce(15, 2, 0.1f, 0.5f);

        particul.SetActive(true);
        StartCoroutine(DestroyObstacle());
    }

    private IEnumerator DestroyObstacle()
    {
        sRenderer.sprite = null;
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
