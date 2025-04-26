using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Timer
{
	public class Timer : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI _timerText;
		[SerializeField] private Slider _timerSlider;

		private float _currentTime;
		public bool IsPlaying { get; private set; }

		public float MaxTime { get; private set; }

		public float CurrentTime
		{
			get => _currentTime;
			set => _currentTime = Mathf.Clamp(value, 0f, MaxTime);
		}

		public float TimePast => MaxTime - _currentTime;

		public event UnityAction OnTimerEnd;

		public void Initialize(float maxTime)
		{
			MaxTime = maxTime;
			_currentTime = maxTime;
			_timerText.text = _currentTime.ToString("00:00");
			_timerSlider.maxValue = MaxTime;
			_timerSlider.value = MaxTime;
			_timerSlider.minValue = 0;
		}

		public void Play()
		{
			IsPlaying = true;
		}

		public void Pause()
		{
			IsPlaying = false;
		}

		public void Stop()
		{
			IsPlaying = false;
			OnTimerEnd = null;
		}

		public void FixedUpdate()
		{
			if (!IsPlaying) return;

			var deltaTime = Time.fixedDeltaTime;

			if (deltaTime > _currentTime)
			{
				OnTimerEnd?.Invoke();
				Stop();
				return;
			}

			_currentTime -= deltaTime;
			_timerText.text = _currentTime.ToString("00.00");
			_timerSlider.value = _currentTime;
		}

		public void SetActive(bool active)
		{
			gameObject.SetActive(active);
		}
	}
}