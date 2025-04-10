using System;
using Game.Configs.KNBConfig;
using UnityEngine;

namespace Game.Configs.EnemyConfigs
{
	[Serializable]
	public class EnemyData
	{
		public string Id;
		public Sprite Sprite;
		public EnemyType EnemyType;
	}
}