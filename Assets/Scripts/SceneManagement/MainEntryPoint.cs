using System;
using Global.SaveSystem;
using Global.SaveSystem.SavableObjects;
using UnityEngine;

namespace SceneManagement
{
	public class MainEntryPoint : MonoBehaviour
	{
		public void Awake()
		{
			if (GameObject.FindGameObjectWithTag(Tags.CommonObject)) return;
			
			var commonObjectPrefab = Resources.Load<CommonObject>("CommonObject");
			var commonObject = Instantiate(commonObjectPrefab);
			DontDestroyOnLoad(commonObject);

			commonObject.AudioManager.LoadOnce();
			commonObject.SceneLoader.Initialize(commonObject.AudioManager);
			commonObject.SaveSystem = new SaveSystem();
			
			var openedSkills = (OpenedSkills) commonObject.SaveSystem.GetData(SavableObjectType.OpenedSkills);
			if (openedSkills.GetOrCreateSkillWithLevel("FlySwatterSkill") == null)
			{
				openedSkills.Skills.Add(new SkillWithLevel()
				{
					Id = "FlySwatterSkill",
					Level = 0,
				});
			}
			if (openedSkills.GetOrCreateSkillWithLevel("KnifeSkill") == null)
			{
				openedSkills.Skills.Add(new SkillWithLevel()
				{
					Id = "KnifeSkill",
					Level = 0,
				});
			}
			if (openedSkills.GetOrCreateSkillWithLevel("HammerSkill") == null)
			{
				openedSkills.Skills.Add(new SkillWithLevel()
				{
					Id = "HammerSkill",
					Level = 0,
				});
			}
			commonObject.SaveSystem.SaveData(SavableObjectType.OpenedSkills);
			
			commonObject.SceneLoader.LoadMetaScene();
		}
	}
}