using UnityEngine;

public class ClickButtonManager : MonoBehaviour
{
    [SerializeField] private ClickButton _clickButton;
    [SerializeField] private ClickButtonConfig _clickButtonConfig;

    public void Initialize()
    {
        _clickButton.Initialize(_clickButtonConfig.DefaultSprite, _clickButtonConfig.ButtonColors);
        _clickButton.SubscribeOnOnClick(ShowClick);
    }

    private void ShowClick()
    {
        Debug.Log("Clicked button " + _clickButton.name);
    }
}
