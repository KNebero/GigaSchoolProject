using System;
using UnityEngine;
using UnityEngine.Events;

public class EnemyManager : MonoBehaviour
{
	[SerializeField] private Transform _enemyContainer;
	[SerializeField] private EnemiesConfig _enemiesConfig;
	
	private EnemyData _currentEnemyData;
	private Enemy _currentEnemy;
	private HealthBar _healthBar;

	public void Initialize(HealthBar healthBar)
	{
		_healthBar = healthBar;
		SpawnEnemy();
	}

	public void SpawnEnemy()
	{
		_currentEnemyData = _enemiesConfig.Enemies[0];
		_currentEnemy = Instantiate(_enemiesConfig.enemyPrefab, _enemyContainer);
		_currentEnemy.Initialize(_currentEnemyData);
		
		InitHPBar();
	}

	private void InitHPBar()
	{
		_healthBar.Show();
		_healthBar.SetMaxValue(_currentEnemyData.Health);
		_currentEnemy.OnDamaged += _healthBar.DecreaseValue;
		_currentEnemy.OnDead += _healthBar.Hide;
	}

	public void DamageCurrentEnemy(float damage)
	{
		_currentEnemy.DoDamage(damage);
		Debug.Log("Damaged. Current health is " + _currentEnemy.GetHealth());
	}
}