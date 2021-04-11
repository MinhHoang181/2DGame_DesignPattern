using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] WeaponType weaponType;
    [SerializeField] FlameType flameColor;

    private IWeapon iWeapon;
    private IFlame iFlame;

    // Start is called before the first frame update
    void Start()
    {
		Weapon(weaponType);
		Flame(flameColor);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void Fire()
	{
		iWeapon.Shoot();
		iFlame.ShowFlame();
	}

	//Xử lý loại vũ khí
	public void Weapon(WeaponType weaponType)
	{
		switch (weaponType)
		{

			case WeaponType.Missile:
				iWeapon = new Missile();
				break;
			case WeaponType.Bullet:
				iWeapon = new Bullet();
				break;
			default:
				iWeapon = new Bullet();
				break;
		}
	}

	public void Flame(FlameType flame)
	{
		switch (flame)
		{
			case FlameType.Blue:
				iFlame = new BlueFlame();
				break;
			case FlameType.Red:
				iFlame = new RedFlame();
				break;
			default:
				iFlame = new BlueFlame();
				break;
		}
	}
}
