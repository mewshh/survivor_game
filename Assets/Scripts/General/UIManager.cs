using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class UIManager : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private RectTransform _joystickHandle;
    [SerializeField] private RectTransform _joystickBase;
    [SerializeField] private float _joystickRadius = 100f;
    [SerializeField] private TMP_Text _killText;
    [SerializeField] private CanvasGroup _gameoverGroup;

    private Vector2 _inputVector;
    private Vector2 _joystickCenter;

    public Vector2 JoystickDirection => _inputVector.normalized;

    void Start()
    {
        _joystickCenter = _joystickBase.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 touchPosition = eventData.position;

        _inputVector = touchPosition - _joystickCenter;

        _inputVector = Vector2.ClampMagnitude(_inputVector, _joystickRadius);

        _joystickHandle.position = _joystickCenter + _inputVector;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _inputVector = Vector2.zero;
        _joystickHandle.position = _joystickCenter;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _joystickCenter = _joystickBase.position;
        OnDrag(eventData);
    }

    public void UpdateKillCounterText(int killCount)
    {
        _killText.text = $"{killCount}";
    }

    public void ShowDarkPanel()
    {
        _gameoverGroup.gameObject.SetActive(true);
        _gameoverGroup.DOFade(1, 0.3f).SetEase(Ease.Linear);
    }
}
