using System;
using System.Collections.Generic;
using Game.Configs.EnemyConfigs;
using Global.SaveSystem;
using Global.SaveSystem.SavableObjects;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace Meta.Shop
{
	public class ShopWindow : MonoBehaviour
	{
		[SerializeField] private Button _closeButton;
		
		[Serializable]
		public struct ButtonTab
		{
			public Button button;
			public GameObject tab;
		}
		
		[SerializeField] private List<ButtonTab> _tabs;

		[SerializeField] private List<SkillItem> _items;

		private Dictionary<string, SkillItem> _itemsMap = new Dictionary<string, SkillItem>();

		private SkillsConfig _skillsConfig;
		private SaveSystem _saveSystem;
		private Wallet _wallet;
		private OpenedSkills _skills;

		public void Initialize(SaveSystem saveSystem, SkillsConfig skillsConfig)
		{
			_skillsConfig = skillsConfig;
			_saveSystem = saveSystem;
			_skills = (OpenedSkills)saveSystem.GetData(SavableObjectType.OpenedSkills);
			_wallet = (Wallet)saveSystem.GetData(SavableObjectType.Wallet);
			
			_closeButton.onClick.AddListener(() => gameObject.SetActive(false));
			
			InitializeItemsMap();
			
			InitializeTabsSwitching();
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
				var skillWithLevel = _skills.GetSkillWithLevel(skillData.SkillId);
				var skillDataBylevel = skillData.GetSkillDataByLevel(skillWithLevel.Level);

				if (!_itemsMap.ContainsKey(skillData.SkillId)) return;

				_itemsMap[skillData.SkillId].Initialize((skillId) => SkillUpgrade(skillId, skillDataBylevel.Cost),
					skillData.SkillId,
					"",
					skillWithLevel.Level,
					skillDataBylevel.Cost,
					_wallet.Coins >= skillDataBylevel.Cost,
					skillData.IsMaxLevel(skillWithLevel.Level));
			}
		}

		private void SkillUpgrade(string skillId, int cost)
		{
			var skillWithLevel = _skills.GetSkillWithLevel(skillId);
			skillWithLevel.Level++;
			_wallet.Coins -= cost;
			
			_saveSystem.SaveData(SavableObjectType.OpenedSkills);
			_saveSystem.SaveData(SavableObjectType.Wallet);

			ShowShopItems();
		}
	}
}