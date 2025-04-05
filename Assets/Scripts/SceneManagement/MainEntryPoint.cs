using System;
using Global.SaveSystem;
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
			
			commonObject.SceneLoader.LoadMetaScene();
		}
	}
}