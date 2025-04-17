using Game.ClickButton;
using Game.Configs.KNBConfig;
using Game.Configs.LevelConfigs;
using Game.Configs.SkillsConfigs;
using Game.Enemies;
using Game.Skills;
using Global.AudioSystem;
using Global.SaveSystem;
using Global.SaveSystem.SavableObjects;
using SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
	public class GameEntryPoint : EntryPoint
	{
		[SerializeField] private ClickButtonManager _clickButtonManager;
		[SerializeField] private EnemyManager _enemyManager;
		[SerializeField] private HealthBar.HealthBar _healthBar;
		[SerializeField] private Timer.Timer _timer;
		[SerializeField] private EndLevelWindow.EndLevelWindow _endLevelWindow;
		[SerializeField] private Button _goToMetaButton;
		[SerializeField] private LevelsConfig _levelsConfig;
		[SerializeField] private SkillsConfig _skillsConfig;
		[SerializeField] private KNBConfig _knbConfig;
		private GameEnterParams _gameEnterParams;
		private CommonObject _commonObject;
		private SkillSystem _skillSystem;
		private EndLevelSystem _endLevelSystem;
		private GameScope _gameScope;

		public override void Run(SceneEnterParams enterParams)
		{
			_commonObject = GameObject.FindWithTag(Tags.CommonObject).GetComponent<CommonObject>();

			if (enterParams is not GameEnterParams gameEnterParams)
			{
				Debug.LogError("Expected GameEnterParams");
				return;
			}

			_gameEnterParams = gameEnterParams;

			_enemyManager.Initialize(_healthBar, _timer);
			var nextLevel = _levelsConfig.GetNextLevelOnLocation(gameEnterParams.Location, gameEnterParams.Level);
			if (nextLevel == -1)
			{
				if (gameEnterParams.Location == _levelsConfig.GetMaxLocation())
					_endLevelWindow.Initialize(_timer, GoToMeta, RestartLevel);
				else
					_endLevelWindow.Initialize(_timer, () =>
					{
						((Cash)_commonObject.SaveSystem.GetData(SavableObjectType.Cash)).CurrentLocation++;
						_commonObject.SaveSystem.SaveData(SavableObjectType.Cash);
						GoToMeta();
					}, RestartLevel);
			}
			else
			{
				_endLevelWindow.Initialize(_timer, NextLevel, RestartLevel);
			}


			_endLevelSystem =
				new EndLevelSystem(_endLevelWindow, _commonObject.SaveSystem, _gameEnterParams, _levelsConfig);

			_gameScope = new GameScope()
			{
				EndLevelSystem = _endLevelSystem,
				EnemyManager = _enemyManager,
				Timer = _timer,
			};

			var openedSkills = (OpenedSkills)_commonObject.SaveSystem.GetData(SavableObjectType.OpenedSkills);

			_skillSystem = new SkillSystem(openedSkills, _skillsConfig, _knbConfig, _gameScope);

			_clickButtonManager.Initialize(_skillSystem);

			_enemyManager.OnLevelPassed += _endLevelSystem.LevelPassed;

			_goToMetaButton.onClick.AddListener(GoToMeta);

			_commonObject.AudioManager.PlayClip(AudioGameNames.Background);
			StartLevel();
		}

		private void StartLevel()
		{
			var maxLocationAndLevel = _levelsConfig.GetMaxLocationAndLevel();
			var location = _gameEnterParams.Location;
			var level = _gameEnterParams.Level;
			if (location > maxLocationAndLevel.x ||
			    (location == maxLocationAndLevel.x && level > maxLocationAndLevel.y))
			{
				location = maxLocationAndLevel.x;
				level = maxLocationAndLevel.y;
			}

			var levelData = _levelsConfig.GetLevel(location, level);

			_enemyManager.StartLevel(levelData);
		}

		public void NextLevel()
		{
			_commonObject.SceneLoader.LoadGameplayScene(new GameEnterParams(_gameEnterParams.Location,
				_gameEnterParams.Level + 1));
		}

		public void RestartLevel()
		{
			_commonObject.SceneLoader.LoadGameplayScene(_gameEnterParams);
		}

		public void GoToMeta()
		{
			_commonObject.SceneLoader.LoadMetaScene();
		}
	}
}