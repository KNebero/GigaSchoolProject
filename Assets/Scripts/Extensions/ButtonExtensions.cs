using UnityEngine.Events;
using UnityEngine.UI;

namespace Extensions
{
	public static class ButtonExtensions
	{
		public static void SubscribeOnly(this Button button, UnityAction action)
		{
			button.onClick.RemoveAllListeners();
			button.onClick.AddListener(action);
		}
	}
}