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

		private IWeapon iWeapon;
		private CircularLinkedList<WeaponScriptableObject> weapons = new CircularLinkedList<WeaponScriptableObject>();

		// Start is called before the first frame update
		void Start()
		{
			weapons.Append(weaponTest);
			UseWeapon(weapons.Current.Value);
		}

		// Update is called once per frame
		void Update()
		{

		}

		public void Fire(Vector2 direction)
		{
			iWeapon.Shoot(direction);
		}

        public void UseWeapon(WeaponScriptableObject weapon)
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
					iWeapon = gameObject.AddComponent<Piston>();
					break;
			}

            iWeapon.ShootPoint = shootPoint;
            iWeapon.HitLayers = hitLayers;
			iWeapon.Weapon = weapon;

			GameController.Instance.weaponChangedEvent(weapon);
        }
    }
}
