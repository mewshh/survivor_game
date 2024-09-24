using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;

    private PlayerController _player;

    [Inject]
    public void Construct(PlayerController uiManager)
    {
        _player = uiManager;
    }

    void LateUpdate()
    {
        transform.position = _player.transform.position + _offset;
    }
}
