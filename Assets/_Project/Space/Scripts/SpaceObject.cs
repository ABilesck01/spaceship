using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceObject : MonoBehaviour
{
    public virtual void Update()
    {
        transform.Translate(-transform.forward * GameManager.SPEED * Time.deltaTime);
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);

        if (other.TryGetComponent(out IHealth health))
        {
            health.TakeDamage(1);
        }
    }
}
