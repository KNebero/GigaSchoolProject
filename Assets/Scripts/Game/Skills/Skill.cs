using Game.Skills.Data;

namespace Game.Skills
{
	public abstract class Skill
	{
		public virtual void Initialize(SkillScope skillScope, SkillData skillData, int level) {}

		public virtual void OnSkillRegistered() {}
		public virtual void SkillProcess() {}
	}
}