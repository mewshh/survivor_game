using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private Transform _weaponTransform;
    [SerializeField] private Transform _bulletSpawnPoint;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _fireRate = 1f;
    [SerializeField] private float _circleCastRadius = 2f;
    [SerializeField] private float _circleCastRange = 10f;
    [SerializeField] private LayerMask _enemyLayerMask;

    private float _nextFireTime;

    void Update()
    {
        DetectAndShootAtClosestEnemy();
    }

    private void DetectAndShootAtClosestEnemy()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, _circleCastRadius, transform.right, _circleCastRange, _enemyLayerMask);

        if (hits.Length == 0)
        {
            Debug.Log("No enemies detected.");
            return;
        }

        Transform closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.GetComponent<Enemy>())
            {
                float distance = Vector3.Distance(transform.position, hit.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = hit.transform;
                }
            }
        }

        if (closestEnemy != null)
        {
            AimAtEnemy(closestEnemy);

            if (Time.time >= _nextFireTime)
            {
                Shoot();
                _nextFireTime = Time.time + 1f / _fireRate;
            }
        }
    }

    private void AimAtEnemy(Transform enemy)
    {
        Vector3 lookDirection = enemy.position - _weaponTransform.position;
        lookDirection.z = 0;
        _weaponTransform.right = lookDirection;
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(_bulletPrefab, _bulletSpawnPoint.position, _bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(_bulletSpawnPoint.right * 10f, ForceMode2D.Impulse);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, _circleCastRadius);

        Vector3 endPosition = transform.position + transform.right * _circleCastRange;
        Gizmos.DrawWireSphere(endPosition, _circleCastRadius);

        Gizmos.DrawLine(transform.position, endPosition);
    }
}
