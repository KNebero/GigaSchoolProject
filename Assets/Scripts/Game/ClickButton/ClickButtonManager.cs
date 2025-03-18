using UnityEngine;
using UnityEngine.Events;

namespace Game.ClickButton
{
    public class ClickButtonManager : MonoBehaviour
    {
        [SerializeField] private global::Game.ClickButton.ClickButton _clickButton;
        [SerializeField] private ClickButtonConfig _clickButtonConfig;

        public event UnityAction OnClicked;

        public void Initialize()
        {
            _clickButton.Initialize(_clickButtonConfig.DefaultSprite, _clickButtonConfig.ButtonColors);
            _clickButton.SubscribeOnClick(() => OnClicked?.Invoke());
        }
    }
}
