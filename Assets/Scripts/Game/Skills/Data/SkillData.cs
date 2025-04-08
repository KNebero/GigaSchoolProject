using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Serialization;

namespace Game.Skills.Data
{
	[Serializable]
	public struct SkillData
	{
		public SkillByLevelCalculationType CalculationType;
		public string SkillId;
		[FormerlySerializedAs("SkillLevel")] public List<SkillDataByLevel> SkillLevels;

		public SkillDataByLevel GetSkillDataByLevel(int level)
		{
			return SkillLevels.Find(x => x.Level == level);
		}

		public bool IsMaxLevel(int level)
		{
			return SkillLevels.Max(x => x.Level) == level;
		}
	}
}