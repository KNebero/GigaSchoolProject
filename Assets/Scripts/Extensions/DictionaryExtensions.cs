using System.Collections.Generic;

namespace Extensions
{
	public static class DictionaryExtensions
	{
		public static TValue GetOrCreate<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key)
			where TValue : new()
		{
			return dictionary.ContainsKey(key) ? dictionary[key] : (dictionary[key] = new TValue());
		}

		public static bool IsNullOrEmpty<TKey, TValue>(this Dictionary<TKey, TValue> dictionary)
		{
			return dictionary == null || dictionary.Count == 0;
		}
	}
}