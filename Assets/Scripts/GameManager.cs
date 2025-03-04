using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ClickButtonManager _clickButtonManager;
    [SerializeField] private EnemyManager _enemyManager;
    [SerializeField] private HealthBar _healthBar;
    
    private void Awake()
    {
        _clickButtonManager.Initialize();
        _enemyManager.Initialize(_healthBar);

        _clickButtonManager.OnClicked += () => _enemyManager.DamageCurrentEnemy(10f);
    }
}
