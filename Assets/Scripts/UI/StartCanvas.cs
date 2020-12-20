using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StartCanvas : MonoBehaviour
{
    [SerializeField] GameObject tapToStartPanel = null;

    public void SetPanelActive(bool b)
    {
        tapToStartPanel.SetActive(b);
    }

    // Button Action
    public void Tap()
    {
        FindObjectOfType<Sled>().start = true;
        FindObjectOfType<Character>().isTouched = true;
        SetPanelActive(false);
        GameManager.Instance.SetGameState(GameState.PLAY);
    }

}
