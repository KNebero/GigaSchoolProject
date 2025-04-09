using System;
using Game.Skills.Data;

namespace Game.Skills
{
	public abstract class CalculableSkill : Skill
	{
		protected virtual int CalculateValue(int baseDamage, int level)
		{
			return (int) Math.Round(baseDamage * (10 - 20 / Math.Log(level + 4.666, 2)));
		}
	}
}