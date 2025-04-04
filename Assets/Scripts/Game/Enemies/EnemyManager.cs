using Game.Configs.EnemyConfigs;
using Game.Configs.LevelConfigs;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Enemies
{
	public class EnemyManager : MonoBehaviour
	{
		[SerializeField] private Transform _enemyContainer;
		[SerializeField] private EnemiesConfig _enemiesConfig;

		private Timer.Timer _timer;
		private Enemy _currentEnemy;
		private HealthBar.HealthBar _healthBar;
		private LevelData _levelData;
		private int _currentEnemyIndex;

		public event UnityAction<bool> OnLevelPassed;

		public void Initialize(HealthBar.HealthBar healthBar, Timer.Timer timer)
		{
			_healthBar = healthBar;
			_timer = timer;
		}

		public void StartLevel(LevelData levelData)
		{
			_levelData = levelData;
			_currentEnemyIndex = -1;
			
			if (!_currentEnemy)
			{
				_currentEnemy = Instantiate(_enemiesConfig.EnemyPrefab, _enemyContainer);
				_currentEnemy.OnDead += SpawnEnemy;
				_currentEnemy.OnDamaged += _healthBar.DecreaseValue;
			}
			
			SpawnEnemy();
		}

		private void SpawnEnemy()
		{
			_timer.Stop();
			++_currentEnemyIndex;
			if (_currentEnemyIndex >= _levelData.Enemies.Count)
			{
				OnLevelPassed?.Invoke(true);
				_timer.Stop();
				return;
			}
			var currentEnemy = _levelData.Enemies[_currentEnemyIndex];

			_timer.SetActive(currentEnemy.IsBoss);
			if (currentEnemy.IsBoss)
			{
				_timer.Initialize(currentEnemy.BossTime);
				_timer.SetActive(true);
				_timer.Play();
				_timer.OnTimerEnd += () => OnLevelPassed?.Invoke(false);
			}
			
			var currentEnemyViewData = _enemiesConfig.GetEnemy(currentEnemy.Id);

			InitHpBar(currentEnemy.Hp);

			_currentEnemy.Initialize(currentEnemyViewData.Sprite, currentEnemy.Hp);
		}

		private void InitHpBar(float health)
		{
			_healthBar.Show();
			_healthBar.SetMaxValue(health);
		}

		public void DamageCurrentEnemy(float damage)
		{
			_currentEnemy.DoDamage(damage);
			Debug.Log($"Damaged. Current health is {_currentEnemy.GetHealth()}");
		}
	}
}