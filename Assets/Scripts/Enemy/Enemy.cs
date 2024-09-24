using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _enemyRenderer;

    public SpriteRenderer EnemySpriteRenderer => _enemyRenderer;

    public void ChangeSprite(Sprite sprite)
    {
        _enemyRenderer.sprite = sprite;
    }
}
