using Game.Configs.EnemyConfigs;
using Game.Configs.KNBConfig;
using Game.Configs.LevelConfigs;
using Global.Formulas;
using Global.Translator;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Enemies
{
	public class EnemyManager : MonoBehaviour
	{
		[SerializeField] private Transform _enemyContainer;
		[SerializeField] private EnemiesConfig _enemiesConfig;
		[SerializeField] private EnemyTypeIconsConfig _enemyTypeIcons;

		private Timer.Timer _timer;
		private HealthBar.HealthBar _healthBar;
		private Image _enemyTypeIcon;
		private TextMeshProUGUI _currentEnemyNumber;
		private TextMeshProUGUI _totalEnemiesAmount;
		private TextMeshProUGUI _bossName;
		private Enemy _currentEnemy;
		private LevelData _levelData;
		private int _currentEnemyIndex;
		private EnemyType _currentEnemyType;

		public event UnityAction<bool, bool> OnLevelPassed;

		public void Initialize(GameScope gameScope)
		{
			_healthBar = gameScope.HealthBar;
			_timer = gameScope.Timer;
			_enemyTypeIcon = gameScope.EnemyTypeIcon;
			_bossName = gameScope.BossName;
			_currentEnemyNumber = gameScope.CurrentEnemyNumber;
			_totalEnemiesAmount = gameScope.TotalEnemiesAmount;
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
			
			_totalEnemiesAmount.text = _levelData.Enemies.Count.ToString();

			SpawnEnemy();
		}

		private void SpawnEnemy()
		{
			_timer.Stop();
			++_currentEnemyIndex;
			if (_currentEnemyIndex >= _levelData.Enemies.Count)
			{
				OnLevelPassed?.Invoke(true, _levelData.Enemies[^1].IsBoss);
				_timer.Stop();
				return;
			}

			var currentEnemy = _levelData.Enemies[_currentEnemyIndex];

			_currentEnemyNumber.text = (_currentEnemyIndex + 1).ToString();
			
			_timer.SetActive(currentEnemy.IsBoss);
			_bossName.gameObject.SetActive(currentEnemy.IsBoss);
			
			if (currentEnemy.IsBoss)
			{
				_bossName.text = TranslationManager.Translate(currentEnemy.BossNameTranslationKey);
				_timer.Initialize(currentEnemy.BossTime);
				_timer.SetActive(true);
				_timer.Play();
				_timer.OnTimerEnd += () => OnLevelPassed?.Invoke(false, true);
			}

			var currentEnemyData = _enemiesConfig.GetEnemy(currentEnemy.Id);
			_currentEnemyType = currentEnemyData.EnemyType;
			var health = EnemyFormulas.CalculateHealth(currentEnemyData.BaseHealth, _levelData.Location,
				_levelData.LevelNumber);
			
			_enemyTypeIcon.gameObject.SetActive(currentEnemyData.EnemyType != EnemyType.None);
			if (_enemyTypeIcon.gameObject.activeSelf)
			{
				_enemyTypeIcon.sprite = _enemyTypeIcons.GetSprite(currentEnemyData.EnemyType);
			}

			InitHpBar(health);

			_currentEnemy.Initialize(currentEnemyData.Sprite, health);
		}

		private void InitHpBar(float health)
		{
			_healthBar.Show();
			_healthBar.SetMaxValue(health);
		}

		public void DamageCurrentEnemy(float damage)
		{
			_currentEnemy.DoDamage(damage);
		}

		public EnemyType GetCurrentEnemyType()
		{
			return _currentEnemyType;
		}
	}
}