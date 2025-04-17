using System;
using Game.Configs.KNBConfig;
using Game.Enemies;
using Game.Skills.Data;
using Global.Formulas;
using UnityEngine.Events;

namespace Game.Skills
{
	public abstract class DamageSkill : Skill
	{
		protected EnemyManager _enemyManager;
		protected KNBConfig _knb;
		protected float _damage;
		protected DamageType _damageType;
		protected PlayerStats _playerStats;

		public override void Initialize(SkillScope skillScope, SkillData skillData, int level)
		{
			base.Initialize(skillScope, skillData, level);
			_enemyManager = skillScope.GameScope.EnemyManager;
			_knb = skillScope.KNBConfig;
			_playerStats = skillScope.PlayerStats;

			_damage = skillData.CalculationType switch
			{
				SkillByLevelCalculationType.Formula => SkillsFormulas.CalculateDamage(
					skillData.GetSkillDataByLevel(0).Value, level),
				SkillByLevelCalculationType.Level => (int)skillData.GetSkillDataByLevel(level).Value,
				_ => skillData.GetSkillDataByLevel(level).Value,
			};
		}

		public override void SkillProcess()
		{
			float multiplier = 1f;
			if (this is ICritable && new Random().Next(0, 100) <= _playerStats.CritChancePercent)
			{
				multiplier += 1f * _playerStats.CritDamagePercent / 100;
			}

			_enemyManager.DamageCurrentEnemy(_knb.CalculateDamage(_damageType, _enemyManager.GetCurrentEnemyType(),
				_damage * multiplier));
		}
	}
}