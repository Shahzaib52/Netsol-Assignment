using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStationary : BaseEnemy
{
    [SerializeField] private float MaxHealth = 100;
    [SerializeField] private GameObject Collider;

    private void OnEnable()
    {
        Events.OnDealDamage += OnDealDamage;
    }

    private void OnDisable()
    {
        Events.OnDealDamage -= OnDealDamage;
    }

    private void OnDealDamage(float damage, int id)
    {
        if (id == Collider.GetInstanceID())
        {
            DealDamage(damage);
        }
    }


    public override void DealDamage(float damage)
    {
        if (MaxHealth > 0)
            MaxHealth -= damage;
        else
        {
            MaxHealth = 0;
            gameObject.SetActive(false);
            Invoke(nameof(Respawn), 10);
        }
    }

    void Respawn()
    {
        MaxHealth = 100;
        gameObject.SetActive(true);
    }

    public override int GetID() => Collider.GetInstanceID();
    public override Vector3 Position() => this.transform.position;
    public override Vector3 Rotation() => this.transform.eulerAngles;

    public override bool IsAlive() => MaxHealth > 0;
}
