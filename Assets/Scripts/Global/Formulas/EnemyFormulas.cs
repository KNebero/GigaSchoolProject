using System;
using UnityEngine;

namespace Global.Formulas
{
	public static class EnemyFormulas
	{
		public static float CalculateHealth(float baseHealth, int location, int level)
		{
			return baseHealth * (10 - 18 / Mathf.Log(location * 5 + level + 4, 2));
		}
	}
}