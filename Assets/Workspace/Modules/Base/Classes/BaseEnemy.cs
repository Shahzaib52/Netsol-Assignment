using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour, EnemyInfo
{
    public abstract int GetID();
    public abstract Vector3 Position();
    public abstract Vector3 Rotation();
    public abstract void DealDamage(float damage);

    public abstract bool IsAlive();
}
