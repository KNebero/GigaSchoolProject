using System;
using UnityEngine;

namespace Global.Formulas
{
	public static class SkillsFormulas
	{
		public static float CalculateDamage(float baseDamage, int level)
		{
			return baseDamage * (10 - 20 / Mathf.Log(level + 4.666f, 2));
		}

		public static int CalculateCost(int baseCost, int newLevel)
		{
			return (int)Math.Round(baseCost * Math.Pow(2, Math.Sqrt(0.75 * newLevel)));
		}

		public static float CalculateCritChance(float baseCritChance, int level)
		{
			return baseCritChance * (level + 1);
		}

		public static float CalculateCritDamage(float baseCritDamage, int level)
		{
			return baseCritDamage * (level + 1);
		}

		public static float CalculateCoinsMultiplier(float baseCoinsMultiplier, int level)
		{
			return baseCoinsMultiplier * (level + 1);
		}

		public static int CalculateTimerIncreaseChance(float baseChance, int level)
		{
			return (int)baseChance * (level + 1);
		}

		public static int CalculateTimerIncrease(float baseTime, int level)
		{
			return (int)Mathf.Round(baseTime * (1 + 0.5f * (level + 1)));
		}
	}
}