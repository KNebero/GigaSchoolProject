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

	public void LevelPassed(bool isPassed)
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

		var maxLocationAndLevel = _levelsConfig.GetMaxLocationAndLevel();

		if (progress.CurrentLocation > maxLocationAndLevel.x ||
		    (progress.CurrentLocation == maxLocationAndLevel.x
		     && progress.CurrentLevel > maxLocationAndLevel.y))
		{
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

		_saveSystem.SaveData(SavableObjectType.Progress);
	}
}