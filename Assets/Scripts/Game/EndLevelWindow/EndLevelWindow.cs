using System.Collections.Generic;
using Extensions;
using Global.Translator;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Game.EndLevelWindow
{
	public class EndLevelWindow : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI _winHeader;
		[SerializeField] private TextMeshProUGUI _loseHeader;

		[SerializeField] private Button _nextButton;
		[SerializeField] private Button _restartButton;

		[SerializeField] private Image _infoBox;
		[SerializeField] private TextMeshProUGUI _infoText;

		[SerializeField] private List<string> _losePhrases;

		[SerializeField] private Color _winInfoColor;
		[SerializeField] private Color _loseInfoColor;

		private Timer.Timer _timer;

		private event UnityAction _onRestartClicked;
		private event UnityAction _onNextClicked;

		public void Initialize(Timer.Timer timer, UnityAction onNextClicked, UnityAction onRestartClicked)
		{
			_timer = timer;
			_onNextClicked = onNextClicked;
			_onRestartClicked = onRestartClicked;
		}

		public void ShowLoseWindow(bool isBoss)
		{
			_infoBox.color = _loseInfoColor;
			_infoText.text = _losePhrases.Count == 0
				? ""
				: TranslationManager.Translate(_losePhrases[Random.Range(0, _losePhrases.Count)]);

			_winHeader.gameObject.SetActive(false);
			_loseHeader.gameObject.SetActive(true);

			_nextButton.SetActive(false);
			_restartButton.SetActive(true);
			
			_restartButton.SubscribeOnly(RestartLevel);

			gameObject.SetActive(true);
		}

		public void ShowWinWindow(bool isBoss, int coinsEarned)
		{
			_infoBox.color = _winInfoColor;
			_infoText.text = TranslationManager.Translate($"WinPhraseIf{(isBoss ? "" : "Not")}Boss")
				.Replace("%TimePassed%", _timer.TimePast.ToString("00.00"))
				.Replace("%CoinsEarned%", coinsEarned.ToString());

			_winHeader.gameObject.SetActive(true);
			_loseHeader.gameObject.SetActive(false);

			_nextButton.SetActive(true);
			_restartButton.SetActive(false);

			_nextButton.SubscribeOnly(NextLevel);

			gameObject.SetActive(true);
		}

		private void RestartLevel()
		{
			_onRestartClicked?.Invoke();
			gameObject.SetActive(false);
		}

		private void NextLevel()
		{
			_onNextClicked?.Invoke();
			gameObject.SetActive(false);
		}
	}
}