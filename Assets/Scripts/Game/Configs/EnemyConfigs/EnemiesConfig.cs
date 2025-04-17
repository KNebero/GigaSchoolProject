using System.Collections.Generic;
using Game.Enemies;
using UnityEngine;

namespace Game.Configs.EnemyConfigs
{
	[CreateAssetMenu(menuName = "Configs/EnemiesConfig", fileName = "EnemiesConfig")]
	public class EnemiesConfig : ScriptableObject
	{
		public Enemy EnemyPrefab;
		public List<EnemyData> Enemies;

		public EnemyData GetEnemy(string id)
		{
			// Создать словарь
			foreach (var enemyData in Enemies)
			{
				if (enemyData.Id == id)
				{
					return enemyData;
				}
			}

			Debug.LogError($"Couldn't find enemy with id = {id}");
			return null;
		}
	}
}