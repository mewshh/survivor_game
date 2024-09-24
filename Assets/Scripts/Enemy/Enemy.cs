using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _enemyRenderer;
    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _damageInterval = 1f;
    [SerializeField] private float _damageAmount = 10f;
    [SerializeField] private float _detectionRadius = 1.5f;
    [SerializeField] private float _stoppingDistance = 1f;

    private PlayerController _player;
    private Coroutine _damageCoroutine;
    private Sprite _defaultSprite;
    public SpriteRenderer EnemySpriteRenderer => _enemyRenderer;

    private void Update()
    {
        if (_player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, _player.transform.position);

            if (distanceToPlayer > _stoppingDistance)
            {
                Vector3 direction = (_player.transform.position - transform.position).normalized;
                transform.position += direction * _speed * Time.deltaTime;
            }

            if (distanceToPlayer <= _detectionRadius)
            {
                if (_damageCoroutine == null)
                {
                    _damageCoroutine = StartCoroutine(DealDamage());
                }
            }
            else
            {
                if (_damageCoroutine != null)
                {
                    StopCoroutine(_damageCoroutine);
                    _damageCoroutine = null;
                }
            }
        }
    }

    public void Initialize(PlayerController player)
    {
        _player = player;
        if(_defaultSprite == null) _defaultSprite = _enemyRenderer.sprite;

        EnemySpriteRenderer.sprite = _defaultSprite;
    }

    private IEnumerator DealDamage()
    {
        while (_player != null && Vector3.Distance(transform.position, _player.transform.position) <= _detectionRadius)
        {
            _player.GetComponent<HealthController>().TakeDamage(_damageAmount);
            yield return new WaitForSeconds(_damageInterval);
        }

        _damageCoroutine = null;
    }

    private void OnDisable()
    {
        if (_damageCoroutine != null)
        {
            StopCoroutine(_damageCoroutine);
            _damageCoroutine = null;
        }
    }
}
