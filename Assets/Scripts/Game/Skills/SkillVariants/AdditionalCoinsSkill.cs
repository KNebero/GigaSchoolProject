using Game.Skills.Data;
using Global.Formulas;
using UnityEngine;
using UnityEngine.Scripting;

namespace Game.Skills.SkillVariants
{
	[Preserve]
	public class AdditionalCoinsSkill : Skill
	{
		private float _coinsMultiplier;
		private EndLevelSystem _endLevelSystem;

		public override void Initialize(SkillScope scope, SkillData skillData, int level)
		{
			base.Initialize(scope, skillData, level);
			_endLevelSystem = scope.GameScope.EndLevelSystem;

			_coinsMultiplier = _skillData.CalculationType switch
			{
				SkillByLevelCalculationType.Formula => 1f +
				                                       SkillsFormulas.CalculateCoinsMultiplier(
					                                       skillData.GetSkillDataByLevel(0).Value, level) / 100,
				SkillByLevelCalculationType.Level => 1f + skillData.GetSkillDataByLevel(level).Value / 100,
				_ => 1
			};
		}

		public override void OnSkillRegistered()
		{
			_endLevelSystem.CoinReward = (int)Mathf.Round(_coinsMultiplier * _endLevelSystem.CoinReward);
		}
	}
}