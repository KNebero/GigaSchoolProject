using Game.Configs.KNBConfig;
using Game.Enemies;
using Game.Skills.Data;
using UnityEngine.Scripting;

namespace Game.Skills.SkillVariants
{
	[Preserve]
	public class HammerSkill : DamageSkill
	{
		public override void Initialize(SkillScope scope, SkillData skillData, int level)
		{
			base.Initialize(scope, skillData, level);
			_damageType = DamageType.Hammer;
		}
	}
}