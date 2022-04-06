using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsController : MonoBehaviour
{
    [SerializeField] private List<BaseWeapon> Weapons;
    [SerializeField] private int ActiveWeaponIndex = 0;
    [SerializeField] private bool AutoFire;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Weapons.Count > 0)
        {
            var engage = false;
            if (AutoFire)
            {
                var nearestEnemy = Events.GetNearestEnemy();
                if (nearestEnemy != null)
                {
                    engage = true;
                    var weapon = Weapons[ActiveWeaponIndex];
                    weapon.transform.LookAt(nearestEnemy.Position());
                }
            }

            if (Input.GetKeyDown(KeyCode.O)) AutoFire = !AutoFire;
            Weapons[ActiveWeaponIndex].Fire(Input.GetKey(KeyCode.Space) || engage);
        }
    }
}
