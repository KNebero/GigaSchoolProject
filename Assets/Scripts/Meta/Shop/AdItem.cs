using System.Net.Mime;
using Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Meta.Shop
{
	public class AdItem : ShopItem
	{
		public int Reward;
		[SerializeField] private TextMeshProUGUI _rewardText;
		public Button Button;

		private void Awake()
		{
			_rewardText.text = Reward.ToString();
		}
	}
}