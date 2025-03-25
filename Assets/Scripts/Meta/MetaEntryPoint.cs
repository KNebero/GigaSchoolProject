using System;
using SceneManagement.Locations;
using UnityEngine;
using UnityEngine.UI;

namespace SceneManagement
{
	public class MetaEntryPoint : EntryPoint
	{
		[SerializeField] private LocationManager _locationManager;

		public override void Run(SceneEnterParams enterParams)
		{
			_locationManager.Initialize(0, StartLevel);
		}

		private void StartLevel(int location, int level)
		{
			var sceneLoader = GameObject.FindWithTag(Tags.SceneLoader).GetComponent<SceneLoader>();
			sceneLoader.LoadGameplayScene(new GameEnterParams(location, level));
		}
	}
}