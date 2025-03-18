using System;
using UnityEngine;
using UnityEngine.UI;

namespace SceneManagement
{
	public class MetaEntryPoint : EntryPoint
	{
		[SerializeField] private Button _startLevelButton;
		
		public override void Run(SceneEnterParams enterParams)
		{
			_startLevelButton.onClick.AddListener(StartLevel);
		}

		private void StartLevel()
		{
			var sceneLoader = GameObject.FindWithTag(SceneTags.LoadScene).GetComponent<SceneLoader>();
			sceneLoader.LoadGameplayScene();
		}
	}
}