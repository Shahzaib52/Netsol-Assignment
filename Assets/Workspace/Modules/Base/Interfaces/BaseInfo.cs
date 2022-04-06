using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface BaseInfo
{
    public bool IsAlive();
    public void DealDamage(float damage);
    public Vector3 Position();
    public Vector3 Rotation();
}
