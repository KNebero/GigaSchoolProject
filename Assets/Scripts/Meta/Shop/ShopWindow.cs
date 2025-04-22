using System;
using System.Collections.Generic;
using Extensions;
using Game.Configs.EnemyConfigs;
using Game.Configs.SkillsConfigs;
using Game.Skills;
using Global.Formulas;
using Global.SaveSystem;
using Global.SaveSystem.SavableObjects;
using Global.Translator;
using Meta.RewardedAd;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace Meta.Shop
{
	public class ShopWindow : MonoBehaviour
	{
		[SerializeField] private Button _closeButton;
		[SerializeField] private TextMeshProUGUI _walletView;
		[SerializeField] private RewardedAdManager _rewardedAdManager;

		[Serializable]
		public struct ButtonTab
		{
			public Button button;
			public GameObject tab;
		}

		[SerializeField] private List<ButtonTab> _tabs;
		[SerializeField] private Button _firstTabButton;

		[SerializeField] private List<SkillItem> _items;
		[SerializeField] private AdItem _adItem;

		private Dictionary<string, SkillItem> _itemsMap = new Dictionary<string, SkillItem>();

		private SkillsConfig _skillsConfig;
		private SaveSystem _saveSystem;
		private Wallet _wallet;
		private OpenedSkills _openedSkills;

		public void Initialize(SaveSystem saveSystem, SkillsConfig skillsConfig)
		{
			_skillsConfig = skillsConfig;
			_saveSystem = saveSystem;
			_openedSkills = (OpenedSkills)saveSystem.GetData(SavableObjectType.OpenedSkills);
			_wallet = (Wallet)saveSystem.GetData(SavableObjectType.Wallet);

			_walletView.SetText(_wallet.Coins.ToString());
			_wallet.OnChanged += (wallet) => _walletView.SetText(wallet.Coins.ToString());
			_closeButton.onClick.AddListener(() => gameObject.SetActive(false));

			_rewardedAdManager.Initialize(callback => EnableRewardedButton(callback), DisableRewardedButton,
				GetAdvReward);

			InitializeItemsMap();

			InitializeTabsSwitching();
			_firstTabButton.onClick.Invoke();
			
			ShowShopItems();
		}

		private void InitializeTabsSwitching()
		{
			foreach (var tab in _tabs)
			{
				tab.button.onClick.AddListener(() => ShowTab(tab.tab));
			}
		}

		private void ShowTab(GameObject showTab)
		{
			foreach (var tab in _tabs)
			{
				tab.button.interactable = tab.tab != showTab;
				tab.tab.SetActive(tab.tab == showTab);
			}
		}

		private void InitializeItemsMap()
		{
			foreach (var skillItem in _items)
			{
				_itemsMap[skillItem.SkillId] = skillItem;
			}
		}

		private void ShowShopItems()
		{
			foreach (var skillData in _skillsConfig.Skills)
			{
				if (!_itemsMap.ContainsKey(skillData.SkillId)) continue;

				var skillWithLevel = _openedSkills.GetSkillWithLevel(skillData.SkillId);
				var newLevel = (skillWithLevel?.Level ?? -1) + 1;

				var cost = (skillData.CalculationType switch
				{
					SkillByLevelCalculationType.Formula => SkillsFormulas.CalculateCost(
						skillData.GetSkillDataByLevel(0).Cost, newLevel),
					SkillByLevelCalculationType.Level => newLevel > skillData.MaxLevel
						? 0
						: skillData.GetSkillDataByLevel(newLevel).Cost,
					_ => 0,
				});

				_itemsMap[skillData.SkillId].Initialize((skillId) => SkillUpgrade(skillId, cost),
					TranslationManager.Translate($"{skillData.SkillId}DisplayName"),
					TranslationManager.Translate($"{skillData.SkillId}Description"),
					newLevel, // Текущий уровень (в индекс с 0) + 1 == Новый уровень
					cost,
					_wallet.Coins >= cost,
					skillData.IsMaxLevel(newLevel - 1));
			}
		}

		private void SkillUpgrade(string skillId, int cost)
		{
			_wallet.Coins -= cost;
			if (_openedSkills.GetSkillWithLevel(skillId) == null)
			{
				_openedSkills.GetOrCreateSkillWithLevel(skillId);
			}
			else
			{
				_openedSkills.GetSkillWithLevel(skillId).Level++;
			}

			_saveSystem.SaveData(SavableObjectType.OpenedSkills);
			_saveSystem.SaveData(SavableObjectType.Wallet);

			ShowShopItems();
		}

		public void EnableRewardedButton(UnityAction callback)
		{
			_adItem.Button.interactable = true;
			_adItem.Button.SubscribeOnly(() => callback?.Invoke());
		}

		public void DisableRewardedButton()
		{
			_adItem.Button.interactable = false;
			_adItem.Button.onClick.RemoveAllListeners();
		}

		private void GetAdvReward()
		{
			_wallet.Coins += _adItem.Reward;
			_saveSystem.SaveData(SavableObjectType.Wallet);
		}
	}
}