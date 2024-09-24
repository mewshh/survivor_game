using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 5f;
    private UIManager _uiManager;

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

        if (direction == Vector2.zero)
        {
            return;
        }

        transform.Translate(direction * movementSpeed * Time.deltaTime);
    }
}
