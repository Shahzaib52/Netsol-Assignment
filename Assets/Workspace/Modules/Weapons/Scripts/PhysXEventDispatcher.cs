using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysXEventDispatcher : MonoBehaviour
{
    public delegate void GenericEvent<T>(T other);
    public GenericEvent<Collider> _OnTriggerEnter, _OnTriggerStay, _OnTriggerExit;
    public GenericEvent<Collision> _OnCollisionEnter, _OnCollisionStay, _OnCollisionExit;
    public GenericEvent<GameObject> _OnParticleCollision;

    private void OnTriggerEnter(Collider other) => _OnTriggerEnter?.Invoke(other);

    private void OnTriggerStay(Collider other) => _OnTriggerStay?.Invoke(other);

    private void OnTriggerExit(Collider other) => _OnTriggerExit?.Invoke(other);


    private void OnCollisionEnter(Collision other) => _OnCollisionEnter?.Invoke(other);

    private void OnCollisionExit(Collision other) => _OnCollisionExit?.Invoke(other);

    private void OnCollisionStay(Collision other) => _OnCollisionStay?.Invoke(other);

    private void OnParticleCollision(GameObject other) => _OnParticleCollision?.Invoke(other);
}
