using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SceneManagement.Locations
{
	public class Location : MonoBehaviour
	{
		[SerializeField] private List<Pin> _pins;

		public void Initialize(UnityAction<int> startLevelCallBack)
		{
			var currentLevel = 1;
			for (int i = 0; i < _pins.Count; i++)
			{
				var level = i;
				var pinType = currentLevel == level
					? PinType.Current
					: level < currentLevel
						? PinType.Passed
						: PinType.Closed;
				
				_pins[i].Initialize(level + 1, pinType, () => startLevelCallBack?.Invoke(level));
			}
		}

		public void SetActive(bool active) {
			gameObject.SetActive(active);
		}
	}
}