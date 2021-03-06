using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    public abstract void Fire(bool value);
    public abstract void Reload();
}
