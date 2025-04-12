using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Game.EndLevelWindow
{
	public class EndLevelWindow : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI _header;

		[SerializeField] private Button _nextOrRestartButton;

		[SerializeField] private Image _infoBox;
		[SerializeField] private TextMeshProUGUI _infoText;

		[SerializeField] private TextMeshProUGUI _nextOrRestartButtonText;

		[SerializeField] private List<string> _losePhrases;

		[SerializeField] private string _winHeader;
		[SerializeField] private Color _winHeaderColor;
		[SerializeField] private string _loseHeader;
		[SerializeField] private Color _loseHeaderColor;

		[SerializeField] private Color _winInfoColor;
		[SerializeField] private Color _loseInfoColor;

		[SerializeField] private string _nextButtonText;
		[SerializeField] private Color _nextButtonColor;
		[SerializeField] private string _restartButtonText;
		[SerializeField] private Color _restartButtonColor;

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
			_infoText.text = _losePhrases.Count == 0 ? "" : _losePhrases[Random.Range(0, _losePhrases.Count)];

			_header.text = _loseHeader;
			_header.color = _loseHeaderColor;
			_nextOrRestartButton.image.color = _restartButtonColor;
			_nextOrRestartButtonText.text = _restartButtonText;

			_nextOrRestartButton.onClick.AddListener(RestartLevel);

			gameObject.SetActive(true);
		}

		public void ShowWinWindow(bool isBoss, int coinsEarned)
		{
			_infoBox.color = _winInfoColor;
			if (isBoss)
			{
				_infoText.text = "Вы победили противника за " + _timer.TimePast.ToString("00:00") + "\n\n";
			}
			else
			{
				_infoText.text = "";
			}

			_infoText.text += $"Вы заработали {coinsEarned} монет";

			_header.text = _winHeader;
			_header.color = _winHeaderColor;
			_nextOrRestartButton.image.color = _nextButtonColor;
			_nextOrRestartButtonText.text = _nextButtonText;

			_nextOrRestartButton.onClick.AddListener(NextLevel);

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