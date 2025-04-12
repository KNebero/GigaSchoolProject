using Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Meta.Shop
{
	public class SkillItem : ShopItem
	{
		[SerializeField] TextMeshProUGUI _label;
		[SerializeField] TextMeshProUGUI _description;
		[SerializeField] TextMeshProUGUI _level;
		[SerializeField] TextMeshProUGUI _cost;
		[SerializeField] Button _buyButton;

		public string SkillId;

		public void Initialize(UnityAction<string> onClick,
			string displayName,
			string description,
			int level,
			int cost,
			bool isEnough,
			bool isMaxLevel)
		{
			_buyButton.SubscribeOnly(() => onClick?.Invoke(SkillId));
			_label.text = displayName;
			_description.text = description;
			_level.text = level.ToString();
			if (isMaxLevel)
			{
				_cost.gameObject.SetActive(false);
				_buyButton.interactable = false;
				return;
			}
			_cost.text = cost.ToString();

			_cost.color = isEnough ? Color.green : Color.red;
			_buyButton.interactable = isEnough;
		}
	}
}