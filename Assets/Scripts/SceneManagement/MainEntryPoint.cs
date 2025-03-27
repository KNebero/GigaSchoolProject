using System;
using Global.SaveSystem;
using UnityEngine;

namespace SceneManagement
{
	public class MainEntryPoint : MonoBehaviour
	{
		public void Awake()
		{
			if (GameObject.FindGameObjectWithTag(Tags.SceneLoader)) return;
			
			var sceneLoaderPrefab = Resources.Load<SceneLoader>("SceneLoader");
			var sceneLoader = Instantiate(sceneLoaderPrefab);
			DontDestroyOnLoad(sceneLoader);
			
			var saveSystem = new GameObject().AddComponent<SaveSystem>();
			saveSystem.Initialize();
			DontDestroyOnLoad(saveSystem);
			
			sceneLoader.LoadMetaScene();
		}
	}
}