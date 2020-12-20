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

    
    public void SetPanelActive(bool b)
    {
        panel.SetActive(b);

    }
    
}
