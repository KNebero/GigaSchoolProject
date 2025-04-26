using System.Collections.Generic;
using Extensions;
using Global.SaveSystem;
using Global.SaveSystem.SavableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Meta
{
	public class TutorialWindow : MonoBehaviour
	{
		[SerializeField] private Button _previousButton;
		[SerializeField] private Button _nextButton;
		[SerializeField] private List<GameObject> _tabs;
		[SerializeField] private Button _closeArea;
		[SerializeField] private Button _finish;

		private int _currentTab;
		private SaveSystem _saveSystem;
		private Cash _cash;

		public void Initialize(SaveSystem saveSystem)
		{
			_saveSystem = saveSystem;
			_cash = (Cash)_saveSystem.GetData(SavableObjectType.Cash);

			ActivateFirstTab();
			
			InitSwitchButtons();
			InitCloseButtons();
		}

		public void SetActive(bool active)
		{
			if (active)
			{
				ActivateFirstTab();

				_previousButton.SetActive(false);
				_nextButton.SetActive(true);
			}
			
			gameObject.SetActive(active);
		}

		private void InitCloseButtons()
		{
			_closeArea.SubscribeOnly(() => SetActive(false));
			_closeArea.interactable = _cash.TutorialCompleted;

			if (!_cash.TutorialCompleted)
			{
				_finish.SubscribeOnly(() =>
				{
					_cash.TutorialCompleted = true;
					_saveSystem.SaveData(SavableObjectType.Cash);
					SetActive(false);
				});
			}
		}

		private void InitSwitchButtons()
		{
			_previousButton.SubscribeOnly(ShowPreviousTab);
			_nextButton.SubscribeOnly(ShowNextTab);

			_previousButton.SetActive(false);
			_nextButton.SetActive(true);
		}

		private void ActivateFirstTab()
		{
			foreach (var tab in _tabs)
			{
				tab.SetActive(false);
			}

			_tabs[_currentTab = 0].SetActive(true);
		}

		private void ShowPreviousTab()
		{
			if (_currentTab <= 0) return;

			_currentTab--;

			_previousButton.SetActive(_currentTab > 0);
			_nextButton.SetActive(true);

			_tabs[_currentTab + 1].SetActive(false);
			_tabs[_currentTab].SetActive(true);
		}

		private void ShowNextTab()
		{
			if (_currentTab >= _tabs.Count - 1 || _cash.TutorialCompleted && _currentTab >= _tabs.Count - 2) return;

			_currentTab++;

			_previousButton.SetActive(true);
			_nextButton.SetActive(!(_currentTab >= _tabs.Count - 1 ||
			                        _cash.TutorialCompleted && _currentTab >= _tabs.Count - 2));

			_tabs[_currentTab - 1].SetActive(false);
			_tabs[_currentTab].SetActive(true);
		}
	}
}