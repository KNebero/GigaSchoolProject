using System;
using Global.SaveSystem;
using Global.SaveSystem.SavableObjects;
using Global.Translator;
using UnityEngine;
using YG;

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

			var openedSkills = (OpenedSkills)commonObject.SaveSystem.GetData(SavableObjectType.OpenedSkills);

			openedSkills.GetOrCreateSkillWithLevel("FlySwatterSkill");
			openedSkills.GetOrCreateSkillWithLevel("KnifeSkill");
			openedSkills.GetOrCreateSkillWithLevel("HammerSkill");

			//commonObject.SaveSystem.SaveData(SavableObjectType.OpenedSkills);

			TranslationManager.SetLanguage(YG2.lang);

			commonObject.SceneLoader.LoadMetaScene();
		}
	}
}