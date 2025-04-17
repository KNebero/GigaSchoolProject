using System;
using Game.Configs.EnemyConfigs;
using Game.Configs.SkillsConfigs;
using Global.AudioSystem;
using Global.SaveSystem;
using Global.SaveSystem.SavableObjects;
using Meta.Locations;
using Meta.Shop;
using SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace Meta
{
	public class MetaEntryPoint : EntryPoint
	{
		[SerializeField] private LocationManager _locationManager;
		[SerializeField] private ShopWindow _shopWindow;
		[SerializeField] private Button _shopButton;
		[SerializeField] private SkillsConfig _skillsConfig;

		private CommonObject _commonObject;


		public override void Run(SceneEnterParams enterParams)
		{
			_commonObject = GameObject.FindWithTag(Tags.CommonObject).GetComponent<CommonObject>();
			_locationManager.Initialize(_commonObject.SaveSystem, StartLevel);
			_shopWindow.Initialize(_commonObject.SaveSystem, _skillsConfig);
			_shopButton.onClick.AddListener(() => _shopWindow.gameObject.SetActive(true));
			_commonObject.AudioManager.PlayClip(AudioMetaNames.Background);
		}

		private void StartLevel(int location, int level)
		{
			_commonObject.SceneLoader.LoadGameplayScene(new GameEnterParams(location, level));
		}
	}
}