using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ClickButtonController _clickButton;

    private void Awake()
    {
        _clickButton.SubscribeOnOnClick(ShowClick);
    }

    private void ShowClick()
    {
        Debug.Log("Clicked");
    }
}
