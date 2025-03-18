using System;
using SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : EntryPoint
{
	[SerializeField] private ClickButtonManager _clickButtonManager;
	[SerializeField] private EnemyManager _enemyManager;
	[SerializeField] private HealthBar _healthBar;
	[SerializeField] private Timer _timer;
	[SerializeField] private EndLevelWindow _endLevelWindow;

	private const string SCENE_LOADER_TAG = "SceneLoader";

	public override void Run(SceneEnterParams enterParams)
	{
		_clickButtonManager.Initialize();
		_enemyManager.Initialize(_healthBar);
		_endLevelWindow.Initialize();

		_clickButtonManager.OnClicked += () => _enemyManager.DamageCurrentEnemy(10f);
		_endLevelWindow.OnRestartClicked += RestartLevel;
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

	public void RestartLevel()
	{
		var sceneLoader = GameObject.FindWithTag(SCENE_LOADER_TAG).GetComponent<SceneLoader>();
		sceneLoader.LoadGameplayScene();
	}
}