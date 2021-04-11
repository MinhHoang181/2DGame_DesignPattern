using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPattern.Strategy
{
	public class WeaponController : MonoBehaviour
	{
		[SerializeField] WeaponType weaponType;
		[SerializeField] GameObject[] weapons;

		private IWeapon iWeapon;

		// Start is called before the first frame update
		void Start()
		{
			Weapon(weaponType);
		}

		// Update is called once per frame
		void Update()
		{

		}

		public void Fire()
		{
			iWeapon.Shoot();
		}

		//Xử lý loại vũ khí
		public void Weapon(WeaponType weaponType)
		{
			switch (weaponType)
			{
				case WeaponType.Bullet:
					iWeapon = gameObject.AddComponent<Bullet>();
					iWeapon.Weapon = weapons[0];
					break;
				default:
					iWeapon = gameObject.AddComponent<Bullet>();
					break;
			}
		}
	}
}
