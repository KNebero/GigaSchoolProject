using System.Collections.Generic;
using Global.Translator;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Meta.Locations
{
	public class Location : MonoBehaviour
	{
		[SerializeField] private string _baseLocationName;
		[SerializeField] private TextMeshProUGUI _locationName;
		[SerializeField] private List<Pin> _pins;

		public void Initialize(ProgressState locationState, int currentLevel, UnityAction<int> startLevelCallBack)
		{
			_locationName.text = TranslationManager.Translate(_baseLocationName);
			for (int i = 0; i < _pins.Count; i++)
			{
				var level = i;
				var pinState = locationState switch
				{
					ProgressState.Passed => ProgressState.Passed,
					ProgressState.Closed => ProgressState.Closed,
					ProgressState.Current => level < currentLevel ? ProgressState.Passed :
						level == currentLevel ? ProgressState.Current : ProgressState.Closed,
					_ => ProgressState.Closed,
				};

				if (pinState == ProgressState.Closed)
				{
					_pins[i].Initialize(pinState, null);
				}
				else
				{
					_pins[i].Initialize(pinState, () => startLevelCallBack?.Invoke(level));
				}
			}
		}

		public void SetActive(bool active)
		{
			gameObject.SetActive(active);
		}
	}
}