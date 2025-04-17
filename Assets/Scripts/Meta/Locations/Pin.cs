using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Meta.Locations
{
	public class Pin : MonoBehaviour
	{
		[SerializeField] private Button _button;
		[SerializeField] private Image _image;
		[SerializeField] private TextMeshProUGUI _text;

		[SerializeField] private Color _currentLevel;
		[SerializeField] private Color _passedLevel;
		[SerializeField] private Color _closedLevel;

		public void Initialize(int levelNumber, ProgressState progressState, UnityAction clickCallback)
		{
			_text.text = $"Ур. {levelNumber}";

			_image.color = progressState switch
			{
				ProgressState.Closed => _closedLevel,
				ProgressState.Passed => _passedLevel,
				ProgressState.Current => _currentLevel,
				_ => _closedLevel
			};

			_button.onClick.AddListener(() => clickCallback?.Invoke());
		}
	}
}