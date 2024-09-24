using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 5f;
    [SerializeField] private Transform _playerVisual;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _circleCastRadius = 2f;
    [SerializeField] private float _circleCastRange = 10f;
    [SerializeField] private Transform _weaponTransform;
    [SerializeField] private LayerMask _enemyLayerMask;
    private UIManager _uiManager;

    private static string RUNNING_ANIMATION = "Running";

    [Inject]
    public void Construct(UIManager uiManager)
    {
        _uiManager = uiManager;
    }

    void Update()
    {
        if (_uiManager == null)
        {
            Debug.LogError("UIManager is not injected!");
            return;
        }

        MovePlayer();
        DetectAndAimAtClosestEnemy();
    }

    private void MovePlayer()
    {
        Vector2 direction = _uiManager.JoystickDirection;

        _animator.SetBool(RUNNING_ANIMATION, true);

        HandlePlayerRotation(direction);

        if (direction == Vector2.zero)
        {
            _animator.SetBool(RUNNING_ANIMATION, false);
            return;
        }

        transform.Translate(direction * _movementSpeed * Time.deltaTime);
    }

    private void HandlePlayerRotation(Vector2 direction)
    {
        if (direction == Vector2.zero) return;
        _playerVisual.localScale = new Vector3((direction.x > 0 ? 1 : -1), 1, 1);
    }

    private void DetectAndAimAtClosestEnemy()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, _circleCastRadius, transform.right, _circleCastRange);

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
            Vector3 lookDirection = closestEnemy.position - _weaponTransform.position;
            lookDirection.z = 0;
            _weaponTransform.right = lookDirection;
        }
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
