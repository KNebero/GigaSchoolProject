using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ClickButtonController : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Button _button;

    public void Initialize()
    {
        // Иницциализация палитры кнопки
        // Визуальное изменение кнопеи при клике
    }

    public void SubscribeOnOnClick(UnityAction action)
    {
        _button.onClick.AddListener(action);
    }

    public void UnsubscribeOnClick(UnityAction action)
    {
        _button.onClick.RemoveListener(action);
    }
}
