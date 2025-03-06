using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private Slider _timerSlider;

    private float _maxTime;
    private float _currentTime;
    private bool _isPlaying;

    public event UnityAction OnTimerEnd;

    public void Initialize(float maxTime)
    {
        _maxTime = maxTime;
        _currentTime = maxTime;
        _timerText.text = _currentTime.ToString("00:00");
        _timerSlider.maxValue = _maxTime;
        _timerSlider.value = _maxTime;
        _timerSlider.minValue = 0;
    }

    public void Play()
    {
        _isPlaying = true;
    }

    public void Pause()
    {
        _isPlaying = false;
    }

    public void Resume()
    {
        _isPlaying = true;
    }

    public void Stop()
    {
        _isPlaying = false;
        OnTimerEnd = null;
    }

    public void FixedUpdate()
    {
        if (!_isPlaying) return;
        
        var deltaTime = Time.fixedDeltaTime;

        if (deltaTime > _currentTime)
        {
            OnTimerEnd?.Invoke();
            Stop();
            return;
        }
        
        _currentTime -= deltaTime;
        _timerText.text = _currentTime.ToString("00:00");
        _timerSlider.value = _currentTime;
    }
}