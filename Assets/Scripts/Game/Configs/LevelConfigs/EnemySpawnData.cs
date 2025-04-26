using System;
using UnityEngine.Serialization;

namespace Game.Configs.LevelConfigs
{
	[Serializable]
	public struct EnemySpawnData
	{
		public string Id;
		public float Hp;
		public bool IsBoss;
		public float BossTime;
		[FormerlySerializedAs("BossName")] public string BossNameTranslationKey;
	}
}