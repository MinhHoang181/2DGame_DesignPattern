using System.Collections.Generic;
using UnityEngine;

namespace DesignPattern.Strategy
{
	public class WeaponController : MonoBehaviour
	{
		public Transform ShootPoint { get { return shootPoint; } }
		public WeaponScriptableObject Weapon { get { return weapons.Current.Value; } }

		[SerializeField] Transform shootPoint;
		[SerializeField] LayerMask hitLayers;

		[SerializeField] WeaponScriptableObject weaponTest;
		[SerializeField] WeaponScriptableObject weaponTest2;

		private IWeapon iWeapon;
		private CircularLinkedList<WeaponScriptableObject> weapons = new CircularLinkedList<WeaponScriptableObject>();

		// Start is called before the first frame update
		void Start()
		{
			weapons.Append(weaponTest);
			weapons.Append(weaponTest2);
			UseWeapon(weapons.Current.Value);
		}

		public void Fire(Vector2 direction)
		{
			iWeapon.Shoot(direction);
		}

		public void ChangeNextWeapon()
        {
			weapons.NextNode();
			UseWeapon(weapons.Current.Value);
        }

		public void ChangePrevWeapon()
        {
			weapons.PrevNode();
			UseWeapon(weapons.Current.Value);
        }

        private void UseWeapon(WeaponScriptableObject weapon)
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
            iWeapon.HitLayers = hitLayers;
			iWeapon.Weapon = weapon;

			GameController.Instance.weaponChangedEvent(weapon);
        }
    }
}
