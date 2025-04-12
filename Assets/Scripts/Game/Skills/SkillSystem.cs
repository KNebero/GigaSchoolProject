using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Game.Configs.EnemyConfigs;
using Game.Configs.KNBConfig;
using Game.Configs.SkillsConfigs;
using Game.Enemies;
using Global.Formulas;
using Global.SaveSystem.SavableObjects;
using Unity.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine.iOS;

namespace Game.Skills
{
	public class SkillSystem
	{
		private SkillScope _scope;
		private SkillsConfig _skillsConfig;
		private Dictionary<SkillTrigger, List<Skill>> _skillsByTrigger;
		private Dictionary<string, Skill> _skillsByName;

		public SkillSystem(OpenedSkills openedSkills, SkillsConfig skillsConfig, EnemyManager enemyManager,
			KNBConfig knbConfig)
		{
			_scope = new SkillScope()
			{
				EnemyManager = enemyManager,
				KNBConfig = knbConfig,
				PlayerStats = new PlayerStats()
				{
					CritChancePercent = openedSkills.GetSkillWithLevel("CritChance") == null
						? 0
						: (int)SkillsFormulas.CalculateCritChance(
							skillsConfig.GetSkillDataByLevel("CritChance", 0).Value,
							openedSkills.GetSkillWithLevel("CritChance").Level),
					CritDamagePercent = openedSkills.GetSkillWithLevel("CritDamage") == null
						? 0
						: (int)SkillsFormulas.CalculateCritChance(
							skillsConfig.GetSkillDataByLevel("CritDamage", 0).Value,
							openedSkills.GetSkillWithLevel("CritDamage").Level),
				}
			};
			_skillsConfig = skillsConfig;
			_skillsByTrigger = new Dictionary<SkillTrigger, List<Skill>>();
			_skillsByName = new Dictionary<string, Skill>();

			foreach (var skill in openedSkills.Skills)
			{
				RegisterSkill(skill);
			}
		}

		private void RegisterSkill(SkillWithLevel skill)
		{
			if (_skillsConfig.GetSkillData(skill.Id).Trigger == SkillTrigger.None) return;

			var skillType = Type.GetType($"Game.Skills.SkillVariants.{skill.Id}");

			if (skillType == null)
			{
				throw new Exception($"Skill with id {skill.Id} not found");
			}

			if (Activator.CreateInstance(skillType) is not Skill skillInstance)
			{
				throw new Exception($"Can not instantiate Skill with id {skill.Id}");
			}

			var skillData = _skillsConfig.GetSkillData(skill.Id);

			skillInstance.Initialize(_scope, skillData, skill.Level);
			skillInstance.OnSkillRegistered();

			if (!_skillsByTrigger.ContainsKey(skillData.Trigger))
			{
				_skillsByTrigger[skillData.Trigger] = new List<Skill>();
			}

			_skillsByTrigger[skillData.Trigger].Add(skillInstance);

			_skillsByName[skill.Id] = skillInstance;
		}

		public void InvokeTrigger(SkillTrigger skillTrigger)
		{
			if (!_skillsByTrigger.ContainsKey(skillTrigger)) return;

			var skillsToActivate = _skillsByTrigger[skillTrigger];

			foreach (var skill in skillsToActivate)
			{
				skill.SkillProcess();
			}
		}

		public void ProcessSkill(string skillId)
		{
			_skillsByName[skillId].SkillProcess();
		}
	}
}