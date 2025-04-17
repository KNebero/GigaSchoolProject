using System;
using Game.Enemies;
using Game.Skills.Data;
using Global.Formulas;
using UnityEngine.Scripting;

namespace Game.Skills.SkillVariants
{
	[Preserve]
	public class TimeIncreasingSkill : Skill
	{
		private EnemyManager _enemyManager;
		private Timer.Timer _timer;
		private int _chance;
		private int _time;

		public override void Initialize(SkillScope scope, SkillData skillData, int level)
		{
			base.Initialize(scope, skillData, level);
			_enemyManager = scope.GameScope.EnemyManager;
			
			_time = _skillData.CalculationType switch
			{
				SkillByLevelCalculationType.Formula => SkillsFormulas.CalculateTimerIncrease(
					skillData.GetSkillDataByLevel(0).Value, level),
				SkillByLevelCalculationType.Level => (int)skillData.GetSkillDataByLevel(level).Value,
				_ => 0
			};

			_chance = _skillData.CalculationType switch
			{
				SkillByLevelCalculationType.Formula => SkillsFormulas.CalculateTimerIncreaseChance(
					skillData.GetSkillDataByLevel(0).TriggerValue, level),
				SkillByLevelCalculationType.Level => (int)skillData.GetSkillDataByLevel(level).TriggerValue,
				_ => 0
			};
		}

		public override void SkillProcess()
		{
			if (!_timer.IsPlaying) return;
			
			if (new Random().Next(0, 100) <= _chance)
			{
				_timer.CurrentTime += _time;
			}
		}
	}
}