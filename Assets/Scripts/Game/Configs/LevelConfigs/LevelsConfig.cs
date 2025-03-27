using System.Collections.Generic;
using Game.Configs.LevelConfigs;
using UnityEngine;

namespace Game.Configs.LevelConfigs
{
	[CreateAssetMenu(menuName = "Configs/LevelsConfig", fileName = "LevelsConfig")]
	public class LevelsConfig : ScriptableObject
	{
		public List<LevelData> Levels;

		public LevelData GetLevel(int location, int level)
		{
			foreach (var levelsData in Levels)
			{
				if (levelsData.Location != location || levelsData.LevelNumber != level) continue;
				return levelsData;
			}

			Debug.LogError($"Not found Level data for location {location} and level {level}");
			return default;
		}

		public int GetMaxLevelOnLocation(int location)
		{
			var maxLevel = -1;

			foreach (var levelData in Levels)
			{
				if (location == levelData.Location && levelData.LevelNumber > maxLevel)
					maxLevel = levelData.LevelNumber;
			}

			return maxLevel;
		}
	}
	
}