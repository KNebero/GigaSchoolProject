using System;
using System.Collections.Generic;
using SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Global.AudioSystem
{
	public class AudioManager : MonoBehaviour
	{
		[SerializeField] private AudioSource _backgroundSounds;
		[SerializeField] private AudioSource _effectSounds;
		private Dictionary<string, AudioClip> _sceneClips = new Dictionary<string, AudioClip>();
		private Dictionary<string, AudioClip> _globalClips = new Dictionary<string, AudioClip>();

		public const string MetaPath = "Audio/Meta";
		public const string GamePath = "Audio/Game";
		public const string GlobalPath = "Audio/Global";

		public void LoadOnce()
		{
			_globalClips = LoadAudioClips(GlobalPath);
		}

		private Dictionary<string, AudioClip> LoadAudioClips(string path)
		{
			var clips = Resources.LoadAll<AudioClip>(path);
			var clipsDictionary = new Dictionary<string, AudioClip>();

			foreach (var audioClip in clips)
			{
				clipsDictionary.Add(audioClip.name, audioClip);
			}

			return clipsDictionary;
		}

		public void Load(string scene)
		{
			if (SceneManager.GetActiveScene().name == scene) return;

			UnloadAssets();
			_sceneClips = scene switch
			{
				Scenes.LevelScene => LoadAudioClips(GamePath),
				Scenes.MetaScene => LoadAudioClips(MetaPath),
				_ => _sceneClips
			};
		}

		private void UnloadAssets()
		{
			foreach (var sceneClip in _sceneClips.Values)
			{
				Resources.UnloadAsset(sceneClip);
			}
		}

		public void PlayClip(string clipName, bool isGlobal = false)
		{
			AudioClip clip;

			if (isGlobal)
			{
				clip = _globalClips[clipName];
				_backgroundSounds.Stop();
				_backgroundSounds.clip = clip;
				_backgroundSounds.Play();
			}
			else
			{
				clip = _sceneClips[clipName];
				_effectSounds.Stop();
				_effectSounds.clip = clip;
				_effectSounds.Play();
			}
		}
	}
}