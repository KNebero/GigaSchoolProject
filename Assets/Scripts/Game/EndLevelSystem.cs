using System;
using Game.Configs.LevelConfigs;
using Global.SaveSystem;
using Global.SaveSystem.SavableObjects;
using SceneManagement;

namespace Game
{
	public class EndLevelSystem
	{
		private readonly EndLevelWindow.EndLevelWindow _endLevelWindow;
		private readonly SaveSystem _saveSystem;
		private readonly GameEnterParams _gameEnterParams;
		private readonly LevelsConfig _levelsConfig;

		public int CoinReward;

		public EndLevelSystem(EndLevelWindow.EndLevelWindow endLevelWindow,
			SaveSystem saveSystem,
			GameEnterParams gameEnterParams,
			LevelsConfig levelsConfig)
		{
			_saveSystem = saveSystem;
			_endLevelWindow = endLevelWindow;
			_gameEnterParams = gameEnterParams;
			_levelsConfig = levelsConfig;

			CoinReward = _levelsConfig.GetLevel(_gameEnterParams.Location, _gameEnterParams.Level).Reward;
		}

		public void LevelPassed(bool isPassed, bool isBoss)
		{
			if (isPassed)
			{
				TrySaveProgress();
				_endLevelWindow.ShowWinWindow(isBoss, CoinReward);
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

			var maxLocationAndLevel = _levelsConfig.GetMaxLocationAndLevel();

			if (_gameEnterParams.Location != progress.CurrentLocation ||
			    _gameEnterParams.Level != progress.CurrentLevel)
			{
				wallet = (Wallet)_saveSystem.GetData(SavableObjectType.Wallet);
				wallet.Coins += CoinReward;
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
			CoinReward = (int)Math.Round(CoinReward * LevelData.FirstTimeMultiplier);
			wallet.Coins += CoinReward;
			
			_saveSystem.SaveData(SavableObjectType.Wallet);

			_saveSystem.SaveData(SavableObjectType.Progress);
		}
	}
}