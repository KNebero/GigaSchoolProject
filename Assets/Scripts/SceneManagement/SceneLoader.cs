using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneManagement
{
	public class SceneLoader : MonoBehaviour
	{
		[SerializeField] private GameObject _loadingScreen;

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

		private IEnumerator LoadScene(string SceneName)
		{
			yield return SceneManager.LoadSceneAsync(SceneName);
		}
	}
}