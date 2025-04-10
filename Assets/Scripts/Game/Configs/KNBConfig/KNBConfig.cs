using System.Collections.Generic;
using Extensions;
using Game.Enemies;
using UnityEngine;

namespace Game.Configs.KNBConfig
{
	[CreateAssetMenu(menuName = "Configs/KNBConfig", fileName = "KNBConfig")]
	public class KNBConfig : ScriptableObject
	{
		[SerializeField] private float DefaultCoefficient;
		[SerializeField] private List<KNBData> _data;

		private Dictionary<DamageType, Dictionary<EnemyType, float>> _coefficientMap;

		public float CalculateDamage(DamageType damageType, EnemyType enemyType, float damage)
		{
			if (_coefficientMap.IsNullOrEmpty()) FillCoefficientMap();

			if (!_coefficientMap.ContainsKey(damageType) || !_coefficientMap[damageType].ContainsKey(enemyType))
			{
				return damage * DefaultCoefficient;
			}

			return damage * _coefficientMap[damageType][enemyType];
		}

		private void FillCoefficientMap()
		{
			_coefficientMap = new Dictionary<DamageType, Dictionary<EnemyType, float>>();

			foreach (var knbData in _data)
			{
				if (!_coefficientMap.ContainsKey(knbData.DamageType))
				{
					_coefficientMap[knbData.DamageType] = new Dictionary<EnemyType, float>();
				}

				_coefficientMap[knbData.DamageType][knbData.EnemyType] = knbData.Coefficient;
			}
		}
	}
}