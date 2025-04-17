using System.Collections.Generic;
using Global.SaveSystem.SavableObjects;
using Newtonsoft.Json;
using UnityEngine;

namespace Global.SaveSystem
{
	public class SaveSystem
	{
		private Dictionary<SavableObjectType, ISavable> _savableObjects;

		public SaveSystem()
		{
			_savableObjects = new()
			{
				{ SavableObjectType.Wallet, new Wallet() },
				{ SavableObjectType.Progress, new Progress() },
				{ SavableObjectType.OpenedSkills, new OpenedSkills() },
				{ SavableObjectType.Cash, new Cash() },
			};

			LoadData();
		}

		private void LoadData()
		{
			var keys = new List<SavableObjectType>(_savableObjects.Keys);
			foreach (var key in keys)
			{
				if (!PlayerPrefs.HasKey(key.ToString())) continue;
				var json = PlayerPrefs.GetString(key.ToString());
				_savableObjects[key] = (ISavable)JsonConvert.DeserializeObject(json, _savableObjects[key].GetType());
			}
		}

		public ISavable GetData(SavableObjectType objectType)
		{
			return _savableObjects[objectType];
		}

		public void SaveData(SavableObjectType objectType)
		{
			var objectToSave = _savableObjects[objectType];
			var json = JsonConvert.SerializeObject(objectToSave);
			PlayerPrefs.SetString(objectType.ToString(), json);
			PlayerPrefs.Save();
		}

		public void SaveAll()
		{
			foreach (var (key, value) in _savableObjects)
			{
				var json = JsonConvert.SerializeObject(value);
				PlayerPrefs.SetString(key.ToString(), json);
			}

			PlayerPrefs.Save();
		}
	}
}