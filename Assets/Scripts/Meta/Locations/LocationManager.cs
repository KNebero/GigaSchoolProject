using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SceneManagement.Locations
{
	public class LocationManager : MonoBehaviour
	{
		[SerializeField] private Button _previousButton;
		[SerializeField] private Button _nextButton;
		
		[SerializeField] private List<Location> _locations;

		private int _currentLocation;

		public void Initialize(int currentLocation, UnityAction<Vector2Int> startLevelCallback)
		{
			_currentLocation = currentLocation;
			InitLocation(currentLocation, startLevelCallback);
			InitializeSwitchLocationButtons();
		}

		private void InitializeSwitchLocationButtons()
		{
			_previousButton.onClick.AddListener(ShowPreviousLocation);
			_nextButton.onClick.AddListener(ShowNextLocation);
		}

		private void ShowNextLocation()
		{
			_locations[_currentLocation].gameObject.SetActive(false);
			_currentLocation = (_currentLocation + 1) % _locations.Count;
			_locations[_currentLocation].gameObject.SetActive(true);
		}

		private void ShowPreviousLocation()
		{
			_locations[_currentLocation].gameObject.SetActive(false);
			_currentLocation = (_currentLocation - 1) % _locations.Count;
			_locations[_currentLocation].gameObject.SetActive(true);
		}

		private void InitLocation(int currentLocation, UnityAction<Vector2Int> startLevelCallback)
		{
			for (int i = 0; i < _locations.Count; i++)
			{
				var locationNumber = i;
				_locations[i].Initialize(level => startLevelCallback?.Invoke(new Vector2Int(locationNumber, level)));
				_locations[i].SetActive(locationNumber == currentLocation);
			}
		}
	}
}