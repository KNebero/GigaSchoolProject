using System;
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
			
			sceneLoader.LoadMetaScene();
		}
	}
}