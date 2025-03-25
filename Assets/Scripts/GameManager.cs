using System;
using Game.ClickButton;
using Game.Configs.LevelConfigs;
using Game.EndLevelWindow;
using Game.Enemies;
using Game.HealthBar;
using Game.Timer;
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
	[SerializeField] private LevelsConfig _levelsConfig;
	private GameEnterParams _gameEnterParams;

	private const string SCENE_LOADER_TAG = "SceneLoader";

	public override void Run(SceneEnterParams enterParams)
	{
		if (enterParams is not GameEnterParams gameEnterParams)
		{
			Debug.LogError("Expected GameEnterParams");
			return;
		}
		
		_gameEnterParams = gameEnterParams;

		_clickButtonManager.Initialize();
		_enemyManager.Initialize(_healthBar, _timer);
		_endLevelWindow.Initialize();

		_clickButtonManager.OnClicked += () => _enemyManager.DamageCurrentEnemy(1f);
		_endLevelWindow.OnRestartClicked += RestartLevel;
		_enemyManager.OnLevelPassed += LevelPassed;

		StartLevel();
	}

	private void LevelPassed(bool isPassed)
	{
		if (isPassed) _endLevelWindow.ShowWinWindow();
		else _endLevelWindow.ShowLooseWindow();
	}
	
	private void StartLevel()
	{
		var levelData = _levelsConfig.GetLevel(_gameEnterParams.Location, _gameEnterParams.Level);

		_enemyManager.StartLevel(levelData);
		
	}

	public void RestartLevel()
	{
		var sceneLoader = GameObject.FindWithTag(SCENE_LOADER_TAG).GetComponent<SceneLoader>();
		sceneLoader.LoadGameplayScene(_gameEnterParams);
	}
}