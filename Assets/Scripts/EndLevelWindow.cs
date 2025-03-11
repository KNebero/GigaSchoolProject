using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class EndLevelWindow : MonoBehaviour
{
	[FormerlySerializedAs("_looseLevelWindow")] [SerializeField] private GameObject _loseLevelWindow;
	[SerializeField] private GameObject _winLevelWindow;

	[FormerlySerializedAs("_looseRestartButton")] [SerializeField] private Button _loseRestartButton;
	[SerializeField] private Button _winRestartButton;
	
	[SerializeField] private TextMeshProUGUI _loseInfoText;
	[SerializeField] private TextMeshProUGUI _winInfoText;

	[SerializeField] private List<string> _losePhrases;
	
	[SerializeField] private Timer _timer;
	
	public event UnityAction OnRestartClicked;

	public void Initialize()
	{
		_loseRestartButton.onClick.AddListener(RestartLevel);
		_winRestartButton.onClick.AddListener(RestartLevel);
	}

	public void ShowLooseWindow()
	{
		_loseLevelWindow.SetActive(true);
		_loseInfoText.text = _losePhrases.Count == 0 ? "" : _losePhrases[Random.Range(0, _losePhrases.Count)];
		_winLevelWindow.SetActive(false);

		gameObject.SetActive(true);
	}

	public void ShowWinWindow()
	{
		_winLevelWindow.SetActive(true);
		_winInfoText.text = "Вы победили противника за " + _timer.TimePast.ToString("00:00");
		_loseLevelWindow.SetActive(false);

		gameObject.SetActive(true);
	}

	private void RestartLevel()
	{
		OnRestartClicked?.Invoke();
		gameObject.SetActive(false);
	}
}