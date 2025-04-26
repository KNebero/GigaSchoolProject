using System;
using Extensions;
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
using YG;

namespace Meta
{
	public class MetaEntryPoint : EntryPoint
	{
		[SerializeField] private LocationManager _locationManager;
		[SerializeField] private ShopWindow _shopWindow;
		[SerializeField] private Button _shopButton;
		[SerializeField] private TutorialWindow _tutorialWindow;
		[SerializeField] private Button _helpButton;
		[SerializeField] private SkillsConfig _skillsConfig;

		private CommonObject _commonObject;

		public override void Run(SceneEnterParams enterParams)
		{
			_commonObject = GameObject.FindWithTag(Tags.CommonObject).GetComponent<CommonObject>();
			_locationManager.Initialize(_commonObject.SaveSystem, StartLevel);
			_shopWindow.Initialize(_commonObject.SaveSystem, _skillsConfig);
			_shopWindow.gameObject.SetActive(false);
			_shopButton.onClick.AddListener(() =>
			{
				YG2.InterstitialAdvShow();
				_shopWindow.gameObject.SetActive(true);
			});
			_tutorialWindow.Initialize(_commonObject.SaveSystem);
			_tutorialWindow.gameObject.SetActive(
				!((Cash)_commonObject.SaveSystem.GetData(SavableObjectType.Cash)).TutorialCompleted
			);
			_helpButton.SubscribeOnly(() => _tutorialWindow.SetActive(true));
			_commonObject.AudioManager.PlayClip(AudioMetaNames.Background);
		}

		private void StartLevel(int location, int level)
		{
			_commonObject.SceneLoader.LoadGameplayScene(new GameEnterParams(location, level));
		}
	}
}