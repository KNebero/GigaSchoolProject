using Extensions;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Meta
{
	public class ConfirmationWindow : MonoBehaviour
	{
		[SerializeField] private Button _confirmButton;
		[SerializeField] private Button _cancelButton;
		[SerializeField] private TextMeshProUGUI _text;
		[SerializeField] private Button _closeArea;

		public void SetActive(bool active)
		{
			gameObject.SetActive(active);
		}
		
		public void Initialize(string question, UnityAction onConfirm = null, UnityAction onCancel = null)
		{
			_confirmButton.SubscribeOnly(() =>
			{
				onConfirm?.Invoke();
				SetActive(false);
			});
			_cancelButton.SubscribeOnly(() =>
			{
				onCancel?.Invoke();
				SetActive(false);
			});
			_closeArea.SubscribeOnly(() => _cancelButton.onClick?.Invoke());
			_text.text = question;
		}
	}
}