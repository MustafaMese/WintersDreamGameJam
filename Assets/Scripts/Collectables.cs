using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    [SerializeField] ParticleSystem firstParticul;
    [SerializeField] ParticleSystem secondParticul;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        UIManager.Instance.IncreaseGold(1);
        StartCoroutine(DestroyCollectable());
    }

    private IEnumerator DestroyCollectable()
    {
        firstParticul.gameObject.SetActive(false);
        secondParticul.gameObject.SetActive(true);
        secondParticul.Play();
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
