using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Global.Translator
{
	public class TranslationManager
	{
		private static TranslationManager _instance;
		private static TranslationManager Instance => _instance ??= new TranslationManager();

		private LanguageConfig _language;
		private Dictionary<string, string> _languageMap;

		private TranslationManager()
		{
		}

		public static void SetLanguage(string language)
		{
			Instance._language = Resources.Load<LanguageConfig>("Languages/" + language);
			Instance._languageMap = new Dictionary<string, string>();
			foreach (var data in Instance._language.Items)
			{
				Instance._languageMap[data.Key] = data.Value;
			}
		}

		public static string Translate(string key)
		{
			return Instance._languageMap.ContainsKey(key) ? Instance._languageMap[key] : null;
		}
	}
}