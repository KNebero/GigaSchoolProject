using System.Collections;
using Global.AudioSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneManagement
{
	public class SceneLoader : MonoBehaviour
	{
		[SerializeField] private GameObject _loadingScreen;
		private AudioManager _audioManager;

		public void Initialize(AudioManager audioManager)
		{
			_audioManager = audioManager;
		}

		public void LoadMetaScene(SceneEnterParams enterParams = null)
		{
			StartCoroutine(LoadAndStartScene(enterParams, Scenes.MetaScene));
		}

		public void LoadGameplayScene(SceneEnterParams enterParams = null)
		{
			StartCoroutine(LoadAndStartScene(enterParams, Scenes.LevelScene));
		}

		private IEnumerator LoadAndStartScene(SceneEnterParams enterParams, string sceneToLoad = "")
		{
			_loadingScreen.SetActive(true);

			yield return LoadScene(Scenes.Loader);
			yield return LoadScene(sceneToLoad);

			var sceneEntryPoint = FindFirstObjectByType<EntryPoint>();
			sceneEntryPoint.Run(enterParams);

			_loadingScreen.SetActive(false);
		}

		private IEnumerator LoadScene(string sceneName)
		{
			_audioManager.Load(sceneName);
			yield return SceneManager.LoadSceneAsync(sceneName);
		}
	}
}