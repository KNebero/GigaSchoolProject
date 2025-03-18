using UnityEngine;
using UnityEngine.Events;

namespace Game.Enemies
{
	public class EnemyManager : MonoBehaviour
	{
		[SerializeField] private Transform _enemyContainer;
		[SerializeField] private EnemiesConfig _enemiesConfig;

		private EnemyData _currentEnemyData;
		private Enemy _currentEnemy;
		private HealthBar.HealthBar _healthBar;

		public event UnityAction OnLevelPassed;

		public void Initialize(HealthBar.HealthBar healthBar)
		{
			_healthBar = healthBar;
		}

		public void SpawnEnemy()
		{
			_currentEnemyData = _enemiesConfig.Enemies[0];
		
			InitHPBar();
		
			if (_currentEnemy == null)
			{
				_currentEnemy = Instantiate(_enemiesConfig.enemyPrefab, _enemyContainer);
				_currentEnemy.OnDead += () => OnLevelPassed?.Invoke();
				_currentEnemy.OnDamaged += _healthBar.DecreaseValue;
				_currentEnemy.OnDead += _healthBar.Hide;
			}

			_currentEnemy.Initialize(_currentEnemyData);
		}

		private void InitHPBar()
		{
			_healthBar.Show();
			_healthBar.SetMaxValue(_currentEnemyData.Health);
		}

		public void DamageCurrentEnemy(float damage)
		{
			_currentEnemy.DoDamage(damage);
			Debug.Log("Damaged. Current health is " + _currentEnemy.GetHealth());
		}
	}
}