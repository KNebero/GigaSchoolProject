using System;
using System.Collections.Generic;

namespace Game.Configs.LevelConfigs
{
	[Serializable]
	public struct LevelData
	{
		public const float FirstTimeMultiplier = 1.5f;

		public int Location;
		public int LevelNumber;
		public List<EnemySpawnData> Enemies;
		public int Reward;
	}
}