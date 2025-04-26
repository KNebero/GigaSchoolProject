using System;
using System.Collections.Generic;
using NUnit.Framework.Internal.Filters;

namespace Game.Configs.LevelConfigs
{
	[Serializable]
	public struct LevelData
	{
		public const float FirstTimeMultiplier = 1.5f;

		public string Id;
		public int Location;
		public int LevelNumber;
		public List<EnemySpawnData> Enemies;
		public int Reward;
	}
}