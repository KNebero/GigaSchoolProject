using Game.Enemies;
using Game.Skills.Data;
using UnityEngine.Scripting;

namespace Game.Skills.SkillVariants
{
	[Preserve]
	public class KnifeSkill : CalculableSkill
	{
		private EnemyManager _enemyManager;
		private SkillData _skillData;
		private int _damage;

		public override void Initialize(SkillScope scope, SkillData skillData, int level)
		{
			_enemyManager = scope.EnemyManager;
			_skillData = skillData;
			_damage = CalculateValue((int)skillData.GetSkillDataByLevel(0).Value, level);
		}

		public override void SkillProcess()
		{
			_enemyManager.DamageCurrentEnemy(_damage);
		}
	}
}