using Unity.VisualScripting;
using UnityEngine.Events;

namespace Global.SaveSystem.SavableObjects
{
	public class Wallet : ISavable
	{
		[field: DoNotSerialize] public event UnityAction<Wallet> OnChanged;

		private int _coins;
		public int Coins
		{
			get => _coins;
			set
			{
				_coins = value;
				OnChanged?.Invoke(this);
			}
		}
	}
}