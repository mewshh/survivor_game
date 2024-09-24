using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 5f;
    [SerializeField] private Transform _playerVisual;
    [SerializeField] private Animator _animator;
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
}
