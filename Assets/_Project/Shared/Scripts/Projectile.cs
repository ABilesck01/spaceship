using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody rb;
    private float damage;

    public float Damage
    {
        get { return damage; }
        set { damage = value; }
    }


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        rb.AddForce(transform.forward * 30, ForceMode.Impulse);
        Destroy(gameObject, 5);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out IHealth health))
        {
            health.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
