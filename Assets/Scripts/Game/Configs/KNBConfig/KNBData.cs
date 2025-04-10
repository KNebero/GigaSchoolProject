using System;
using Game.Configs.EnemyConfigs;

namespace Game.Configs.KNBConfig
{
	[Serializable]
	public struct KNBData
	{
		public DamageType DamageType;
		public EnemyType EnemyType;
		public float Coefficient;
	}
}