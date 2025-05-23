using Game.Configs.KNBConfig;
using Game.Enemies;
using Game.Skills.Data;
using UnityEngine.Scripting;

namespace Game.Skills.SkillVariants
{
	[Preserve]
	public class KnifeSkill : DamageSkill, ICritable
	{
		public override void Initialize(SkillScope scope, SkillData skillData, int level)
		{
			base.Initialize(scope, skillData, level);
			_damageType = DamageType.Knife;
		}
	}
}