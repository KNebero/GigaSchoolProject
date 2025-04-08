using System;
using Game.ClickButton;
using Game.Configs.EnemyConfigs;
using Game.Configs.LevelConfigs;
using Game.Configs.SkillsConfigs;
using Game.EndLevelWindow;
using Game.Enemies;
using Game.HealthBar;
using Game.Skills;
using Game.Timer;
using Global.AudioSystem;
using Global.SaveSystem;
using Global.SaveSystem.SavableObjects;
using Meta.Locations;
using SceneManagement;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameEntryPoint : EntryPoint
{
	[SerializeField] private ClickButtonManager _clickButtonManager;
	[SerializeField] private EnemyManager _enemyManager;
	[SerializeField] private HealthBar _healthBar;
	[SerializeField] private Timer _timer;
	[SerializeField] private EndLevelWindow _endLevelWindow;
	[SerializeField] private Button _goToMetaButton;
	[SerializeField] private LevelsConfig _levelsConfig;
	[SerializeField] private SkillsConfig _skillsConfig;
	private GameEnterParams _gameEnterParams;
	private CommonObject _commonObject;
	private SkillSystem _skillSystem;
	private EndLevelSystem _endLevelSystem;

	public override void Run(SceneEnterParams enterParams)
	{
		_commonObject = GameObject.FindWithTag(Tags.CommonObject).GetComponent<CommonObject>();

		if (enterParams is not GameEnterParams gameEnterParams)
		{
			Debug.LogError("Expected GameEnterParams");
			return;
		}

		_gameEnterParams = gameEnterParams;

		_clickButtonManager.Initialize();
		_enemyManager.Initialize(_healthBar, _timer);
		_endLevelWindow.Initialize(_timer, GoToMeta, RestartLevel);
		
		var openedSkills = (OpenedSkills) _commonObject.SaveSystem.GetData(SavableObjectType.OpenedSkills);
		
		_skillSystem = new SkillSystem(openedSkills, _skillsConfig, _enemyManager);
		_endLevelSystem = new EndLevelSystem(_endLevelWindow, _commonObject.SaveSystem, _gameEnterParams, _levelsConfig);

		_clickButtonManager.FlySwatterOnClicked += () =>
		{
			_skillSystem.ProcessSkill("FlySwatterSkill");
			_skillSystem.InvokeTrigger(SkillTrigger.OnDamage);
		};
		_clickButtonManager.KnifeOnClicked += () =>
		{
			_skillSystem.ProcessSkill("KnifeSkill");
			_skillSystem.InvokeTrigger(SkillTrigger.OnDamage);
		};
		_clickButtonManager.HammerOnClicked += () =>
		{
			_skillSystem.ProcessSkill("HammerSkill");
			_skillSystem.InvokeTrigger(SkillTrigger.OnDamage);
		};
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
		if (location > maxLocationAndLevel.x || (location == maxLocationAndLevel.x && level > maxLocationAndLevel.y))
		{
			location = maxLocationAndLevel.x;
			level = maxLocationAndLevel.y;
		}
		var levelData = _levelsConfig.GetLevel(location, level);

		_enemyManager.StartLevel(levelData);
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