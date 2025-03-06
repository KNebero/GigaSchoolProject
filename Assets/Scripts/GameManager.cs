using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	[SerializeField] private ClickButtonManager _clickButtonManager;
	[SerializeField] private EnemyManager _enemyManager;
	[SerializeField] private HealthBar _healthBar;
	[SerializeField] private Timer _timer;
	[SerializeField] private EndLevelWindow _endLevelWindow;

	private void Awake()
	{
		_clickButtonManager.Initialize();
		_enemyManager.Initialize(_healthBar);
		_endLevelWindow.Initialize();

		_clickButtonManager.OnClicked += () => _enemyManager.DamageCurrentEnemy(10f);
		_endLevelWindow.OnRestartClicked += StartLevel;
		_enemyManager.OnLevelPassed += () =>
		{
			_endLevelWindow.ShowWinWindow();
			_timer.Stop();
		};

		StartLevel();
	}

	private void StartLevel()
	{
		_enemyManager.SpawnEnemy();
		_timer.Initialize(10f);

		_timer.Play();
		_timer.OnTimerEnd += _endLevelWindow.ShowLooseWindow;
		
	}
}