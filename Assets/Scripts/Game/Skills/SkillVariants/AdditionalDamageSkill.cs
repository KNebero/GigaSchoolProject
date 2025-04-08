using Game.Enemies;
using Game.Skills.Data;
using UnityEngine.Scripting;

namespace Game.Skills.SkillVariants
{
	[Preserve]
	public class AdditionalDamageSkill : Skill
	{
		private EnemyManager _enemyManager;
		private SkillDataByLevel _skillDataByLevel;

		public override void Initialize(SkillScope scope, SkillData skillData, int level)
		{
			_enemyManager = scope.EnemyManager;
			_skillDataByLevel = skillData.GetSkillDataByLevel(level);
		}

		public override void SkillProcess()
		{
			_enemyManager.DamageCurrentEnemy(_skillDataByLevel.Value);
		}
	}
}