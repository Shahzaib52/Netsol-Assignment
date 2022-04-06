using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GattlingGun : BaseWeapon
{
    [SerializeField] private ParticleSystem Projectile;
    [SerializeField] private PhysXEventDispatcher EventDispatcher;
    [SerializeField] private bool Firing;

    private void OnEnable()
    {
        EventDispatcher._OnParticleCollision += OnParticleCollision;
    }

    private void OnDisable()
    {
        EventDispatcher._OnParticleCollision -= OnParticleCollision;
    }

    private void OnParticleCollision(GameObject other)
    {
        Events.DealDamage(1, other.GetInstanceID());
    }

    public override void Fire(bool value)
    {
        if (value && !Firing)
        {
            Firing = value;
            Projectile.Play(true);
        }
        else if (!value)
        {
            Firing = value;
            Projectile.Stop(true);
        }
    }

    public override void Reload()
    {
        //TODO: Implement reload method
    }
}
