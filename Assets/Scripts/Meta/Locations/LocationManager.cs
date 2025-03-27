using System.Collections.Generic;
using Global.SaveSystem.SavableObjects;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Meta.Locations
{
	public class LocationManager : MonoBehaviour
	{
		[SerializeField] private Button _previousButton;
		[SerializeField] private Button _nextButton;
		
		[SerializeField] private List<Location> _locations;

		private int _currentLocation;

		public void Initialize(Progress progress, UnityAction<int, int> startLevelCallback)
		{
			_currentLocation = progress.CurrentLocation;
			InitLocation(progress, startLevelCallback);
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

		private void InitLocation(Progress progress, UnityAction<int, int> startLevelCallback)
		{
			for (int i = 0; i < _locations.Count; i++)
			{
				var locationNumber = i;
				ProgressState locationState = progress.CurrentLocation > locationNumber
						? ProgressState.Passed
						: progress.CurrentLocation == locationNumber
						? ProgressState.Current
						: ProgressState.Closed;
				
				var currentLevel = progress.CurrentLevel;
				
				_locations[i].Initialize(locationState, currentLevel, level => startLevelCallback?.Invoke(locationNumber, level));
				_locations[i].SetActive(locationNumber == progress.CurrentLocation);
			}
		}
	}
}