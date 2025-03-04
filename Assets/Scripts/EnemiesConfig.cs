using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Configs/EnemiesConfig", fileName="EnemiesConfig")]
public class EnemiesConfig : ScriptableObject
{
	public Enemy enemyPrefab;
	public List<EnemyData> Enemies;
}