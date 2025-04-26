using System;
using System.Collections.Generic;
using Extensions;
using Game.Configs.LevelConfigs;
using Global.SaveSystem;
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

		private SaveSystem _saveSystem;
		private Progress _progress;
		private Cash _cash;
		private int _currentLocation;

		public void Initialize(SaveSystem saveSystem, UnityAction<int, int> startLevelCallback)
		{
			_saveSystem = saveSystem;
			_progress = (Progress)saveSystem.GetData(SavableObjectType.Progress);
			_cash = (Cash)saveSystem.GetData(SavableObjectType.Cash);

			if (_cash.CurrentLocation >= _locations.Count)
			{
				_cash.CurrentLocation = _locations.Count - 1;
				_saveSystem.SaveData(SavableObjectType.Cash);
			}

			_currentLocation = _cash.CurrentLocation;

			InitLocation(_progress, startLevelCallback);
			InitializeSwitchLocationButtons();
		}

		private void InitializeSwitchLocationButtons()
		{
			_previousButton.onClick.AddListener(ShowPreviousLocation);
			_nextButton.onClick.AddListener(ShowNextLocation);

			_previousButton.SetActive(_currentLocation > 0);
			_nextButton.SetActive(_currentLocation < _locations.Count - 1 &&
			                      _currentLocation < _progress.CurrentLocation);
		}

		private void ShowNextLocation()
		{
			if (_currentLocation >= _locations.Count - 1 || _currentLocation >= _progress.CurrentLocation) return;

			++_currentLocation;

			_previousButton.SetActive(true);
			_nextButton.SetActive(_currentLocation < _locations.Count - 1 &&
			                      _currentLocation < _progress.CurrentLocation);

			_locations[_currentLocation - 1].gameObject.SetActive(false);
			_locations[_currentLocation].gameObject.SetActive(true);

			_cash.CurrentLocation = _currentLocation;
			_saveSystem.SaveData(SavableObjectType.Cash);
		}

		private void ShowPreviousLocation()
		{
			if (_currentLocation <= 0) return;

			--_currentLocation;

			_previousButton.SetActive(_currentLocation > 0);
			_nextButton.SetActive(true);

			_locations[_currentLocation + 1].gameObject.SetActive(false);
			_locations[_currentLocation].gameObject.SetActive(true);

			_cash.CurrentLocation = _currentLocation;
			_saveSystem.SaveData(SavableObjectType.Cash);
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

				_locations[i].Initialize(locationState, currentLevel,
					level => startLevelCallback?.Invoke(locationNumber, level));
				_locations[i].SetActive(locationNumber == _currentLocation);
			}
		}
	}
}