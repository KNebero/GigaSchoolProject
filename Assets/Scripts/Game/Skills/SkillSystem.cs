using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Game.Configs.EnemyConfigs;
using Game.Enemies;
using Global.SaveSystem.SavableObjects;
using UnityEditor.Experimental.GraphView;
using UnityEngine.iOS;

namespace Game.Skills
{
	public class SkillSystem
	{
		private SkillScope _scope;
		private SkillsConfig _skillsConfig;
		private Dictionary<SkillTrigger, List<Skill>> _skillsByTrigger;

		public SkillSystem(OpenedSkills openedSkills, SkillsConfig skillsConfig, EnemyManager enemyManager)
		{
			_scope = new SkillScope()
			{
				EnemyManager = enemyManager,
			};
			_skillsConfig = skillsConfig;
			_skillsByTrigger = new Dictionary<SkillTrigger, List<Skill>>();
			
			foreach (var skill in openedSkills.Skills)
			{
				RegisterSkill(skill);
			}
		}

		private void RegisterSkill(SkillWithLevel skill)
		{
			var skillData = _skillsConfig.GetSkillData(skill.Id, skill.Level);

			var skillType = Type.GetType($"Game.Skills.SkillVariants.{skill.Id}");
			
			if (skillType == null)
			{
				throw new Exception($"Skill with id {skill.Id} not found");
			}

			if (Activator.CreateInstance(skillType) is not Skill skillInstance)
			{
				throw new Exception($"Can not instantiate Skill with id {skill.Id}");
			}
			
			skillInstance.Initialize(_scope, skillData);
			skillInstance.OnSkillRegistered();
			
			if (!_skillsByTrigger.ContainsKey(skillData.Trigger))
			{
				_skillsByTrigger[skillData.Trigger] = new List<Skill>();
			}
			_skillsByTrigger[skillData.Trigger].Add(skillInstance);
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
	}
}