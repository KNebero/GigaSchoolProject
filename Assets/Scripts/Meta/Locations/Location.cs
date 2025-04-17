using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Meta.Locations
{
	public class Location : MonoBehaviour
	{
		[SerializeField] private List<Pin> _pins;

		public void Initialize(ProgressState locationState, int currentLevel, UnityAction<int> startLevelCallBack)
		{
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
					_pins[i].Initialize(level + 1, pinState, null);
				}
				else
				{
					_pins[i].Initialize(level + 1, pinState, () => startLevelCallBack?.Invoke(level));
				}
			}
		}

		public void SetActive(bool active)
		{
			gameObject.SetActive(active);
		}
	}
}