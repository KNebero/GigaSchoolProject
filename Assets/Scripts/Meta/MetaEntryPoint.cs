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

		private void StartLevel(Vector2Int locationLevel)
		{
			var sceneLoader = GameObject.FindWithTag(Tags.SceneLoader).GetComponent<SceneLoader>();
			sceneLoader.LoadGameplayScene();
		}
	}
}