using System;
using System.Runtime.InteropServices;
using Game.Configs.LevelConfigs;
using Game.EndLevelWindow;
using Global.SaveSystem;
using Global.SaveSystem.SavableObjects;
using SceneManagement;

public class EndLevelSystem
{
	private readonly EndLevelWindow _endLevelWindow;
	private readonly SaveSystem _saveSystem;
	private readonly GameEnterParams _gameEnterParams;
	private readonly LevelsConfig _levelsConfig;

	public EndLevelSystem(EndLevelWindow endLevelWindow,
		SaveSystem saveSystem,
		GameEnterParams gameEnterParams,
		LevelsConfig levelsConfig)
	{
		_saveSystem = saveSystem;
		_endLevelWindow = endLevelWindow;
		_gameEnterParams = gameEnterParams;
		_levelsConfig = levelsConfig;
	}

	public void LevelPassed(bool isPassed, bool isBoss)
	{
		if (isPassed)
		{
			TrySaveProgress();
			_endLevelWindow.ShowWinWindow(isBoss);
		}
		else
		{
			_endLevelWindow.ShowLoseWindow(isBoss);
		}
	}

	private void TrySaveProgress()
	{
		var progress = (Progress)_saveSystem.GetData(SavableObjectType.Progress);
		Wallet wallet;
		LevelData level;

		var maxLocationAndLevel = _levelsConfig.GetMaxLocationAndLevel();

		if (_gameEnterParams.Location != progress.CurrentLocation ||
		    _gameEnterParams.Level != progress.CurrentLevel)
		{
			wallet = (Wallet)_saveSystem.GetData(SavableObjectType.Wallet);
			level = _levelsConfig.GetLevel(_gameEnterParams.Location, _gameEnterParams.Level);
			wallet.Coins += level.Reward;
			_saveSystem.SaveData(SavableObjectType.Wallet);
			
			return;
		}
		
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
		
		wallet = (Wallet)_saveSystem.GetData(SavableObjectType.Wallet);
		level = _levelsConfig.GetLevel(_gameEnterParams.Location, _gameEnterParams.Level);
		wallet.Coins += (int) Math.Floor(level.Reward * LevelData.FirstTimeMultiplier);
		_saveSystem.SaveData(SavableObjectType.Wallet);

		_saveSystem.SaveData(SavableObjectType.Progress);
	}
}