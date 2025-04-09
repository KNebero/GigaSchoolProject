using System;
using System.Collections.Generic;
using Game.Configs.LevelConfigs;
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
			_currentLocation = Math.Min(progress.CurrentLocation, _locations.Count - 1);
			InitLocation(progress, startLevelCallback);
			InitializeSwitchLocationButtons();
		}

		private void InitializeSwitchLocationButtons()
		{
			_previousButton.onClick.AddListener(ShowPreviousLocation);
			_nextButton.onClick.AddListener(ShowNextLocation);
			
			_previousButton.interactable = _currentLocation > 0;
			_nextButton.interactable = _currentLocation < _locations.Count - 1;
		}

		private void ShowNextLocation()
		{
			if (_currentLocation >= _locations.Count - 1) return;

			++_currentLocation;

			_previousButton.interactable = true;
			_nextButton.interactable = _currentLocation < _locations.Count - 1;
			
			_locations[_currentLocation - 1].gameObject.SetActive(false);
			_locations[_currentLocation].gameObject.SetActive(true);
		}

		private void ShowPreviousLocation()
		{
			if (_currentLocation <= 0) return;

			--_currentLocation;

			_previousButton.interactable = _currentLocation > 0;
			_nextButton.interactable = true;
			
			_locations[_currentLocation + 1].gameObject.SetActive(false);
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
				_locations[i].SetActive(locationNumber == _currentLocation);
			}
		}
	}
}