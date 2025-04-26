using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Global.Translator
{
	public class TranslationManager
	{
		private static TranslationManager _instance;
		private static TranslationManager Instance => _instance ??= new TranslationManager("en");
		
		private LanguageConfig _languageConfig;
		private Dictionary<string, string> _languageMap;

		private string _language = "";
		private string Language
		{
			get => _language;
			set
			{
				if (_language == value) return;
				
				_languageConfig = Resources.Load<LanguageConfig>("Languages/" + value);
				_languageMap = new Dictionary<string, string>();
				foreach (var data in _languageConfig.Items)
				{
					_languageMap[data.Key] = data.Value;
				}
				_language = value;
			}
		}

		private TranslationManager(string language)
		{
			Language = language;
		}

		public static void SetLanguage(string language)
		{
			if (_instance == null)
			{
				_instance = new TranslationManager(language);
			}
			else
			{
				_instance.Language = language;
			}
		}

		public static string Translate(string key)
		{
			return Instance._languageMap.ContainsKey(key) ? Instance._languageMap[key] : key;
		}
	}
}