using Global.AudioSystem;
using Global.SaveSystem;
using Global.Translator;
using Meta;
using UnityEngine;
using UnityEngine.UIElements;

namespace SceneManagement
{
	public class CommonObject : MonoBehaviour
	{
		public SceneLoader SceneLoader;
		public AudioManager AudioManager;
		public SaveSystem SaveSystem;
		public ConfirmationWindow ConfirmationWindow;
	}
}