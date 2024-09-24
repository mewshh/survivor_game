using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _damage;

    private void Start()
    {
        Destroy(gameObject, 3);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HealthController healthController = collision.collider.GetComponent<HealthController>();
        if (healthController != null)
        {
            healthController.TakeDamage(_damage);
        }

        Destroy(gameObject);
    }

    public void DoDamage()
    {

    }
}
