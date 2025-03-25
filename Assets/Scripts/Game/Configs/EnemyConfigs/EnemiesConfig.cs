using System.Collections.Generic;
using Game.Enemies;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.COnfigs.EnemyConfigs
{
	[CreateAssetMenu(menuName="Configs/EnemiesConfig", fileName="EnemiesConfig")]
	public class EnemiesConfig : ScriptableObject
	{
		public Enemy EnemyPrefab;
		public List<EnemyViewData> Enemies;
		
		public EnemyViewData GetEnemy(string id)
		{
			// Создать словарь
			foreach (var enemyData in Enemies)
			{
				if (enemyData.Id == id)
				{
					return enemyData;
				}
			}
			
			Debug.Log($"Couldn't find enemy with id = {id}");
			return null;
		}
	}
}