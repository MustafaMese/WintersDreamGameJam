using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameCanvas : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] TextMeshProUGUI pointText;

    private int gold = 0;
    private int point = 0;

    public void IncreaseGold(int value)
    {
        gold += value;
        goldText.text = gold.ToString();
    }

    public void IncreasePoint(int value)
    {
        point += value;
        pointText.text = point.ToString();
    }

    public void SetPanelActive(bool b)
    {
        panel.SetActive(b);
        Reset();
    }

    private void Reset()
    {
        goldText.text = gold.ToString();
        pointText.text = point.ToString();
    }

}
