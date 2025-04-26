using System;
using System.Collections.Generic;
using Extensions;
using Game.Configs.KNBConfig;
using UnityEngine;

namespace Game.Configs.EnemyConfigs
{
	[CreateAssetMenu(menuName = "Configs/EnemyTypeIconsConfig", fileName = "EnemyTypeIconsConfig")]
	public class EnemyTypeIconsConfig : ScriptableObject
	{
		[Serializable]
		public struct TypeIcon
		{
			public EnemyType EnemyType;
			public Sprite Sprite;
		}

		public List<TypeIcon> TypeIcons;
		private Dictionary<EnemyType, Sprite> _typeIconsMap;

		private void FillMap()
		{
			_typeIconsMap = new Dictionary<EnemyType, Sprite>();
			foreach (var typeIcon in TypeIcons)
			{
				_typeIconsMap[typeIcon.EnemyType] = typeIcon.Sprite;
			}
		}
		
		public Sprite GetSprite(EnemyType enemyType)
		{
			if (_typeIconsMap.IsNullOrEmpty()) FillMap();
			
			return _typeIconsMap.ContainsKey(enemyType) ? _typeIconsMap[enemyType] : null;
		}
	}
}