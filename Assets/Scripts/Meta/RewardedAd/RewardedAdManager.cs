using System;
using System.Collections;
using System.Collections.Generic;
using Extensions;
using Global.Translator;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YG;

namespace Meta.RewardedAd
{
	public class RewardedAdManager : MonoBehaviour
	{
		[SerializeField] private ConfirmationWindow _confirmWindow;
		[SerializeField] private float _delay;
		
		private UnityAction _disableRewardButton;
		private UnityAction<UnityAction> _enableRewardButton;
		private UnityAction _rewardAction;

		public void Initialize(UnityAction<UnityAction> enableRewardButton, UnityAction disableRewardButton, UnityAction rewardAction)
		{
			_enableRewardButton = enableRewardButton;
			_disableRewardButton = disableRewardButton;
			_rewardAction = rewardAction;
			_confirmWindow.Initialize(TranslationManager.Translate("ShowAdConfirmationQuestion"), onConfirm:ShowAdvertisement);
			enableRewardButton?.Invoke(ShowConfirmWindow);
		}

		private void ShowConfirmWindow()
		{
			_confirmWindow.SetActive(true);
		}

		private void ShowAdvertisement()
		{
			YG2.RewardedAdvShow("MetaCoinsButton", () => _rewardAction?.Invoke());

			_disableRewardButton?.Invoke();
			StartCoroutine(ShowButtonAfterTime());
		}

		private IEnumerator ShowButtonAfterTime()
		{
			yield return new WaitForSeconds(_delay);
			_enableRewardButton?.Invoke(ShowConfirmWindow);
		}

		private void OnDestroy()
		{
			StopAllCoroutines();
			_disableRewardButton?.Invoke();
		}
	}
}