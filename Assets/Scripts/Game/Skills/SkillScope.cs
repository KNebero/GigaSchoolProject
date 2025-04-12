using Game.Configs.KNBConfig;
using Game.Enemies;
using Global.SaveSystem.SavableObjects;

namespace Game.Skills
{
	public class SkillScope
	{
		public EnemyManager EnemyManager;
		public KNBConfig KNBConfig;
		public PlayerStats PlayerStats;
	}

	public class PlayerStats
	{
		public int CritChancePercent;
		public int CritDamagePercent;
	}
}