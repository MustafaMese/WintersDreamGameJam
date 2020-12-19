using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] GameObject particul;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Partiküller uçuşur
        //particul.SetActive(true);
        StartCoroutine(DestroyObstacle());
    }

    private IEnumerator DestroyObstacle()
    {
        gameObject.SetActive(false);
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
