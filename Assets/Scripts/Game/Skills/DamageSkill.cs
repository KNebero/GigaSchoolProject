using System;
using Game.Configs.KNBConfig;
using Game.Enemies;
using Game.Skills.Data;
using UnityEngine.Events;

namespace Game.Skills
{
	public abstract class DamageSkill : Skill
	{
		protected EnemyManager _enemyManager;
		protected KNBConfig _knb;
		protected int _damage;
		protected DamageType _damageType;

		public static int CalculateDamageByFormula(int baseDamage, int level)
		{
			return (int)Math.Round(baseDamage * (10 - 20 / Math.Log(level + 4.666, 2)));
		}
		
		public override void Initialize(SkillScope skillScope, SkillData skillData, int level)
		{
			base.Initialize(skillScope, skillData, level);
			_enemyManager = skillScope.EnemyManager;
			_knb = skillScope.KNBConfig;

			_damage = skillData.CalculationType switch
			{
				SkillByLevelCalculationType.Formula => CalculateDamageByFormula(
					(int)skillData.GetSkillDataByLevel(0).Value, level),
				SkillByLevelCalculationType.Level => (int)skillData.GetSkillDataByLevel(level).Value,
				_ => (int)skillData.GetSkillDataByLevel(level).Value,
			};
		}

		public override void SkillProcess()
		{
			_enemyManager.DamageCurrentEnemy(_knb.CalculateDamage(_damageType, _enemyManager.GetCurrentEnemyType(),
				_damage));
		}
	}
}