using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    [SerializeField] GameObject firstParticul;
    [SerializeField] GameObject secondParticul;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        UIManager.Instance.IncreaseGold(1);
        StartCoroutine(DestroyCollectable());
    }

    private IEnumerator DestroyCollectable()
    {
        firstParticul.SetActive(false);
        secondParticul.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
