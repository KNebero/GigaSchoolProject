using System;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Translator
{
	[Serializable]
	public struct LanguageStringData
	{
		public string Key;
		public string Value;
	}
	
	[CreateAssetMenu(menuName = "Configs/LanguageConfig", fileName = "LanguageConfig")]
	public class LanguageConfig : ScriptableObject
	{
		public List<LanguageStringData> Items = new()
		{
			new()
			{
				Key = "FlySwatterSkillDisplayName",
				Value = "Fly Swatter"
			},
			new()
			{
				Key = "FlySwatterSkillDescription",
				Value = ""
			},
			new()
			{
				Key = "KnifeSkillDisplayName",
				Value = "Kitchen knife"
			},
			new()
			{
				Key = "KnifeSkillDescription",
				Value = ""
			},
			new()
			{
				Key = "HammerSkillDisplayName",
				Value = "Hammer"
			},
			new()
			{
				Key = "HammerSkillDescription",
				Value = ""
			},
			new()
			{
				Key = "AdditionalDamageSkillDisplayName",
				Value = "Strength"
			},
			new()
			{
				Key = "AdditionalDamageSkillDescription",
				Value = ""
			},
			new()
			{
				Key = "CritChanceDisplayName",
				Value = "Crit chance"
			},
			new()
			{
				Key = "CritChanceDescription",
				Value = ""
			},
			new()
			{
				Key = "CritDamageDisplayName",
				Value = "Crit damage"
			},
			new()
			{
				Key = "CritDamageDescription",
				Value = ""
			},
			new()
			{
				Key = "AdditionalCoinsSkillDisplayName",
				Value = "Midas's hand"
			},
			new()
			{
				Key = "AdditionalCoinsSkillDescription",
				Value = ""
			},
			new()
			{
				Key = "TimeIncreasingSkillDisplayName",
				Value = "Poison Resistance"
			},
			new()
			{
				Key = "TimeIncreasingSkillDescription",
				Value = ""
			}
		};
	}
}