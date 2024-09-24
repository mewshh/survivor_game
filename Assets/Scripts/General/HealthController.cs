using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using Zenject;

public class HealthController : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100f;

    [Header("UI")]
    [SerializeField] private Image _filler;

    private float _currentHealth;
    private GameManager _gameManager;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    [Inject]
    public void Construct(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    public void TakeDamage(float damageAmount)
    {
        _currentHealth -= damageAmount;

        _filler.DOFillAmount(_currentHealth / _maxHealth, 0.15f).SetEase(Ease.Linear);

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float healAmount)
    {
        _currentHealth += healAmount;

        if (_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
    }

    public float GetCurrentHealth()
    {
        return _currentHealth;
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} has died.");

        GetComponent<Collider2D>().enabled = false;

        if (gameObject.GetComponent<Enemy>())
        {
            var enemy = gameObject.GetComponent<Enemy>();
            enemy.ChangeSprite(_gameManager.deathSprite);
            DOVirtual.DelayedCall(0.5f, () =>
            {
                enemy.transform.DOScale(0, 0.2f).SetEase(Ease.Linear);
            });
        }
        else if (gameObject.GetComponent<PlayerController>())
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
