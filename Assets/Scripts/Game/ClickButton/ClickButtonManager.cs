using UnityEngine;
using UnityEngine.Events;

namespace Game.ClickButton
{
    public class ClickButtonManager : MonoBehaviour
    {
        [SerializeField] private global::Game.ClickButton.ClickButton _flySwatterButton;
        [SerializeField] private global::Game.ClickButton.ClickButton _knifeButton;
        [SerializeField] private global::Game.ClickButton.ClickButton _hammerButton;
        [SerializeField] private ClickButtonConfig _clickButtonConfig;

        public event UnityAction FlySwatterOnClicked;
        public event UnityAction KnifeOnClicked;
        public event UnityAction HammerOnClicked;

        public void Initialize()
        {
            _flySwatterButton.Initialize(_clickButtonConfig.FlySwatterSprite, _clickButtonConfig.FlySwatterColors);
            _flySwatterButton.SubscribeOnClick(() => FlySwatterOnClicked?.Invoke());
            
            _knifeButton.Initialize(_clickButtonConfig.KnifeSprite, _clickButtonConfig.KnifeColors);
            _knifeButton.SubscribeOnClick(() => KnifeOnClicked?.Invoke());
            
            _hammerButton.Initialize(_clickButtonConfig.HammerSprite, _clickButtonConfig.HammerColors);
            _hammerButton.SubscribeOnClick(() => HammerOnClicked?.Invoke());
        }
    }
}
