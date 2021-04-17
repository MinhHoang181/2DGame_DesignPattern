using System.Collections;
using System.Collections.Generic;
using DesignPattern.Factory;
using UnityEngine;

namespace DesignPattern.Strategy
{
	public class WeaponController : MonoBehaviour
	{
		[SerializeField] Transform shootPoint;
		[SerializeField] WeaponType weaponType;

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
			Component c = gameObject.GetComponent<IWeapon>() as Component;
			if (c != null)
			{
				Destroy(c);
			}

			switch (weaponType)
			{
				case WeaponType.Bullet:
					iWeapon = gameObject.AddComponent<Bullet>();
					break;
				default:
					iWeapon = gameObject.AddComponent<Bullet>();
					break;
			}
			iWeapon.ShootPoint = shootPoint;
		}
	}
}
