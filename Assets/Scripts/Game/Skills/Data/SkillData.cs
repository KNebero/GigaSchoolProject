using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Serialization;

namespace Game.Skills.Data
{
	[Serializable]
	public struct SkillData
	{
		public string SkillId;
		public SkillByLevelCalculationType CalculationType;
		public SkillTrigger Trigger;
		public int MaxLevel;
		[FormerlySerializedAs("SkillLevel")] public List<SkillDataByLevel> SkillLevels;

		public SkillDataByLevel GetSkillDataByLevel(int level)
		{
			return SkillLevels.Find(x => x.Level == level);
		}

		public bool IsMaxLevel(int level)
		{
			return MaxLevel != -1 && level == MaxLevel;
		}
	}
}