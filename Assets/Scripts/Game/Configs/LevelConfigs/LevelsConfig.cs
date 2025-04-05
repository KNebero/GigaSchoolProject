using System.Collections.Generic;
using Extensions;
using Game.Configs.LevelConfigs;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Configs.LevelConfigs
{
	[CreateAssetMenu(menuName = "Configs/LevelsConfig", fileName = "LevelsConfig")]
	public class LevelsConfig : ScriptableObject
	{
		[SerializeField] private List<LevelData> _levels;

		private Dictionary<int, Dictionary<int, LevelData>> _levelsMap;

		public LevelData GetLevel(int location, int level)
		{
			if (_levelsMap.IsNullOrEmpty()) FillLevelMap();

			return _levelsMap[location][level];
		}

		public int GetMaxLocation()
		{
			if (_levelsMap.IsNullOrEmpty()) FillLevelMap();
			var maxLocation = -1;

			foreach (var location in _levelsMap.Keys)
			{
				if (location > maxLocation)
					maxLocation = location;
			}

			return maxLocation;
		}

		public int GetMaxLevelOnLocation(int location)
		{
			if (_levelsMap.IsNullOrEmpty()) FillLevelMap();
			var maxLevel = -1;

			foreach (var levelNumber in _levelsMap[location].Keys)
			{
				if (levelNumber > maxLevel)
					maxLevel = levelNumber;
			}

			return maxLevel;
		}

		public Vector2Int GetMaxLocationAndLevel()
		{
			var maxLocation = GetMaxLocation();
			var maxLevel = GetMaxLevelOnLocation(maxLocation);

			return new Vector2Int(maxLocation, maxLevel);
		}

		public void FillLevelMap()
		{
			_levelsMap = new Dictionary<int, Dictionary<int, LevelData>>();

			foreach (var levelData in _levels)
			{
				var locationMap = _levelsMap.GetOrCreate(levelData.Location);
				locationMap[levelData.LevelNumber] = levelData;
			}
		}
	}
}