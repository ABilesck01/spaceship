using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceObject : MonoBehaviour
{
    [SerializeField] protected float score = 10;

    public virtual void Update()
    {
        transform.Translate(-transform.forward * GameManager.SPEED * Time.deltaTime);
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        //Destroy(gameObject);

        //if (other.TryGetComponent(out IHealth health))
        //{
        //    health.TakeDamage(1);
        //}

        if (other.TryGetComponent(out PlayerProjectile projectile))
        {
            DestroyObject(true);
            return;
        }
        DestroyObject(false);
    }

    public void DestroyObject(bool willScore)
    {
        if(willScore)
        {
            if(GameManager.Current != null)
                GameManager.Current.Score += (int)score;
            else
                CampaingManager.Current.Score += (int)score;
        }
        Destroy(gameObject);
    }
}
