using System.Collections.Generic;
using DesignPattern.Factory;
using UnityEngine;

namespace DesignPattern.Strategy
{
	public class WeaponController : MonoBehaviour
	{
		public Transform ShootPoint { get { return shootPoint; } }
		public ScriptableWeapon Weapon { get { return currentWeapon; } }

		[SerializeField] Transform shootPoint;
		private Character character;

		private IWeapon iWeapon;
		private List<ScriptableWeapon> weapons = new List<ScriptableWeapon>();

		private ScriptableWeapon currentWeapon;
		private int currentIndex = 0;

		// Start is called before the first frame update
		void Start()
		{
			character = transform.GetComponent<Character>();

			currentWeapon = weapons[currentIndex];
			UseWeapon(currentWeapon);
		}

		public void Fire(Vector2 direction)
		{
			if (iWeapon != null)
            {
				iWeapon.Shoot(direction);
			}
		}

		public void AddWeapon(ScriptableWeapon weapon)
        {
			if (weapons.Contains(weapon))
            {

            } else
            {
				weapons.Add(weapon);
			}
        }

		public void ChangeNextWeapon()
        {
			if (currentIndex >= weapons.Count - 1)
            {
				currentIndex = 0;
            } else
            {
				currentIndex++;
            }

			currentWeapon = weapons[currentIndex];
			UseWeapon(currentWeapon);
        }

		public void ChangePrevWeapon()
        {
			if (currentIndex <= 0)
            {
				currentIndex = weapons.Count - 1;
            } else
            {
				currentIndex--;
            }

			currentWeapon = weapons[currentIndex];
			UseWeapon(currentWeapon);
		}

        private void UseWeapon(ScriptableWeapon weapon)
        {
            Component c = gameObject.GetComponent<IWeapon>() as Component;
            if (c != null)
            {
                Destroy(c);
            }

			switch (weapon.type)
            {
				case WeaponType.Piston:
					iWeapon = gameObject.AddComponent<Piston>();
					break;
				default:
					iWeapon = gameObject.AddComponent<Hand>();
					break;
			}

            iWeapon.ShootPoint = shootPoint;
			iWeapon.Weapon = weapon;

			GameController.Instance.weaponChangedEvent(character, weapon);
        }
    }
}
