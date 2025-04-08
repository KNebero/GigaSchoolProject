using System.Collections.Generic;

namespace Global.SaveSystem.SavableObjects
{
	public class OpenedSkills : ISavable
	{
		public List<SkillWithLevel> Skills = new();

		public SkillWithLevel GetSkillWithLevel(string skillId)
		{
			foreach (var skillWithLevel in Skills)
			{
				if (skillWithLevel.Id == skillId)
				{
					return skillWithLevel;
				}
			}

			return null;
		}
	}
}