using System.Collections.Generic;
using Game.Skills.Data;
using UnityEngine;

namespace Game.Configs.SkillsConfigs
{
	[CreateAssetMenu(menuName="Configs/SkillsConfig", fileName="SkillsConfig")]
	public class SkillsConfig : ScriptableObject
	{
		public List<SkillData> Skills;

		private Dictionary<string, Dictionary<int, SkillDataByLevel>> _skillDataByLevelMap;
		private Dictionary<string, SkillData> _skillDataMap;

		public SkillData GetSkillData(string skillId)
		{
			if (_skillDataMap == null || _skillDataMap.Count == 0)
			{
				FillSkillDataMap();
			}
			
			return _skillDataMap[skillId];
		}

		private void FillSkillDataMap()
		{
			_skillDataMap = new Dictionary<string, SkillData>();
			foreach (var skillData in Skills)
			{
				_skillDataMap[skillData.SkillId] = skillData;
			}
		}

		public SkillDataByLevel GetSkillDataByLevel(string skillId, int level)
		{
			if (_skillDataByLevelMap == null || _skillDataByLevelMap.Count == 0)
			{
				FillSkillDataByLevelMap();
			}
			
			return _skillDataByLevelMap[skillId][level];
		}

		private void FillSkillDataByLevelMap()
		{
			_skillDataByLevelMap = new Dictionary<string, Dictionary<int, SkillDataByLevel>>();
			foreach (var skillData in Skills)
			{
				if (!_skillDataByLevelMap.ContainsKey(skillData.SkillId))
				{
					_skillDataByLevelMap[skillData.SkillId] = new Dictionary<int, SkillDataByLevel>();
				}
				foreach (var skillDataByLevel in skillData.SkillLevels)
				{
					_skillDataByLevelMap[skillData.SkillId][skillDataByLevel.Level] = skillDataByLevel;
				}
			}
		}
	}
}