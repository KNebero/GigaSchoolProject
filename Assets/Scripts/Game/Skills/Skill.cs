using Game.Skills.Data;

namespace Game.Skills
{
	public abstract class Skill
	{
		protected SkillData _skillData;

		public virtual void Initialize(SkillScope skillScope, SkillData skillData, int level)
		{
			_skillData = skillData;
		}

		public virtual void OnSkillRegistered() {}
		public virtual void SkillProcess() {}
	}
}