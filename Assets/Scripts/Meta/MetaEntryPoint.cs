using System;
using Global.AudioSystem;
using Global.SaveSystem;
using Global.SaveSystem.SavableObjects;
using Meta.Locations;
using SceneManagement;
using UnityEngine;

namespace Meta
{
	public class MetaEntryPoint : EntryPoint
	{
		[SerializeField] private LocationManager _locationManager;
		
		private CommonObject _commonObject;

		public override void Run(SceneEnterParams enterParams)
		{
			_commonObject = GameObject.FindWithTag(Tags.CommonObject).GetComponent<CommonObject>();
			var progress = (Progress) _commonObject.SaveSystem.GetData(SavableObjectType.Progress);
			_locationManager.Initialize(progress, StartLevel);
			_commonObject.AudioManager.PlayClip(AudioMetaNames.Background);
		}

		private void StartLevel(int location, int level)
		{
			_commonObject.SceneLoader.LoadGameplayScene(new GameEnterParams(location, level));
		}
	}
}