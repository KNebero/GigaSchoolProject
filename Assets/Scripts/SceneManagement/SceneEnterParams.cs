namespace SceneManagement
{
	public abstract class SceneEnterParams
	{
		public SceneEnterParams(string sceneName)
		{
			SceneName = sceneName;
		}

		public string SceneName { get; }
	}
}