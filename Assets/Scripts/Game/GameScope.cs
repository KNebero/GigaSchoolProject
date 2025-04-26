using Game.Enemies;
using TMPro;
using UnityEngine.UI;

namespace Game
{
	public class GameScope
	{
		public EndLevelSystem EndLevelSystem;
		public EnemyManager EnemyManager;
		public Timer.Timer Timer;
		public HealthBar.HealthBar HealthBar;
		public Image EnemyTypeIcon;
		public TextMeshProUGUI CurrentEnemyNumber;
		public TextMeshProUGUI TotalEnemiesAmount;
		public TextMeshProUGUI BossName;
	}
}