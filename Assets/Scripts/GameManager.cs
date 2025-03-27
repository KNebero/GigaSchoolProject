using System;
using Game.ClickButton;
using Game.Configs.LevelConfigs;
using Game.EndLevelWindow;
using Game.Enemies;
using Game.HealthBar;
using Game.Timer;
using Global.SaveSystem;
using Global.SaveSystem.SavableObjects;
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
	private SaveSystem _saveSystem;

	private const string SCENE_LOADER_TAG = "SceneLoader";

	public override void Run(SceneEnterParams enterParams)
	{
		_saveSystem = FindFirstObjectByType<SaveSystem>();

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
		if (isPassed)
		{
			TrySaveProgress();
			_endLevelWindow.ShowWinWindow();
		}
		else
		{
			_endLevelWindow.ShowLooseWindow();
		}
	}

	private void TrySaveProgress()
	{
		var progress = (Progress)_saveSystem.GetData(SavableObjectType.Progress);

		if (_gameEnterParams.Location != progress.CurrentLocation ||
		    _gameEnterParams.Level != progress.CurrentLevel) return;

		var maxLevel = _levelsConfig.GetMaxLevelOnLocation(progress.CurrentLocation);

		if (progress.CurrentLevel >= maxLevel)
		{
			progress.CurrentLevel = 0;
			++progress.CurrentLocation;
		}
		else
		{
			++progress.CurrentLevel;
		}
		
		_saveSystem.SaveData(SavableObjectType.Progress);
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