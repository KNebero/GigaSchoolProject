using UnityEngine;
using UnityEngine.UI;

namespace Game.ClickButton
{
    [CreateAssetMenu(menuName = "Configs/ClickButtonConfig", fileName = "ClickButtonConfig")]
    public class ClickButtonConfig : ScriptableObject
    {
        public Sprite FlySwatterSprite;
        public ColorBlock FlySwatterColors;
        
        public Sprite KnifeSprite;
        public ColorBlock KnifeColors;
        
        public Sprite HammerSprite;
        public ColorBlock HammerColors;
    }
}
