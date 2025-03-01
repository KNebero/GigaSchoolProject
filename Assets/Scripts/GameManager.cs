using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ClickButtonManager _clickButtonManager;
    
    private void Awake()
    {
        _clickButtonManager.Initialize();
    }
}
