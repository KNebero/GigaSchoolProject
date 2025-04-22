using Game.Skills;
using UnityEngine;
using UnityEngine.Events;

namespace Game.ClickButton
{
	public class ClickButtonManager : MonoBehaviour
	{
		[SerializeField] private global::Game.ClickButton.ClickButton _flySwatterButton;
		[SerializeField] private global::Game.ClickButton.ClickButton _knifeButton;
		[SerializeField] private global::Game.ClickButton.ClickButton _hammerButton;

		private SkillSystem _skillSystem;

		public void Initialize(SkillSystem skillSystem)
		{
			_skillSystem = skillSystem;

			_flySwatterButton.Initialize();
			_flySwatterButton.SubscribeOnClick(() =>
			{
				_skillSystem.InvokeTrigger(SkillTrigger.OnFlySwatterSkill);
				_skillSystem.InvokeTrigger(SkillTrigger.OnDamage);
			});

			_knifeButton.Initialize();
			_knifeButton.SubscribeOnClick(() =>
			{
				_skillSystem.InvokeTrigger(SkillTrigger.OnKnifeSkill);
				_skillSystem.InvokeTrigger(SkillTrigger.OnDamage);
			});

			_hammerButton.Initialize();
			_hammerButton.SubscribeOnClick(() =>
			{
				_skillSystem.InvokeTrigger(SkillTrigger.OnHammerSkill);
				_skillSystem.InvokeTrigger(SkillTrigger.OnDamage);
			});
		}
	}
}