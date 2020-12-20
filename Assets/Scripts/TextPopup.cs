using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextPopup : MonoBehaviour
{
    [SerializeField] float xSpace = 0;
    [SerializeField] float ySpace = 0;

    [SerializeField] TextMeshPro textMeshPro = null;
    [SerializeField] Color perfect;
    [SerializeField] Color moderate;
    [SerializeField] Color fail;
    [SerializeField] float disapperTime = 0;


    private void Start()
    {
        DOTween.Init();
    }

    public void Setup(int value, Vector3 target, bool isPerfect = false, bool isFail = false)
    {
        gameObject.SetActive(true);

        if (isPerfect)
            textMeshPro.color = perfect;
        else if (isFail)
            textMeshPro.color = fail;
        else
            textMeshPro.color = moderate;
        
        textMeshPro.text = value.ToString();
        Move(target);
    }

    private void Move(Vector3 target)
    {
        float number = Random.Range(-xSpace * 2, xSpace * 2);
        Vector2 targetPos = new Vector2(target.x + number, target.y + ySpace);
        transform.DOMove(targetPos, disapperTime).OnComplete(() => FinishMove());
    }

    private void FinishMove()
    {
        if (gameObject.activeSelf)
            gameObject.SetActive(false);

    }

    public float GetDisapperTime()
    {
        return disapperTime;
    }
}
